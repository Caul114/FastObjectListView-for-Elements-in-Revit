using FastObjectListViewProva.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastObjectListViewProva
{
    public partial class FastObjectListViewProvaWF : Form
    {
        public FastObjectListViewProvaWF()
        {
            InitializeComponent();
        }

        public FastObjectListViewProvaWF(Command dataBuffer)
        {
            InitializeComponent();

            List<SingleData> data = dataBuffer.Elements;
            fastObjectListView1.SetObjects(data);
        }
    }
}
