using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StockDataStreamer
{
    public partial class CalendarForm : Form
    {

        private DateTime m_selected;
        public DateTime SelectedDate
        {
            get
            {
                return m_selected;
            }

        }
        public CalendarForm()
        {
            InitializeComponent();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            m_selected = e.Start;
            Hide();
        }
    }
}
