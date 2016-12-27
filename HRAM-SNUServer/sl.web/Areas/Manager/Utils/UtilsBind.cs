using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaPoco;
using sl.model;

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
            List<T_UserType> userTypes = UtilsDB.GetUserTypeList();
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
            List<T_ReviewResult> reviewResults = UtilsDB.GetReviewResults();
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

        #region 绑定会员类型
        public static List<SelectListItem> MemberTypes()
        {
            List<T_MemberType> memberTypes = UtilsDB.GetMemberTypes();
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
    }
}