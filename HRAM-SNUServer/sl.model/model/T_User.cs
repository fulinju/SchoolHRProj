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
        [JsonProperty("u_loginname")]
        public string U_LoginName { get; set; }

        [Column]
        [JsonProperty("u_logintypeid")]
        public string U_LoginTypeID { get; set; }

        [Column]
        [JsonProperty("u_password")]
        public string U_Password { get; set; }

        [Column]
        [JsonProperty("u_username")]
        public string U_UserName { get; set; }

        [Column]
        [JsonProperty("u_phone")]
        public string U_Phone { get; set; }

        [Column]
        [JsonProperty("u_maibox")]
        public string U_MaiBox { get; set; }

        //忽略该属性
        //[JsonIgnore]
        //public string Check_Code { get; set; }
    }
}
