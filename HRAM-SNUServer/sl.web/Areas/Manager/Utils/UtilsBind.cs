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
                    list.Add(new SelectListItem { Text = userTypes[i].A_LoginTypeValue, Value = userTypes[i].A_LoginTypeID });

                }
            }
            else
            {
                list.Add(new SelectListItem { Text = ConstantData.ErrorMsg, Value = ""+ ConstantData.ErrorCode });
            }

            return list;
        }
        #endregion
    }
}