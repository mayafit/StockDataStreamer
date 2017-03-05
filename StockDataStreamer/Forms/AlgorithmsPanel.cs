using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;
using StockDataStreamer.DBLayer;
using StockDataStreamer.Algorithms;

namespace StockDataStreamer.Forms
{
    public partial class AlgorithmsPanel : Form, IDockableForm
    {
        private DockableFormInfo m_dFormInfo;
        private Guid m_id;
        public DockableFormInfo FormInfo
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


        public Guid FormID
        {
            get
            {
                return m_id;

            }
        }
        public AlgorithmsPanel()
        {
            m_id = Guid.NewGuid();
            InitializeComponent();
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

        private void AlgorithmsPanel_Load(object sender, EventArgs e)
        {
            PopulateStocksList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pair<int, string> item = listView1.SelectedItems[0].Tag as Pair<int, string>;
            DataSet values = StockDBConnection.Connection.GetPriceVolume(item.First, fromTimePicker.Value, toTimePicker.Value);
            int rowsReturnd = values.Tables[0].Rows.Count;
            int[] prices = new int[rowsReturnd];
            int[] volumes = new int[rowsReturnd];
            DateTime [] dates = new DateTime[rowsReturnd];
            for (int i = 0; i < rowsReturnd; i++)
            {
                prices[i] = Convert.ToInt32(values.Tables[0].Rows[i][0].ToString());
                volumes[i] = Convert.ToInt32(values.Tables[0].Rows[i][1].ToString());
                dates[i] = Convert.ToDateTime(values.Tables[0].Rows[i][2].ToString());
            }
            double[] trends;
            double[] obv, ad;
            for (int i = 0; i < listBox1.SelectedIndices.Count; i++)
            {
                switch (listBox1.SelectedIndices[i])
                {
                    case 0:
                        trends = TrendCalculations.CalculateTrends(volumes);
                        OnAlgGraphRequested(trends, dates,"Trends of " + item.Second);
                      break;
                    case 1:
                      obv = TrendCalculations.CalculateOBV(volumes);
                      OnAlgGraphRequested(obv, dates, "OBV of " + item.Second);
                        break;
                    case 2:
                        ad = TrendCalculations.CalculateAD(prices, volumes);
                        OnAlgGraphRequested(ad, dates, "AD of " + item.Second);
                        break;
                    case 3:
                        ad = TrendCalculations.XDaysAvarage(prices, 3);
                        OnAlgGraphRequested(ad, dates, "3 Days Average of " + item.Second);
                        break;
                    case 4:
                        ad = TrendCalculations.XDaysAvarage(prices, 5);
                        OnAlgGraphRequested(ad, dates, "5 Days Average of " + item.Second);
                        break;
                }
            }
        }
        public EventHandler<AlgGraphRequestedEventArgs> AlgGraphRequested;
        private void OnAlgGraphRequested(double[] data, DateTime[] dates,string graphName)
        {
            if (AlgGraphRequested != null)
                AlgGraphRequested(this, new AlgGraphRequestedEventArgs(data, dates, graphName));
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            Pair<int, string> item = listView1.SelectedItems[0].Tag as Pair<int, string>;
            Pair<DateTime,DateTime> dates = StockDBConnection.Connection.GetDatesBoundries(item.First);
            if(dates == null)
            {
                fromTimePicker.Enabled =false;
                toTimePicker.Enabled = false;
            }
            else{
                fromTimePicker.Enabled =true;
                toTimePicker.Enabled = true;
                fromTimePicker.MinDate = dates.First;
                toTimePicker.MinDate = dates.First;
                fromTimePicker.MaxDate = dates.Second;
                toTimePicker.MaxDate = dates.Second;
            }
        }
    }

    public class AlgGraphRequestedEventArgs : EventArgs
    {
        double[] m_data;
        DateTime[] m_dates;
        string m_name;

        public double[] Data
        {
            get
            {
                return m_data;
            }
        }
        public DateTime[] Dates
        {
            get
            {
                return m_dates;
            }
        }
        public string Name
        {
            get
            {
                return m_name;
            }
        }

        public AlgGraphRequestedEventArgs(double[] data, DateTime[] dates, string name)
        {
            m_dates = dates;
            m_data = data;
            m_name = name;
        }
    }
}
