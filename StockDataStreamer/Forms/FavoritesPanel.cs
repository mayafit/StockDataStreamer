using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;

namespace StockDataStreamer.Forms
{
    public partial class FavoritesPanel : Form, IDockableForm
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

        public FavoritesPanel()
        {
            m_id = Guid.NewGuid();
            InitializeComponent();
        }
    }
}
