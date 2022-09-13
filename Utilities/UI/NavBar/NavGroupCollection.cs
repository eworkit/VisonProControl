using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.UI
{
    public class NavGroupCollection :List<NavGroup>
    {
        private NavBar _ownerBar;
        public NavGroupCollection(NavBar ownerBar):base()
        {
            this._ownerBar = ownerBar;
        }
        public new void Add(NavGroup item)
        {
            this._ownerBar.SetLayOut(item);
            base.Add(item);
            item.GroupIndex = this.Count - 1;
            item.SmallImageList = this._ownerBar.SmallImageList;
        }
        public new void Add()
        {
            NavGroup item = new NavGroup(this._ownerBar);
            this.Add(item);
        }

    }
}
