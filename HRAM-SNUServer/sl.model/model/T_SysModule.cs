using System;
using System.Collections.Generic;
using PetaPoco;

namespace sl.model
{
    [TableName("T_SysModule")]
    [PrimaryKey("M_ID")]
    [ExplicitColumns]
    public class T_SysModule
    {
        [ResultColumn]
        public List<T_SysModule> children { get; set; }

        [Column]
        public int M_ID { get; set; }

        [Column]
        public string M_Name { get; set; }

        [Column]
        public string M_LinkUrl { get; set; }

        [Column]
        public string M_Icon { get; set; }

        [Column]
        public int? M_ParentNo { get; set; }

        [Column]
        public int? M_Sort { get; set; }

        [Column]
        public bool M_IsVisible { get; set; }

        [Column]
        public bool? M_IsSingle { get; set; }

        [Column]
        public bool M_IsMenu { get; set; }

        [Column]
        public bool M_IsDeleted { get; set; }

        [Column]
        public string M_Controller { get; set; }

    }
}
