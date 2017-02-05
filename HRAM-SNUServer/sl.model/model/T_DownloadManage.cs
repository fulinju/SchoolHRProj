using System;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model.model
{
    /// <summary>
    /// 下载管理
    /// </summary>
    /// 
    [TableName("T_DownloadManage")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_DownloadManage
    {
        [Column]
        [JsonProperty("pk_id")]
        public string pk_id { get; set; }

        [Column]
        [JsonProperty("u_loginname")]
        public string U_LoginName { get; set; }

        [Column]
        [JsonProperty("dm_title")]
        public string DM_Title { get; set; }

        [Column]
        [JsonProperty("dm_typeid")]
        public string DM_TypeID { get; set; }

        [Column]
        [JsonProperty("dm_fileurl")]
        public string DM_FileURL { get; set; }

        [Column]
        [JsonProperty("dm_downloadnum")]
        public string DM_DownloadNum { get; set; }

        [Column]
        [JsonProperty("dm_uploadtime")]
        public string DM_UploadTime { get; set; }

        [Column]
        [JsonProperty("isdeleted")]
        public bool IsDeleted { get; set; }
    }
}
