namespace sl.model
{
    /// <summary>
    /// 发布的条目
    /// </summary>
    public class PublishInfo
    { 
        public string uUserName { get; set; }//发布者的用户名

        public string pmTitle { get; set; }

        public string pmTypeValue { get; set; }//发布的类型

        public string pmADImgListID { get; set; }

        public string pmPublishTime { get; set; }

        public string pmViews { get; set; }

        public string pmText { get; set; }
    }
}
