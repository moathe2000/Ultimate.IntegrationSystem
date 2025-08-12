using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Ultimate.IntegrationSystem.Api.Interface;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.Models
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(IsNullable = true, ElementName = "Response")]
    public partial class DbTransResultModel
    {

        private ResponseResult resultField;
        private dynamic objectField;

        /// <remarks/>
        public ResponseResult Result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }

        [XmlElement("Data")]
        public dynamic Data
        {
            get
            {
                return objectField;
            }
            set
            {
                objectField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResult
    {

        private string msgNo;

        private string msgTxt;

        /// <remarks/>
        [XmlElement("MSGNO")]
        public string MsgNo
        {
            get
            {
                return this.msgNo;
            }
            set
            {
                this.msgNo = value;
            }
        }

        /// <remarks/>
        [XmlElement("MSGTXT")]
        public string MsgTxt
        {
            get
            {
                return this.msgTxt;
            }
            set
            {
                this.msgTxt = value;
            }
        }
    }
}
