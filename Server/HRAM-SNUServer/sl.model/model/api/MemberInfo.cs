using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.model
{
    public class MemberInfo
    {
        public string memberID { get; set; }//发布文章的ID
        public string uLoginName { get; set; }//发布者的登录名

        //外层嵌套用户名
        public string uUserName { get; set; }
        public string mReviewResultID { get; set; }
        public string mReviewResultValue { get; set; }
        public string mApplyTime { get; set; }
        public string mName { get; set; }
        public string mTypeID { get; set; }
        public string mTypeValue { get; set; }
        public string mOrganizationCode { get; set; }
        public string mAddress { get; set; }
        public string mCorporateName { get; set; }
        public string mIDCardNo { get; set; }
        public string mContacts { get; set; }
        public string mContactsPhone { get; set; }
        public string mSummary { get; set; }
        public string mImgURL { get; set; }
        public string mURL { get; set; }
        public bool isDeleted { get; set; }


    }
}
