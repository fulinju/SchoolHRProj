using System.Collections.Generic;
namespace sl.model
{
    /// <summary>
    /// 发布的条目
    /// </summary>
    public class PublishInfo
    {
        public string publishID { get; set; }//发布文章的ID

        public string uLoginName { get; set; }//发布者的登录名

        public string uUserName { get; set; }//发布者的用户名

        public string pmTitle { get; set; }

        public string pmTypeID { get; set; }
        public string pmTypeValue { get; set; }//发布的类型

        public string pmADImgListID { get; set; } //广告图片

        public List<ADImgInfo> pmADImgList { get; set; }

        //public string pmADImgListNum { get; set; }

        //public string pmADImgListURL { get; set; }

        public string pmPublishTime { get; set; }

        public string pmViews { get; set; }

        public string pmText { get; set; }
    }
}
