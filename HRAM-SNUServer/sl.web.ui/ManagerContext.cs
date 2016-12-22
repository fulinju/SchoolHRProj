using System.Web;
using PetaPoco;
using PetaPoco.Orm;
using sl.IService;
using sl.common;
using sl.model;


namespace sl.web.ui
{
    public class ManagerContext
    {
        public static T_User UserInfo
        {
            get { return GetSessionUserInfo(); }
        }

        private static T_User GetSessionUserInfo()
        {
            T_User manager = HttpContext.Current.Session[Key.MANAGER_INFO] as T_User;
            if (manager == null)
            {
                string username = Utils.GetCookie(Key.MANAGER_NAME);
                string userpass = Utils.GetCookie(Key.MANAGER_PASS);
                if (username != "" || userpass != "")
                {
                    Condition where = Condition.Builder.Equal("A_LoginName", username).Equal("A_Password", Security.MD5Encrypt(userpass));
                    manager = DIContainer.Resolve<ITUserService>().Load(where);
                    if (manager != null)
                    {
                        HttpContext.Current.Session[Key.MANAGER_INFO] = manager;
                        return manager;
                    }
                }
                return null;
            }
            return manager;
        }
    }
}
