using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 会员类型
    /// </summary>
    [TableName("T_MemberType")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_MemberType
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("m_typeid")]
        public string M_TypeID { get; set; }

        [Column]
        [JsonProperty("m_typevalue")]
        public string M_TypeValue { get; set; }
    }
}
