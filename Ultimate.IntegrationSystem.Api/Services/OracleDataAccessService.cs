using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using Ultimate.IntegrationSystem.Api.DBMangers;
using Ultimate.IntegrationSystem.Api.Interface;
using Ultimate.IntegrationSystem.Api.Models;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;

namespace Ultimate.IntegrationSystem.Api.Services
{
   

    public class OracleDataAccessService : IDataAccessService
    {

        private OracleConnection _con;
        private const string _poolSettings = "Pooling=True;Max Pool Size=50;";
        private readonly ILogger<OracleDataAccessService> _logger;
        private readonly IConfiguration _config;
        private readonly IDbModelMappingService _dbModelMapping;
        private string connectionString;
        private string _currentSchema;
        private string _host;
        private readonly IntegrationApiDbContext _dbContext;

        private string _username;
        private string _password;
        private int _port;
        private string _serviceName;
        private string _systemType;

        public OracleDataAccessService(ILogger<OracleDataAccessService> logger,
                                IConfiguration config,
                                IDbModelMappingService dbModelMapping,
                                IntegrationApiDbContext dbContext)
        {
            _logger = logger;
            _config = config;
            _dbModelMapping = dbModelMapping;
            _dbContext = dbContext;

            // Fetch settings from DBSetting asynchronously
            var settings = _dbContext.ConnectionSettings.FirstOrDefaultAsync(s => s.ID == 1).Result;

            if (settings != null)
            {
                // Set values from DBSetting or use default values if null
                _currentSchema = settings.SelectedSystem == "ONYX" ? $"IAS{settings.Year.ToString()}{settings.Activity.ToString()}" : $"{settings.Year.ToString()}{settings.Activity.ToString()}";
                _username = settings.SchemaName;
                _password = settings.Password;
                _host = settings.Host ?? throw new ArgumentNullException("Host cannot be null.");
                _serviceName = settings.ServiceName ?? throw new ArgumentNullException("ServiceName cannot be null.");
                _port = settings.Port; // Default to 1521 if not specified in DBSetting
                _systemType = settings.SelectedSystem ?? "ONYX";
                // Check if any important settings are missing and log an error if needed
                if (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password))
                {
                    _logger.LogError("Username or password is missing in DBSetting.");
                    throw new Exception("Username or password is missing in DBSetting.");
                }

            }
            else
            {
                _logger.LogError("No valid settings found in DBSetting.");
                throw new Exception("No valid settings found in DBSetting.");
            }

