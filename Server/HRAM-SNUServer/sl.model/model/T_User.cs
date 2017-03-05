using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    [TableName("T_User")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_User
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("uLoginName")]
        public string uLoginName { get; set; }

        [Column]
        [JsonProperty("uLoginTypeID")]
        public string uLoginTypeID { get; set; }

        [Column]
        [JsonProperty("uPassword")]
        public string uPassword { get; set; }

        [Column]
        [JsonProperty("uUserName")]
        public string uUserName { get; set; }

        [Column]
        [JsonProperty("uPhone")]
        public string uPhone { get; set; }

        [Column]
        [JsonProperty("uMaiBox")]
        public string uMaiBox { get; set; }

        [JsonIgnore]
        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }

        //忽略该属性
        //[JsonIgnore]
        //public string Check_Code { get; set; }
    }
}
