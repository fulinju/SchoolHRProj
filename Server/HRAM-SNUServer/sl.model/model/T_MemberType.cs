using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 会员类型
    /// </summary>
    [TableName("T_MemberType")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_MemberType
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("mTypeID")]
        public string mTypeID { get; set; }

        [Column]
        [JsonProperty("mTypeValue")]
        public string mTypeValue { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
}
