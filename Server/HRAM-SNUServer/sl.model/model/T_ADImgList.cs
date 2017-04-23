using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 发布的广告列表
    /// </summary>
    [TableName("T_ADImgList")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_ADImgList
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("pmADImgListID")]
        public string pmADImgListID { get; set; }

        [Column]
        [JsonProperty("pmADImgListURL")]
        public string pmADImgListURL { get; set; }


        [Column]
        [JsonProperty("pmADImgListNum")]
        public int pmADImgListNum { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
}
