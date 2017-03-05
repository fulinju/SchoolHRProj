using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using sl.model;

namespace sl.service
{
    public partial class SysModuleService
    {
        //树状结构的根节点
        public List<T_SysModule> TreeList(Database DB,Sql sql,int rootNo)
        {
            List<T_SysModule> sortNodes = new List<T_SysModule>();
            List<T_SysModule> list = DB.Fetch<T_SysModule>(sql);
            List<T_SysModule> rootNodes = list.Where(p => p.mParentNo == rootNo).ToList();
            foreach (T_SysModule m in rootNodes)
            {
                GetChildrens(list, m, sortNodes, true);
            }
            return sortNodes;
        }

        //获取树状结构的子集
        private void GetChildrens(List<T_SysModule> nodes, T_SysModule parentNode, List<T_SysModule> sortNodes, bool root)
        {
            List<T_SysModule> chilren = nodes.Where(p => p.mParentNo == parentNode.pkId).ToList();
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
