using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sl.extension
{
    public class LinkButton
    {
        public string Dgid { get; set; }
        public string Icon { get; set; }
        public bool Plain { get; set; }
        public bool Disabled { get; set; }
        public string OnClick { get; set; }

        public LinkButton(string icon = "", bool plain = true)
        {
            Dgid = "";
            Icon = icon;
            Plain = plain;
            Disabled = false;
            OnClick = "";
        }
    }
}
