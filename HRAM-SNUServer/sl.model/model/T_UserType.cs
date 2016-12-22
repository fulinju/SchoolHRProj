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
        [JsonProperty("a_logintypeid")]
        public string A_LoginTypeID { get; set; }

        [Column]
        [JsonProperty("a_logintypevalue")]
        public string A_LoginTypeValue { get; set; }
    }
}
