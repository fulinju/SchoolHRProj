using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.model
{
    public class PublishExportInfo
    {
        public string uUserName { get; set; }//发布者的用户名

        public string pmTitle { get; set; }

        public string pmTypeValue { get; set; }//发布的类型

        public string pmADImgListID { get; set; } //广告图片

        public string pmPublishTime { get; set; }

        public string pmViews { get; set; }

        public string pmText { get; set; }
    }
}
