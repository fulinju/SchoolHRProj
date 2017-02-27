namespace sl.model
{
    /// <summary>
    /// 发布的条目
    /// </summary>
    public class PublishInfo
    { 
        public string u_username { get; set; }//发布者的用户名

        public string pm_title { get; set; }

        public string pm_typevalue { get; set; }//发布的类型

        public string pm_adimglistid { get; set; }

        public string pm_publishtime { get; set; }

        public string pm_views { get; set; }

        public string pm_text { get; set; }
    }
}