            // Construct the connection string with appropriate parameters
            connectionString = $"User Id={_currentSchema};" +
                               $"Proxy User Id=onyxproxy;Proxy Password=ys$onyx#proxy;" +
                               $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={_host})(PORT={_port})))" +
                               $"(CONNECT_DATA=(SERVICE_NAME={_serviceName})));{_poolSettings}";


            _con = new OracleConnection(string.Format(connectionString, _currentSchema));
        }

        /// <summary>
        /// Sets the Oracle schema based on year and branch.
        /// Example: branchYear = "2024", branchUser = "01" => schema = "202401"
        /// </summary>
        public void SetSchemaFromParts(string branchYear, string branchUser)
        {
            if (!string.IsNullOrWhiteSpace(branchYear) && !string.IsNullOrWhiteSpace(branchUser))
            {
                _currentSchema = $"ias{branchYear}{branchUser}";
            }
            else
            {
                _currentSchema = null;
                //   throw new ArgumentException("branchYear and branchUser cannot be null or empty.");
            }
        }
        /// <summary>
        /// get employees as json
        /// </summary>
        /// <param name="P_EMP_NO"></param>
        /// <param name="P_EMP_NO_FROM"></param>
        /// <param name="P_EMP_NO_TO"></param>
        /// <param name="P_LNG_NO"></param>
        /// <returns></returns>
        public async Task<string> GetEmployeesAsJson(  int? P_EMP_NO = null, int? P_EMP_NO_FROM = null,int? P_EMP_NO_TO = null,int? P_LNG_NO = 1)
        {
            //    var schema = _currentSchema ?? _config["Schema"];
            _con = new OracleConnection(string.Format(connectionString, _currentSchema));
           using var cmd = new OracleCommand(@"
            BEGIN
              :p0 := HRS_INTEGRATE_PLATFORMS.GET_EMPLOYEE_JSON(
                P_EMP_NO       => :P_EMP_NO,
                P_EMP_NO_FROM  => :P_EMP_NO_FROM,
                P_EMP_NO_TO    => :P_EMP_NO_TO,
                P_LNG_NO       => :P_LNG_NO
              );
            END;", _con);

            var p0 = cmd.Parameters.Add("p0", OracleDbType.Clob, ParameterDirection.ReturnValue);
            cmd.Parameters.Add("P_EMP_NO", OracleDbType.Int64).Value = (object?)P_EMP_NO ?? DBNull.Value;
            cmd.Parameters.Add("P_EMP_NO_FROM", OracleDbType.Int64).Value = (object?)P_EMP_NO_FROM ?? DBNull.Value;
            cmd.Parameters.Add("P_EMP_NO_TO", OracleDbType.Int64).Value = (object?)P_EMP_NO_TO ?? DBNull.Value;
            cmd.Parameters.Add("P_LNG_NO", OracleDbType.Int64).Value = (object?)P_LNG_NO ?? DBNull.Value;

            await _con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            var clob = (OracleClob)p0.Value;
            return (clob == null || clob.IsNull) ? "" : clob.Value;
        }


        /// <summary>
        /// get employee documents as json
        /// </summary>
        /// <param name="P_EMP_NO"></param>
        /// <param name="P_CODE_NO"></param>
        /// <param name="P_DCMNT_TYP_NO"></param>
        /// <param name="P_DOC_OWNR_TYP"></param>
        /// <returns></returns>

        public async Task<string> GetEmpDocsAsJson(
    decimal? P_EMP_NO = null,
    decimal? P_CODE_NO = null,
    decimal? P_DCMNT_TYP_NO = null,
    decimal? P_DOC_OWNR_TYP = null)
    {
        _con = new OracleConnection(string.Format(connectionString, _currentSchema));

        using var cmd = new OracleCommand(@"
            BEGIN
              :p0 := HRS_INTEGRATE_PLATFORMS.GET_EMP_DOC_LIST_CUR(
                P_EMP_NO        => :P_EMP_NO,
                P_CODE_NO       => :P_CODE_NO,
                P_DCMNT_TYP_NO  => :P_DCMNT_TYP_NO,
                P_DOC_OWNR_TYP  => :P_DOC_OWNR_TYP
              );
            END;", _con);

        cmd.BindByName = true;

        // قيمة الإرجاع (CLOB)
        var p0 = cmd.Parameters.Add("p0", OracleDbType.Clob, ParameterDirection.ReturnValue);

        // معاملات الإدخال
        cmd.Parameters.Add("P_EMP_NO", OracleDbType.Decimal).Value = (object?)P_EMP_NO ?? DBNull.Value;
        cmd.Parameters.Add("P_CODE_NO", OracleDbType.Decimal).Value = (object?)P_CODE_NO ?? DBNull.Value;
        cmd.Parameters.Add("P_DCMNT_TYP_NO", OracleDbType.Decimal).Value = (object?)P_DCMNT_TYP_NO ?? DBNull.Value;
        cmd.Parameters.Add("P_DOC_OWNR_TYP", OracleDbType.Decimal).Value = (object?)P_DOC_OWNR_TYP ?? DBNull.Value;

        try
        {
            await _con.OpenAsync().ConfigureAwait(false);
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

            var clob = p0.Value as OracleClob;
            return (clob == null || clob.IsNull) ? "" : clob.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GET_EMP_DOC_LIST_CUR failed");
            throw;
        }
        finally
        {
            if (_con.State == ConnectionState.Open)
                _con.Close();
        }
    }









        public async Task<string> GetDocsAndRnwlAsJson(
    // فلاتر جدول HRS_EMP_DOC_DTL
    decimal? P_EMP_NO = null,
    decimal? P_CODE_NO = null,
    decimal? P_DCMNT_TYP_NO = null,
    string P_DOC_NO = null,          // DOC_NO في جدولك VARCHAR2(60)
    decimal? P_SUB_CODE_NO = null,
    int? P_ONLY_ACTIVE = 1,        // 1=Active فقط, 0=الكل

    // فلاتر جدول HRS_RNWL_EMP_DOC_DTL
    decimal? P_RNWL_DOC_TYP = 810,     // غالباً 810 لمسار التجديد
    decimal? P_RNWL_DOC_NO = null,
    decimal? P_RNWL_DOC_SRL = null
)
        {
            using var _con = new OracleConnection(string.Format(connectionString, _currentSchema));

            using var cmd = new OracleCommand(@"
        BEGIN
          :p0 := HRS_INTEGRATE_PLATFORMS.GET_DOCS_AND_RNWL_JSON(
            P_EMP_NO        => :P_EMP_NO,
            P_CODE_NO       => :P_CODE_NO,
            P_DCMNT_TYP_NO  => :P_DCMNT_TYP_NO,
            P_DOC_NO        => :P_DOC_NO,
            P_SUB_CODE_NO   => :P_SUB_CODE_NO,
            P_ONLY_ACTIVE   => :P_ONLY_ACTIVE,
            P_RNWL_DOC_TYP  => :P_RNWL_DOC_TYP,
            P_RNWL_DOC_NO   => :P_RNWL_DOC_NO,
            P_RNWL_DOC_SRL  => :P_RNWL_DOC_SRL
          );
        END;", _con);

            cmd.BindByName = true;

            // قيمة الإرجاع (CLOB)
            var p0 = cmd.Parameters.Add("p0", OracleDbType.Clob, ParameterDirection.ReturnValue);

            // معاملات الإدخال — لاحظ استخدام DBNull.Value عندما تكون null
            cmd.Parameters.Add("P_EMP_NO", OracleDbType.Decimal).Value = (object?)P_EMP_NO ?? DBNull.Value;
            cmd.Parameters.Add("P_CODE_NO", OracleDbType.Decimal).Value = (object?)P_CODE_NO ?? DBNull.Value;
            cmd.Parameters.Add("P_DCMNT_TYP_NO", OracleDbType.Decimal).Value = (object?)P_DCMNT_TYP_NO ?? DBNull.Value;
            cmd.Parameters.Add("P_DOC_NO", OracleDbType.Varchar2).Value = (object?)P_DOC_NO ?? DBNull.Value;
            cmd.Parameters.Add("P_SUB_CODE_NO", OracleDbType.Decimal).Value = (object?)P_SUB_CODE_NO ?? DBNull.Value;
            cmd.Parameters.Add("P_ONLY_ACTIVE", OracleDbType.Decimal).Value = (object?)P_ONLY_ACTIVE ?? DBNull.Value;

            cmd.Parameters.Add("P_RNWL_DOC_TYP", OracleDbType.Decimal).Value = (object?)P_RNWL_DOC_TYP ?? DBNull.Value;
            cmd.Parameters.Add("P_RNWL_DOC_NO", OracleDbType.Decimal).Value = (object?)P_RNWL_DOC_NO ?? DBNull.Value;
            cmd.Parameters.Add("P_RNWL_DOC_SRL", OracleDbType.Decimal).Value = (object?)P_RNWL_DOC_SRL ?? DBNull.Value;

            try
            {
                await _con.OpenAsync().ConfigureAwait(false);
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                using var clob = p0.Value as OracleClob;
                return (clob == null || clob.IsNull) ? string.Empty : clob.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET_DOCS_AND_RNWL_JSON failed");
                throw;
            }
        }







        public async Task<string> GetBranchJson(int? P_USR_NO = 1, int P_LNG_NO = 1)
        {
         //   var schema = _currentSchema ?? _config["Schema"];
            //string connectionString = string.Format(_config["ConnectionString"], schema); // تأكيد على استخدام connection string من الإعدادات
            using (_con = new OracleConnection(string.Format(connectionString, _currentSchema)))
            {
                // استعلام PL/SQL لاستدعاء دالة GET_Branch_JSON
                string plsqlQuery = @"
        Begin 
            :p0 := HRS_INTEGRATE_PLATFORMS.GET_Branch_JSON(
                        P_USR_NO => :P_USR_NO
                    );
        end;";

                using (var command = new OracleCommand(plsqlQuery, _con))
                {
                    // إضافة المعاملات إلى الاستعلام
                    var p0 = new OracleParameter("p0", OracleDbType.Clob, ParameterDirection.ReturnValue);
                    command.Parameters.Add(p0);
                    command.Parameters.Add(new OracleParameter("P_USR_NO", OracleDbType.Int64)).Value = P_USR_NO ?? 1;  // التحقق من القيمة إذا كانت null


                    try
                    {
                        // فتح الاتصال
                        await _con.OpenAsync();

                        // تنفيذ الاستعلام
                        await command.ExecuteNonQueryAsync();

                        // استخراج CLOB الناتج
                        var returnValue = p0.Value as Oracle.ManagedDataAccess.Types.OracleClob;
                        var result = returnValue?.IsNull == true ? string.Empty : returnValue?.Value.ToString() ?? string.Empty;

                        return result;
                    }
                    catch (OracleException ex)
                    {
                        _logger.LogError(ex, "Oracle Database error while fetching branch data.");
                        throw new ApplicationException("Database operation failed.", ex);  // استخدام استثناء مخصص مع رسالة مفيدة
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "General error during branch data retrieval.");
                        throw;
                    }
                    finally
                    {
                        // ضمان إغلاق الاتصال بشكل صحيح
                        if (_con.State == System.Data.ConnectionState.Open)
                            await _con.CloseAsync();
                    }
                }
            }
        }


     

      







       

        public bool ValidateOnyxUser(int? uId, string uPassword)
        {


            _con = new OracleConnection(string.Format(connectionString, _currentSchema));
            OracleCommand oracleCommand = new OracleCommand("IAS_WEB_DOC_PKG.Validate_Onyx_User", _con);
            oracleCommand.CommandType = CommandType.StoredProcedure;
            oracleCommand.Parameters.Add("v_result", OracleDbType.Int32);
            oracleCommand.Parameters["v_result"].Direction = ParameterDirection.ReturnValue;
            oracleCommand.Parameters.Add("p_user_id", OracleDbType.Int32);
            oracleCommand.Parameters["p_user_id"].Value = uId;
            oracleCommand.Parameters.Add("p_user_password", OracleDbType.Varchar2);
            oracleCommand.Parameters["p_user_password"].Value = uPassword;
            try
            {
                if (_con.State != ConnectionState.Open)
                {
                    _con.Open();
                }
                oracleCommand.ExecuteReader();
                int obj = int.Parse(oracleCommand.Parameters["v_result"].Value.ToString());
                return obj == 1;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "");
                throw new Exception(e.Message);
                // Logger.Add(e.Message + " \r\n" + e.StackTrace, null, LogType.Error);
                //return false;
            }
            finally
            {
                _con.Dispose();
                // Dispose();
            }
        }









      
     

       

    }
}
