using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    [TableName("T_PublishManage")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_PublishManage
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("uLoginName")]
        public string uLoginName { get; set; }

        [Column]
        [JsonProperty("pmTitle")]
        public string pmTitle { get; set; }

        [Column]
        [JsonProperty("pmTypeID")]
        public string pmTypeID { get; set; }

        [Column]
        [JsonProperty("pmADImgListID")]
        public string pmADImgListID { get; set; }

        [Column]
        [JsonProperty("pmPublishTime")]
        public string pmPublishTime { get; set; }

        [Column]
        [JsonProperty("pmViews")]
        public int pmViews { get; set; }

        [Column]
        [JsonProperty("pmText")]
        public string pmText { get; set; }

        [Column]
        [JsonProperty("pmPreview")]
        public string pmPreview { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }

        //忽略该属性
        //[JsonIgnore]
        //public string Check_Code { get; set; }
    }
}
