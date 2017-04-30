using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.model
{
    public class UserModel
    {
        public string uID { get; set; }
        public string uLoginStr { get; set; }
        //public string uPassword { get; set; }
        public string uClientKey { get; set; }
        public string uUserName { get; set; }
        public string uPhone { get; set; }
        public string uMaiBox { get; set; }
        public string uTokenActiveTime { get; set; }
        public string uTokenExpiredTime { get; set; }
        public string uToken{ get; set; }
    }
}
