namespace sl.model
{
    /// <summary>
    /// 下载的信息
    /// </summary>
    public class DownLoadInfo
    {
        public string downloadID { get; set; }//下载的ID

        public string uLoginName { get; set; }//发布者的登录名

        //外层嵌套用户名
        public string uUserName { get; set; }
        public string dmTitle { get; set; }
        public string dmTypeID { get; set; }
        public string dmTypeValue { get; set; }

        public string dmFileURL { get; set; }

        public int dmDownloadNum { get; set; }

        public string dmUploadTime { get; set; }
    }
}
