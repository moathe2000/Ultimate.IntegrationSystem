using Newtonsoft.Json;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Ultimate.IntegrationSystem.Api.Interface;
using Ultimate.IntegrationSystem.Api.Models;
using JsonException = Newtonsoft.Json.JsonException;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Ultimate.IntegrationSystem.Api.Services
{
    public class DbModelMappingService : IDbModelMappingService
    {

        private readonly ILogger<DbModelMappingService> _logger;
        public DbModelMappingService(ILogger<DbModelMappingService> logger)
        {
            _logger = logger;
        }
        
        public DbTransResultModel GetDbResultModelFromJson(string jsonString)
        {
            try
            {
                // تحويل سلسلة JSON إلى كائن DbTransResultModel باستخدام JsonConvert
                var result = JsonConvert.DeserializeObject<DbTransResultModel>(jsonString);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "حدث خطأ أثناء تحويل البيانات من JSON");
                throw;
            }
        }

        public List<T> MapJson<T>(DbTransResultModel model)
        {
            List<T> result = new List<T>();
            try
            {
                var serializerForInternal = new JsonSerializer();

                foreach (var item in model.Data)
                {
                    // تحويل العنصر إلى سلسلة JSON
                    string jsonString = item.ToString();

                    try
                    {
                        // استخدام JsonSerializer لتحويل السلسلة JSON إلى كائن
                        using (var reader = new JsonTextReader(new StringReader(jsonString)))
                        {
                            T deserializedItem = serializerForInternal.Deserialize<T>(reader);
                            result.Add(deserializedItem);
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        // تسجيل الخطأ عندما يحدث فشل في التحويل
                        _logger.LogError(jsonEx, $"خطأ أثناء تحويل العنصر إلى JSON: {jsonString}");
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "خطأ أثناء تحويل البيانات من JSON");
                throw;
            }

            return result;
        }

       

        
    }
}
