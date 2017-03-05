using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Crom.Controls.Docking;
using ZedGraph;

namespace StockDataStreamer.Forms
{
    public partial class BasicGraphPanel : Form,IDockableForm
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

        public String StockName
        {
           set
           {
               graph.GraphPane.Title.Text = value;
           }
        }


        public Guid FormID
        {
            get
            {
                return m_id;

            }
        }

        public List<Pair<DateTime,double>> Values
        {
            set
            {
                if(value == null)
                    return;
                PointPairList list = new PointPairList();
                for(int i=0;i<value.Count;i++)
                {
                    double x = new XDate(value[i].First);
                    double y = value[i].Second;
                    list.Add(x, y);
                }
                graph.GraphPane.AddCurve("data", list, Color.Blue, SymbolType.Circle);
                graph.GraphPane.XAxis.Type = AxisType.Date;
                graph.AxisChange();
            }
        }

        public BasicGraphPanel()
        {
            InitializeComponent();
        }
    }
}
