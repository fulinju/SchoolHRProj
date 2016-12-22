using System;
using System.Collections.Generic;
using PetaPoco;

namespace sl.model
{
    [TableName("T_SysModule")]
    [PrimaryKey("ModuleID")]
    [ExplicitColumns]
    public class T_SysModule
    {
        [ResultColumn]
        public List<T_SysModule> children { get; set; }

        [Column]
        public int ModuleID { get; set; }

        [Column]
        public string ModuleName { get; set; }

        [Column]
        public string ModuleLinkUrl { get; set; }

        [Column]
        public string ModuleIcon { get; set; }

        [Column]
        public int? ParentNo { get; set; }

        [Column]
        public int? Sort { get; set; }

        [Column]
        public bool IsVisible { get; set; }

        [Column]
        public bool? IsSingle { get; set; }

        [Column]
        public bool IsMenu { get; set; }

        [Column]
        public bool IsDeleted { get; set; }

        [Column]
        public string ModuleController { get; set; }

    }
}
