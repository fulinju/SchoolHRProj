using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.model
{
    /// <summary>
    /// Api消息返回
    /// </summary>
    public class ApiMsgEntity
    {
        public int code { get; set; }
        public string message { get; set; }

        public ApiMsgEntity(int code)
        {
            this.code = code;

            switch (code)
            {
                #region 账户

                case 10000:
                    {
                        message = "注册失败";
                    }
                    break;

                case 10001:
                    {
                        message = "该用户名已被注册";
                    }
                    break;

                case 10002:
                    {
                        message = "该邮箱已被注册";
                    }
                    break;
                
                #endregion
            }
        }
    }
}
