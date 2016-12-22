using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.model
{
    public class TypeModels
    {
       public List<T_UserType> userTypeList{get;set;}

       public T_UserType userType { get; set; }

       public List<T_DMType> dmTypeList { get; set; }

       public T_DMType dmType { get; set; }

       public List<T_PMType> pmTypeList { get; set; }

       public T_PMType pmType { get; set; }
    }
}
