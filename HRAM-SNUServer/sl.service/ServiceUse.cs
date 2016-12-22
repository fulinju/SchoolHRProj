using PetaPoco.Orm;
using sl.model;
using sl.IService;

namespace sl.service
{
    public partial class SysModuleService : BaseDao<T_SysModule>, ISysModuleService
    {

    }

    public partial class TUserService : BaseDao<T_User>, ITUserService
    {

    }
}
