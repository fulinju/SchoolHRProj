using PetaPoco.Orm;
using sl.model;
using sl.IService;

namespace sl.service
{
    public partial class SysModuleService : BaseDao<T_SysModule>, ISysModuleService { }

    public partial class TUserService : BaseDao<T_User>, ITUserService { }

    public partial class TMemberService : BaseDao<T_Member>, ITMemberService { }

    public partial class TUserTypeService : BaseDao<T_UserType>, ITUserTypeService { }

    public partial class TDMTypeService : BaseDao<T_DMType>, ITDMTypeService { }

    public partial class TPMTypeService : BaseDao<T_PMType>, ITPMTypeService { }

    public partial class TReviewResultService : BaseDao<T_ReviewResult>, ITReviewResultService { }

    public partial class TMemberTypeService : BaseDao<T_MemberType>, ITMemberTypeService { }
}
