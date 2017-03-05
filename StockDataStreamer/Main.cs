using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StockDataStreamer.Properties;
using Crom.Controls.Docking;
using StockDataStreamer.Forms;
using StockDataStreamer.DBLayer;

namespace StockDataStreamer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                mainPanel.FormInfo = dockContainer1.Add(mainPanel, zAllowedDock.Left, mainPanel.FormID);
                mainPanel.GraphRequested += mainPanel_GraphRequested;
                dockContainer1.DockForm(mainPanel.FormInfo, DockStyle.Left, zDockMode.Inner);
                algsPanel.FormInfo = dockContainer1.Add(algsPanel, zAllowedDock.Left, algsPanel.FormID);
                algsPanel.AlgGraphRequested += algsPanel_AlgGraphRequested;
                dockContainer1.DockForm(algsPanel.FormInfo, mainPanel.FormInfo, DockStyle.Left, zDockMode.Inner);
            }
        }

        void mainPanel_GraphRequested(object sender, GraphRequestedEventArgs e)
        {
            AddGraph(e.Item, e.StartDate, e.EndDate);
        }
        void algsPanel_AlgGraphRequested(object sender, AlgGraphRequestedEventArgs e)
        {
            List<Pair<DateTime, double>> values = new List<Pair<DateTime, double>>();
            for (int i = 0; i < e.Data.Length; i++)
                values.Add(new Pair<DateTime, double>(e.Dates[i], e.Data[i]));
            AddAlgGraph(values, e.Name);
        }


        private void downloadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManualDataDl mdd = new ManualDataDl();
            mdd.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(Settings.Default.LastDataUpdate).Days > 0)
            {
                StocksDataRetreiver st = new StocksDataRetreiver(Settings.Default.LastDataUpdate, DateTime.Now,FileTypes.All);
                st.Completed += st_Completed;
                st.Start();
            }
        }

        void st_Completed(object sender, ExThreadEventArgs e)
        {
            MessageBox.Show("Stock Data Updated");
            Settings.Default.LastDataUpdate = DateTime.Now;
            Settings.Default.Save();

        }

        private void AddGraph(Pair<int, string> item, DateTime startDate, DateTime endDate)
        {
            BasicGraphPanel panel = new BasicGraphPanel();
            panel.FormInfo = dockContainer1.Add(panel, zAllowedDock.All, mainPanel.FormID);
            dockContainer1.DockForm(mainPanel.FormInfo, DockStyle.Left, zDockMode.None);
            List<Pair<DateTime,double>> values = StockDBConnection.Connection.SValuesForDates(item.First, startDate, endDate);
            panel.StockName = item.Second;
            panel.Values = values;
        }

        private void AddAlgGraph(List<Pair<DateTime, double>> values , string name)
        {
            BasicGraphPanel panel = new BasicGraphPanel();
            panel.FormInfo = dockContainer1.Add(panel, zAllowedDock.All, mainPanel.FormID);
            dockContainer1.DockForm(mainPanel.FormInfo, DockStyle.Left, zDockMode.None);
            panel.StockName = name;
            panel.Values = values;

        }

    }
}
