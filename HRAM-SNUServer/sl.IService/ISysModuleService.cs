using System.Collections.Generic;
using PetaPoco;
using sl.model;

namespace sl.IService
{
    public partial interface ISysModuleService
    {
        List<T_SysModule> TreeList(Database DB, Sql sql,int rootNo);
    }
}
