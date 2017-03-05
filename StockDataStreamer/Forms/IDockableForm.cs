using System;
using Crom.Controls.Docking;

namespace StockDataStreamer.Forms
{
    internal interface IDockableForm
    {
        DockableFormInfo FormInfo
        {
            get; set;
        }

        Guid FormID
        { get;
        }

    }
}
