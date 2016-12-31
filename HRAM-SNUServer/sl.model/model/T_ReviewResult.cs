using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 审核结果
    /// </summary>
    [TableName("T_ReviewResult")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_ReviewResult
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("m_reviewresultid")]
        public string M_ReviewResultID { get; set; }

        [Column]
        [JsonProperty("m_reviewresultvalue")]
        public string M_ReviewResultValue { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }
    }
}
