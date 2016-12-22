using PetaPoco.Orm;
using sl.model;

namespace sl.IService
{
    public partial interface ISysModuleService : IBaseDao<T_SysModule> { }

    public partial interface ITUserService : IBaseDao<T_User> { }

    public partial interface ITMemberService : IBaseDao<T_Member> { }

    public partial interface ITUserTypeService : IBaseDao<T_UserType> { }

    public partial interface ITDMTypeService : IBaseDao<T_DMType> { }

    public partial interface ITPMTypeService : IBaseDao<T_PMType> { }

}
