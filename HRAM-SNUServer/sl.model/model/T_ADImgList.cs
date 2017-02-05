using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 发布的广告列表
    /// </summary>
    [TableName("T_ADImgList")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_ADImgList
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("pm_adimglistld")]
        public string PM_ADImgListID { get; set; }

        [Column]
        [JsonProperty("pm_adimglisturl")]
        public string PM_ADImgListURL { get; set; }


        [Column]
        [JsonProperty("pm_adimglistnum")]
        public string PM_ADImgListNum { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }
    }
}
