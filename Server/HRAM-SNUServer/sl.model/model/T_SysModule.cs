using System;
using System.Collections.Generic;
using PetaPoco;
using Newtonsoft.Json;

namespace sl.model
{
    [TableName("T_SysModule")]
    [PrimaryKey("pkId")]
    [ExplicitColumns]
    public class T_SysModule
    {
        [Column]
        [JsonProperty("pkId")]
        public int pkId { get; set; }

        [ResultColumn]
        public List<T_SysModule> children { get; set; }

        [Column]
        public string mName { get; set; }

        [Column]
        public string mLinkUrl { get; set; }

        [Column]
        public string mIcon { get; set; }

        [Column]
        public int? mParentNo { get; set; }

        [Column]
        public int? mSort { get; set; }

        [Column]
        public bool mIsVisible { get; set; }

        [Column]
        public bool mIsMenu { get; set; }

        [Column]
        public bool isDeleted { get; set; }

        [Column]
        public string mController { get; set; }

    }
}
