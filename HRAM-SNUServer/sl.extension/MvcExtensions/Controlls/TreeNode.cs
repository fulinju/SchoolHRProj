using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.extension.MvcExtensions.Controlls
{
    /// <summary>
    /// 模块树的EasyUi构造
    /// </summary>
    public class TreeNode
    {
        public int id { get; set; }
        public string text { get; set; }
        public List<TreeNode> children { get; set; }
        public int? Pid { get; set; }
        public string iconCls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rootID"></param>
        /// <returns></returns>
        public static List<TreeNode> CreateTree(List<TreeNode> list,int rootID)
        {
            List<TreeNode> sortNodes = new List<TreeNode>();
            List<TreeNode> rootNodes = list.Where(p => p.Pid == rootID).ToList(); 
            foreach (TreeNode treeNode in rootNodes)
            {
                GetChildrens(list, treeNode, sortNodes, true);
            }
            return sortNodes;
        }
        private static void GetChildrens(List<TreeNode> nodes, TreeNode parentNode, List<TreeNode> sortNodes, bool root)
        {
            List<TreeNode> chilren = nodes.Where(p => p.Pid == parentNode.id).ToList();
            parentNode.children = chilren;
            if (root)
                sortNodes.Add(parentNode);
            foreach (TreeNode m in chilren)
            {
                GetChildrens(nodes, m, sortNodes, false);
            }
        }
    }
}
