using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    [TableName("T_PublishManage")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_PublishManage
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("u_loginname")]
        public string U_LoginName { get; set; }

        [Column]
        [JsonProperty("pm_title")]
        public string PM_Title { get; set; }

        [Column]
        [JsonProperty("pm_typeid")]
        public string PM_TypeID { get; set; }

        [Column]
        [JsonProperty("pm_adimglistid")]
        public string PM_ADImgListID { get; set; }

        [Column]
        [JsonProperty("pm_publishtime")]
        public string PM_PublishTime { get; set; }

        [Column]
        [JsonProperty("pm_views")]
        public int PM_Views { get; set; }

        [Column]
        [JsonProperty("pm_text")]
        public string PM_Text { get; set; }

        [Column]
        [JsonProperty("pm_preview")]
        public string PM_Preview { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }

        //忽略该属性
        //[JsonIgnore]
        //public string Check_Code { get; set; }
    }
}
