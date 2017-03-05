using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 发布类型
    /// </summary>
    [TableName("T_PMType")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_PMType
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }


        [Column]
        [JsonProperty("pmTypeID")]
        public string pmTypeID { get; set; }

        [Column]
        [JsonProperty("pmTypeValue")]
        public string pmTypeValue { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }

    }
}
