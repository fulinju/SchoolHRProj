using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 发布类型
    /// </summary>
    [TableName("T_PMType")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_PMType
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }


        [Column]
        [JsonProperty("pm_typeid")]
        public string PM_TypeID { get; set; }

        [Column]
        [JsonProperty("pm_typevalue")]
        public string PM_TypeValue { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }

    }
}
