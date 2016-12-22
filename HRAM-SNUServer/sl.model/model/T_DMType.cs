using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 下载类型
    /// </summary>
    [TableName("T_DMType")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_DMType
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("dm_typeid")]
        public string DM_TypeID { get; set; }

        [Column]
        [JsonProperty("dm_typevalue")]
        public string DM_TypeValue { get; set; }
    }
}
