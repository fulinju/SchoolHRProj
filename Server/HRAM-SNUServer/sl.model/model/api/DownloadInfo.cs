namespace sl.model
{
    /// <summary>
    /// 下载的信息
    /// </summary>
    public class DownLoadInfo
    {
        //外层嵌套用户名
        public string u_username { get; set; }

        public string dm_title { get; set; }

        public string dm_typevalue { get; set; }

        public string dm_fileurl { get; set; }

        public int dm_downloadnum { get; set; }

        public string dm_uploadtime { get; set; }
    }
}
