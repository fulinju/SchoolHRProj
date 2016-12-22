using System.Collections.Generic;
using PetaPoco;
using sl.model;

namespace sl.IService
{
    public partial interface ISysModuleService
    {
        List<T_SysModule> TreeList(Sql where, string sort);
    }
}
