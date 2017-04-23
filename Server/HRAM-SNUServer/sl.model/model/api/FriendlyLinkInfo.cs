using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.model
{
    public class FriendlyLinkInfo
    {
        public string friendlyLinkID { get; set; }//下载的ID

        public string uLoginName { get; set; }//发布者的登录名

        public string uUserName { get; set; }   //外层嵌套用户名

        public string flTypeValue { get; set; }

        public string flName { get; set; }

        public string flURL { get; set; }

        public string flImgURL { get; set; }

        public string flAddTime { get; set; }

        public bool isDeleted { get; set; }


    }
}
