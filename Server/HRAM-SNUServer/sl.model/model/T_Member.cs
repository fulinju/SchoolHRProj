using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 会员
    /// </summary>
    [TableName("T_Member")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_Member
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("uLoginName")]
        public string uLoginName { get; set; }

        [Column]
        [JsonProperty("mReviewResultID")]
        public string mReviewResultID { get; set; }

        [Column]
        [JsonProperty("mApplyTime")]
        public string mApplyTime { get; set; }

        [Column]
        [JsonProperty("mName")]
        public string mName { get; set; }

        [Column]
        [JsonProperty("mtypeid")]
        public string mTypeID { get; set; }

        [Column]
        [JsonProperty("mOrganizationCode")]
        public string mOrganizationCode { get; set; }

        [Column]
        [JsonProperty("mAddress")]
        public string mAddress { get; set; }

        [Column]
        [JsonProperty("mCorporateName")]
        public string mCorporateName { get; set; }

        [Column]
        [JsonProperty("mIDCardNo")]
        public string mIDCardNo { get; set; }

        [Column]
        [JsonProperty("mContacts")]
        public string mContacts { get; set; }

        [Column]
        [JsonProperty("mContactsPhone")]
        public string mContactsPhone { get; set; }

        [Column]
        [JsonProperty("mSummary")]
        public string mSummary { get; set; }

        [Column]
        [JsonProperty("mImgURL")]
        public string mImgURL { get; set; }

        [Column]
        [JsonProperty("mURL")]
        public string mURL { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
}
