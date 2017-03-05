using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 审核结果
    /// </summary>
    [TableName("T_ReviewResult")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_ReviewResult
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("mReviewResultID")]
        public string mReviewResultID { get; set; }

        [Column]
        [JsonProperty("mReviewResultValue")]
        public string mReviewResultValue { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
}
