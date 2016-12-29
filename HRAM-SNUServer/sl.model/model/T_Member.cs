using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 会员
    /// </summary>
    [TableName("T_Member")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_Member
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("u_loginname")]
        public string U_LoginName { get; set; }

        [Column]
        [JsonProperty("m_reviewresultid")]
        public string M_ReviewResultID { get; set; }

        [Column]
        [JsonProperty("m_applytime")]
        public string M_ApplyTime { get; set; }

        [Column]
        [JsonProperty("m_name")]
        public string M_Name { get; set; }

        [Column]
        [JsonProperty("m_typeid")]
        public string M_TypeID { get; set; }

        [Column]
        [JsonProperty("m_organizationcode")]
        public string M_OrganizationCode { get; set; }

        [Column]
        [JsonProperty("m_address")]
        public string M_Address { get; set; }

        [Column]
        [JsonProperty("m_corporatename")]
        public string M_CorporateName { get; set; }

        [Column]
        [JsonProperty("m_idcardno")]
        public string M_IDCardNo { get; set; }

        [Column]
        [JsonProperty("m_contacts")]
        public string M_Contacts { get; set; }

        [Column]
        [JsonProperty("m_contactsphone")]
        public string M_ContactsPhone { get; set; }

        [Column]
        [JsonProperty("m_summary")]
        public string M_Summary { get; set; }

        [Column]
        [JsonProperty("m_imgurl")]
        public string M_ImgURL { get; set; }

        [Column]
        [JsonProperty("m_url")]
        public string M_URL { get; set; }
    }
}
