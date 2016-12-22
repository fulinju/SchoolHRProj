using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    [TableName("T_User")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_User
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("a_loginname")]
        public string A_LoginName { get; set; }

        [Column]
        [JsonProperty("a_logintypeid")]
        public string A_LoginTypeID { get; set; }

        [Column]
        [JsonProperty("a_password")]
        public string A_Password { get; set; }

        [Column]
        [JsonProperty("a_username")]
        public string A_UserName { get; set; }

        [Column]
        [JsonProperty("a_phone")]
        public string A_Phone { get; set; }

        [Column]
        [JsonProperty("a_maibox")]
        public string A_MaiBox { get; set; }

        //忽略该属性
        //[JsonIgnore]
        //public string Check_Code { get; set; }
    }
}
