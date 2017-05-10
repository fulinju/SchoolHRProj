using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.model
{
    public class ModifyPasswordModel
    {
        public string uID { get; set; }
        public string uLoginStr { get; set; }
        public string uOldPassword { get; set; }
        public string uNewPassword { get; set; }
        public string uClientKey { get; set; }
    }
}
