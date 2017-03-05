using System;
using System.Windows.Forms;

namespace StockDataStreamer
{
    public partial class ManualDataDl : Form
    {
        private DateTime m_start;
        private DateTime m_end;
        public ManualDataDl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileTypes ftype;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    ftype = FileTypes.TradingData;
                break;
                case 1:
                ftype = FileTypes.SecList;
                break;
                case 2:
                ftype = FileTypes.TradingDataExt;
                break;
                default:
                ftype = FileTypes.All;
                break;
            }
            progressBar.Visible = true;
            progressBar.Maximum = Math.Abs( m_end.Subtract(m_start).Days);
            StocksDataRetreiver th = new StocksDataRetreiver(m_start, m_end,ftype);
            th.Completed += th_Completed;
            th.Step += th_Step;
            th.Start();
        }

        void th_Step(object sender, EventArgs e)
        {
           StepBar();
        }

        void th_Completed(object sender, ExThreadEventArgs e)
        {
            CloseProgress();
        }



        private delegate void Invoker();
        private void CloseProgress()
        {
            if (!InvokeRequired)
            {
                progressBar.Visible = false;
            }
            else
                Invoke(new Invoker(CloseProgress));
        }
        private void StepBar()
        {
            if (!InvokeRequired)
            {
             progressBar.PerformStep();
            }
            else
                Invoke(new Invoker(StepBar));
        }


        private void button4_Click(object sender, EventArgs e)
        {
            CalendarForm cform = new CalendarForm();
            cform.ShowDialog();
            textBox2.Text = cform.SelectedDate.ToString();
            m_start = cform.SelectedDate;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CalendarForm cform = new CalendarForm();
            cform.ShowDialog();
            textBox3.Text = cform.SelectedDate.ToString();
            m_end = cform.SelectedDate;
        }
    }
}
