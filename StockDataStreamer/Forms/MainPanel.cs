using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Crom.Controls.Docking;
using StockDataStreamer.DBLayer;

namespace StockDataStreamer.Forms
{
    public partial class MainPanel : Form, IDockableForm
    {
        private DockableFormInfo m_dFormInfo;
        private Guid m_id;
       public  DockableFormInfo FormInfo
        {
            get
            {
                return m_dFormInfo;
            }
            set
            {
                m_dFormInfo = value;
            }
        }

        public Guid FormID{ 
            get
            {
                 return m_id ;

            } 
        }


        public event EventHandler<GraphRequestedEventArgs> GraphRequested;

        private void OnGraphRequested(GraphRequestedEventArgs graphRequestedEventArgs)
        {
            if (GraphRequested != null)
                GraphRequested(this, graphRequestedEventArgs);
            
            
        }

        public MainPanel()
        {
            m_id = Guid.NewGuid();
           InitializeComponent();
        }

        private void MainPanel_Load(object sender, EventArgs e)
        {
            PopulateStocksList();
        }

        private void PopulateStocksList()
        {
            List<Pair<int, string>> values = StockDBConnection.Connection.Securities;
            foreach (Pair<int, string> pair in values)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(pair.First.ToString());
                lvi.SubItems.Add(pair.Second);
                lvi.Tag = pair;
                listView1.Items.Add(lvi);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(listView1.SelectedItems.Count == 0)
                return;
            Pair<int, string> item = listView1.SelectedItems[0].Tag as Pair<int, string>;
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.Subtract(new TimeSpan(365, 0, 0, 0));
            OnGraphRequested(new GraphRequestedEventArgs(item, startDate, endDate));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            Pair<int, string> item = listView1.SelectedItems[0].Tag as Pair<int, string>;
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.Subtract(new TimeSpan(7   , 0, 0, 0));
            OnGraphRequested(new GraphRequestedEventArgs(item, startDate, endDate));
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            Pair<int, string> item = listView1.SelectedItems[0].Tag as Pair<int, string>;
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
            OnGraphRequested(new GraphRequestedEventArgs(item, startDate, endDate));
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            Pair<int, string> item = listView1.SelectedItems[0].Tag as Pair<int, string>;
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.MinValue;
            OnGraphRequested(new GraphRequestedEventArgs(item, startDate, endDate));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            Pair<int, string> item = listView1.SelectedItems[0].Tag as Pair<int, string>;
            DateTime endDate = toTimePicker.Value;
            DateTime startDate = fromTimePicker.Value;
            OnGraphRequested(new GraphRequestedEventArgs(item, startDate, endDate));
        }
     }

    public class GraphRequestedEventArgs : EventArgs
    {
        private Pair<int, string> m_item;
        private DateTime m_startDate;
        private DateTime m_endDate;

        public GraphRequestedEventArgs(Pair<int, string> item, DateTime startDate, DateTime endDate)
        {
            m_item = item;
            m_startDate = startDate;
            m_endDate = endDate;

        }

        public DateTime EndDate
        {
            get { return m_endDate; }
        }

        public DateTime StartDate
        {
            get { return m_startDate; }
        }

        public Pair<int, string> Item
        {
            get { return m_item; }
        }
    }
}
