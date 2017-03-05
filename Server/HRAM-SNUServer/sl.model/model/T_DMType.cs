using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 下载类型
    /// </summary>
    [TableName("T_DMType")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_DMType
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("dmTypeID")]
        public string dmTypeID { get; set; }

        [Column]
        [JsonProperty("dmTypeValue")]
        public string dmTypeValue { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
}
