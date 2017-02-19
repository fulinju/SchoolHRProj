using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PetaPoco;
using Newtonsoft.Json;
using System.IO;
using sl.common;
using sl.model;
using sl.service.manager;


namespace sl.web
{
    /// <summary>
    /// 用于DropList的绑定数据
    /// </summary>
    public class UtilsBind
    {
        #region 绑定账户类型
        public static List<SelectListItem> UserTypes()
        {
            List<T_UserType> userTypes = HRAManagerService.GetUserTypeList();
            List<SelectListItem> list = new List<SelectListItem>();
            if (userTypes != null)
            {
                for (int i = 0; i < userTypes.Count; i++)
                {
                    list.Add(new SelectListItem { Text = userTypes[i].U_LoginTypeValue, Value = userTypes[i].U_LoginTypeID });

                }
            }
            else
            {
                list.Add(new SelectListItem { Text = ConstantData.ErrorMsg, Value = ""+ ConstantData.ErrorCode });
            }

            return list;
        }
        #endregion

        #region 绑定审核结果类型
        public static List<SelectListItem> ReviewResults()
        {
            List<T_ReviewResult> reviewResults = HRAManagerService.GetReviewResults();
            List<SelectListItem> list = new List<SelectListItem>();
            if (reviewResults != null)
            {
                for (int i = 0; i < reviewResults.Count; i++)
                {
                    list.Add(new SelectListItem { Text = reviewResults[i].M_ReviewResultValue, Value = reviewResults[i].M_ReviewResultID });

                }
            }
            else
            {
                list.Add(new SelectListItem { Text = ConstantData.ErrorMsg, Value = "" + ConstantData.ErrorCode });
            }

            return list;
        }
        #endregion

        #region 绑定是否操作
        public static List<SelectListItem> ChooseYesOrNo()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text ="否", Value = "0" });
            list.Add(new SelectListItem { Text = "是", Value = "1" });

            return list;
        }
        #endregion

        #region 绑定会员类型
        public static List<SelectListItem> MemberTypes()
        {
            List<T_MemberType> memberTypes = HRAManagerService.GetMemberTypes();
            List<SelectListItem> list = new List<SelectListItem>();
            if (memberTypes != null)
            {
                for (int i = 0; i < memberTypes.Count; i++)
                {
                    list.Add(new SelectListItem { Text = memberTypes[i].M_TypeValue, Value = memberTypes[i].M_TypeID });

                }
            }
            else
            {
                list.Add(new SelectListItem { Text = ConstantData.ErrorMsg, Value = "" + ConstantData.ErrorCode });
            }

            return list;
        }
        #endregion


        #region 绑定下载类型
        public static List<SelectListItem> DownloadTypes()
        {
            List<T_DMType> types = HRAManagerService.GetDownloadTypeList();
            List<SelectListItem> list = new List<SelectListItem>();
            if (types != null)
            {
                for (int i = 0; i < types.Count; i++)
                {
                    list.Add(new SelectListItem { Text = types[i].DM_TypeValue, Value = types[i].DM_TypeID });

                }
            }
            else
            {
                list.Add(new SelectListItem { Text = ConstantData.ErrorMsg, Value = "" + ConstantData.ErrorCode });
            }

            return list;
        }
        #endregion

        #region 绑定控制器名称
        public static List<SelectListItem> ControllerNames()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "无控制器", Value = "" }); //无控制器

            var findPath = System.Web.HttpContext.Current.Server.MapPath("~/Areas/Manager/Controllers"); //Controller的路径

            var files = DirFile.GetFileNames(findPath, "*.cs", false); //不搜索子目录

            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    list.Add(new SelectListItem { Text = DirFile.GetFileName(files[i]), Value = DirFile.GetFileName(files[i]) }); //截取名称带扩展名，Value也不需要绝对路径
                }
            }
            else
            {
                list.Add(new SelectListItem { Text = ConstantData.ErrorMsg, Value = "" + ConstantData.ErrorCode });
            }

            return list;
        }
        #endregion

        #region 绑定View
        public static List<SelectListItem> ViewNames()
        {
            List<SelectListItem> list = new List<SelectListItem>(); //列表选项
            list.Add(new SelectListItem { Text = "无视图", Value = "" }); //无控制器


            var findPath = System.Web.HttpContext.Current.Server.MapPath("~/Areas/Manager/Views"); //Controller的路径

            var dirs = DirFile.GetDirectories(findPath); //VIews下的子目录 不继续搜索

            if (dirs != null)
            {
                for (int i = 0; i < dirs.Length; i++)
                {
                    var childrenDir = DirFile.GetFileName(dirs[i]);
                    var files = DirFile.GetFileNames(findPath + '/' + childrenDir, "*.cshtml", false);
                     if (files != null)
                     {
                         for (int j = 0; j < files.Length; j++)
                         {
                             list.Add(new SelectListItem { Text = DirFile.GetFileName(files[j]), Value = "/Manager/" + childrenDir + '/' + DirFile.GetFileNameNoExtension(files[j]) });//和JS不同,''里面只能有一个字符   值不含扩展名
                         }
                     }
                }
            }
            else
            {
                list.Add(new SelectListItem { Text = ConstantData.ErrorMsg, Value = "" + ConstantData.ErrorCode });
            }          

            return list;
        }
        #endregion
    }
}