﻿using System;
using System.Collections.Generic;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    [TableName("T_SysModule")]
    [PrimaryKey("pk_id")]
    [ExplicitColumns]
    public class T_SysModule
    {
        [Column]
        [JsonProperty("pk_id")]
        public int pk_id { get; set; }

        [ResultColumn]
        public List<T_SysModule> children { get; set; }

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
        public bool M_IsMenu { get; set; }

        [Column]
        public bool IsDeleted { get; set; }

        [Column]
        public string M_Controller { get; set; }

    }
}