using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using sl.model;

namespace sl.service
{
    public partial class SysModuleService
    {
        public List<T_SysModule> TreeList(Sql where, string sort)
        {
            List<T_SysModule> sortNodes = new List<T_SysModule>();
           // List<T_SysModule> list = List(where, "Sort asc");
            Database DB = new Database("ConnectionString");
            List<T_SysModule> list = DB.Fetch<T_SysModule>(where);
            List<T_SysModule> rootNodes = list.Where(p => p.M_ParentNo == 0).ToList();
            foreach (T_SysModule m in rootNodes)
            {
                GetChildrens(list, m, sortNodes, true);
            }
            return sortNodes;
        }
        private void GetChildrens(List<T_SysModule> nodes, T_SysModule parentNode, List<T_SysModule> sortNodes, bool root)
        {
            List<T_SysModule> chilren = nodes.Where(p => p.M_ParentNo == parentNode.M_ID).ToList();
            parentNode.children = chilren;
            if (root)
                sortNodes.Add(parentNode);
            foreach (T_SysModule m in chilren)
            {
                GetChildrens(nodes, m, sortNodes, false);
            }
        }
    }
}
