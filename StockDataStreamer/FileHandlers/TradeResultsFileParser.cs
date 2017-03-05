using System;
using System.IO;
using StockDataStreamer.DataObjects;
using StockDataStreamer.DBLayer;

namespace StockDataStreamer.FileHandlers
{
    public class TradeResultsFileParser
    {

        private StreamReader m_reader;
        private short m_year;

        public  TradeResultsFileParser( short year)
        {
           m_year = year;
        }

        public void Close()
        {
            if(m_reader != null)
                m_reader.Close();
            StockDBConnection.Connection.Close();
        }


        public void Parse0020File(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            m_reader = new StreamReader(file);
            string line;
            short month,day,lineType;

            while(!string.IsNullOrEmpty(line = m_reader.ReadLine()))
            {
                lineType = short.Parse(line.Substring(0, 2));
                if(lineType != 2)
                    continue;
                TradeResult tr = new TradeResult();
                tr.SecurityID = int.Parse(line.Substring(2, 8));
                tr.SecurityType = int.Parse(line.Substring(10, 2));
                tr.Sector = int.Parse(line.Substring(12, 2));
                tr.SubSector = int.Parse(line.Substring(14, 2));
                tr.TradeMethod = int.Parse(line.Substring(16, 2));
                tr.ClosePrice = int.Parse(line.Substring(18,9));
                tr.ClosePriceType = int.Parse(line.Substring(27, 2));
                tr.ClosePriceTrend = int.Parse(line.Substring(29, 1));
                tr.PriceChange = int.Parse(line.Substring(30, 8));
                tr.PercentChange = int.Parse(line.Substring(38, 4));
                tr.PercentPartialExec = int.Parse(line.Substring(42, 4));
                tr.TurnOver = int.Parse(line.Substring(46, 9));
                tr.SupplyDemandBestOrders = int.Parse(line.Substring(55, 9));
                tr.IsSupply = (int.Parse(line.Substring(64, 1)) == 1) ? true : false;
                tr.ExCode = short.Parse(line.Substring(65, 2));
                month = short.Parse(line.Substring(67, 2));
                day = short.Parse(line.Substring(69, 2));
                tr.UpdateTime = new DateTime(m_year, month, day);
                tr.SupplyDemandAtPrice = int.Parse(line.Substring(71, 9));
                if (!StockDBConnection.Connection.IsOpen)
                    StockDBConnection.Connection.Open();
                StockDBConnection.Connection.UpdateTradeInfoToDB(tr);
            }
            m_reader.Close();
        }
        public void Parse0803File(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            m_reader = new StreamReader(file);
            string line;
            short lineType;

            while (!string.IsNullOrEmpty(line = m_reader.ReadLine()))
            {
                lineType = short.Parse(line.Substring(0, 2));
                if (lineType != 2)
                    continue;
                SecurityData tr = new SecurityData();
                tr.SecurityID = int.Parse(line.Substring(2, 8));
                tr.ShortName = line.Substring(10, 10).Trim();
                tr.Name = line.Substring(20, 15).Replace('\'',' ').Trim();
                tr.IsinNumber = line.Substring(35, 12).Replace('\'', ' ').Trim();
                tr.TradedIn = short.Parse(line.Substring(47, 2));
                tr.SecurityType = int.Parse(line.Substring(60, 4));
                tr.Sector = int.Parse(line.Substring(64, 6));
                StockDBConnection.Connection.AddSecurityToDB(tr);
            }
            m_reader.Close();

        }
        public void Parse0068File(string fileName,DateTime date)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            m_reader = new StreamReader(file);
            string line;
            short month, day, lineType;

            while (!string.IsNullOrEmpty(line = m_reader.ReadLine()))
            {
                lineType = short.Parse(line.Substring(0, 2));
                if (lineType != 2)
                    continue;
                TradeResultExt tr = new TradeResultExt();
                tr.SecurityID = int.Parse(line.Substring(2, 8));
                tr.Volume = int.Parse(line.Substring(12,9));
                tr.PercentageDaily = int.Parse(line.Substring(21, 7));
                tr.PercentageMonthly = int.Parse(line.Substring(29, 7));
                tr.Date = date;
                if (!StockDBConnection.Connection.IsOpen)
                    StockDBConnection.Connection.Open();
                StockDBConnection.Connection.UpdateTradeExInfoToDB(tr);
            }
            m_reader.Close();
        }

    }
}
