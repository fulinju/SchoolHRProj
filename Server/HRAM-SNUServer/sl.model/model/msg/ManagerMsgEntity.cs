using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.model.err
{
    public class ManagerMsgEntity
    {
        public int code { get; set; }
        public string message { get; set; }

        public ManagerMsgEntity(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public ManagerMsgEntity(int code)
        {
            this.code = code;

            switch (code)
            {
                #region 通用
                case 10000:
                    message = "保存成功";
                    break;

                case 10001:
                    message = "保存失败";
                    break;

                case 10002:
                    message = "删除成功";
                    break;

                case 10003:
                    message = "删除失败";
                    break;

                case 10004:
                    message = "上传成功";
                    break;

                case 10005:
                    message = "上传失败";
                    break;

                #endregion

                #region 下载相关
                #endregion

                #region 友情链接相关
                #endregion

                #region 账户相关相关
                #endregion

                #region 会员相关
                #endregion

                #region 系统设置相关
                #endregion

                #region 类型管理相关
                #endregion

                #region 主页面相关
                #endregion
                default:
                    message = "未知错误";
                    break;
            }
        }
    }
}
