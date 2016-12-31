using System;
using PetaPoco;
using Newtonsoft.Json;


namespace sl.model
{

    [TableName("T_UserType")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_UserType
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("u_logintypeid")]
        public string U_LoginTypeID { get; set; }

        [Column]
        [JsonProperty("u_logintypevalue")]
        public string U_LoginTypeValue { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }
    }
}
