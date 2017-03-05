using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    /// <summary>
    /// 下载管理
    /// </summary>
    /// 
    [TableName("T_DownloadManage")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_DownloadManage
    {
        [Column]
        [JsonProperty("pkId")]
        public string pkId { get; set; }

        [Column]
        [JsonProperty("uLoginName")]
        public string uLoginName { get; set; }

        [Column]
        [JsonProperty("dmTitle")]
        public string dmTitle { get; set; }

        [Column]
        [JsonProperty("dmTypeID")]
        public string dmTypeID { get; set; }

        [Column]
        [JsonProperty("dmFileURL")]
        public string dmFileURL { get; set; }

        [Column]
        [JsonProperty("dmDownloadNum")]
        public int dmDownloadNum { get; set; }

        [Column]
        [JsonProperty("dmUploadTime")]
        public string dmUploadTime { get; set; }

        [Column]
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }
    }
}
