using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    [TableName("T_FLType")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_FLType
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("flTypeID")]
        public string flTypeID { get; set; }

        [Column]
        [JsonProperty("flTypeValue")]
        public string flTypeValue { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
}
