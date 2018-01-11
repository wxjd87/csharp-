using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfKarliCards_GUI
{
    class numberOfPlayers:ObservableCollection<int>
    {
        public numberOfPlayers():base()
        {
            Add(2);
            Add(3);
            Add(4);
        }
    }
}
