using System;
using PetaPoco;
using Newtonsoft.Json;


namespace sl.model
{

    [TableName("T_UserType")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_UserType
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("uLoginTypeID")]
        public string uLoginTypeID { get; set; }

        [Column]
        [JsonProperty("uLoginTypeValue")]
        public string uLoginTypeValue { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
}
