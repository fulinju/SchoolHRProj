using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.model
{
    /// <summary>
    /// 多表一起显示，门户网站上很多表都是采用此种发式
    /// </summary>
    public class TypeModels
    {
       public List<T_UserType> userTypeList{get;set;}

       public T_UserType userType { get; set; }

       public List<T_DMType> dmTypeList { get; set; }

       public T_DMType dmType { get; set; }

       public List<T_PMType> pmTypeList { get; set; }

       public T_PMType pmType { get; set; }

       public List<T_ReviewResult> reviewResultList { get; set; }

       public T_ReviewResult reviewResult { get; set; }

       public List<T_MemberType> memberTypeList { get; set; }

       public T_MemberType memberType { get; set; }

       public List<T_FLType> flTypeList { get; set; }

       public T_FLType flType { get; set; }
    }
}
