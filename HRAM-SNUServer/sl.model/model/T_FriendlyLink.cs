using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 友情链接
    /// </summary>
    [TableName("T_FriendlyLink")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_FriendlyLink
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("u_loginname")]
        public string U_LoginName { get; set; }

        [Column]
        [JsonProperty("fl_name")]
        public string FL_Name { get; set; }

        [Column]
        [JsonProperty("fl_url")]
        public string FL_URL { get; set; }

        [Column]
        [JsonProperty("fl_imgurl")]
        public string FL_ImgURL { get; set; }

        [Column]
        [JsonProperty("fl_addtime")]
        public string FL_AddTime { get; set; }

        [Column]
        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }
    }
}
