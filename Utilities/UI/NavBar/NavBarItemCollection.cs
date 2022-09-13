using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.UI
{
    public class NavBarItemCollection : List<NavBarItem>
    {
        private NavGroup _ownerGroup;
        
        public NavBarItemCollection(NavGroup ownerGroup):base()
        {
            this._ownerGroup = ownerGroup;
        }
        public new void Add(NavBarItem item)
        {
            this._ownerGroup.SetLayOut(item);
            base.Add(item);
            item.ItemIndex = this.Count - 1;
        }
        public new void Add()
        {
            NavBarItem item = new NavBarItem(this._ownerGroup);
            this.Add(item);            
        }
    }
}
