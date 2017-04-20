using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 友情链接
    /// </summary>
    [TableName("T_FriendlyLink")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_FriendlyLink
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("uLoginName")]
        public string uLoginName { get; set; }

        [Column]
        [JsonProperty("flName")]
        public string flName { get; set; }


        [Column]
        [JsonProperty("flTypeID")]
        public string flTypeID { get; set; }

        [Column]
        [JsonProperty("flURL")]
        public string flURL { get; set; }

        [Column]
        [JsonProperty("flImgURL")]
        public string flImgURL { get; set; }

        [Column]
        [JsonProperty("flAddTime")]
        public string flAddTime { get; set; }

        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
}
