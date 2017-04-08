using System.Collections.Generic;
namespace sl.model
{
    /// <summary>
    /// 发布的条目
    /// </summary>
    public class PublishInfo
    {
        public string pkId { get; set; }//发布者的id

        public string uUserName { get; set; }//发布者的用户名

        public string pmTitle { get; set; }

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
