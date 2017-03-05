using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using StockDataStreamer.DataObjects;
using System.Data;

namespace StockDataStreamer.DBLayer
{
    public class StockDBConnection
    {
        private static string DB_CONN_STR = "server=localhost;User Id=root;password=alonma;Persist Security Info=True;database=stocksdb";
        private static string INSERT_TD_STATEMENT = "INSERT INTO stocksdb.stocksTradeData VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},'{14}',{15},{16})";
        private static string INSERT_TD_Ext_STATEMENT = "UPDATE stocksdb.stocksTradeData SET volume={0} , percentChangeDay={1} , percentChangeMonth={2} where stockID={3} AND DateUpdated='{4}'";
        private static string ADD_STOCK = "INSERT INTO stocksdb.securities VALUES ({0},'{1}','{2}','{3}',{4},{5},{6})";
        private MySqlConnection m_dbConnection;
        private MySqlCommand m_dbCommand;
        private static StockDBConnection m_instance;
        public static StockDBConnection Connection
        {
            get
            {
                if(m_instance == null)
                    m_instance = new StockDBConnection();
                if(!m_instance.IsOpen)
                    m_instance.Open();
                return m_instance;
                
            }
        }
        private StockDBConnection()
        {
            
        }

        public bool IsOpen
        {
            get { return m_dbConnection != null ; }
        }


        public void Open()
        {
            m_dbConnection = new MySqlConnection(DB_CONN_STR);
            m_dbConnection.Open();
            m_dbCommand = m_dbConnection.CreateCommand();
        }

        public void Close()
        {
            if (m_dbConnection != null)
                m_dbConnection.Close();
            m_dbConnection = null;
        }


        public void UpdateTradeInfoToDB(TradeResult item)
        {
            try
            {
                m_dbCommand.CommandText = string.Format(INSERT_TD_STATEMENT,
                                                    item.SecurityID,
                                                    item.SecurityType,
                                                    item.Sector,
                                                    item.SubSector,
                                                    item.TradeMethod,
                                                    item.ClosePrice,
                                                    item.ClosePriceType,
                                                    item.ClosePriceTrend,
                                                    item.PriceChange,
                                                    item.PercentChange,
                                                    item.TurnOver,
                                                    item.SupplyDemandBestOrders,
                                                    (item.IsSupply) ? 1 : 0,
                                                    item.ExCode,
                                                    item.UpdateTime.ToString("yyyy-MM-dd"),
                                                    item.SupplyDemandAtPrice,
                                                    item.PercentPartialExec
                                                    );
                m_dbCommand.ExecuteNonQuery();
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// Adds the security to DB.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddSecurityToDB(SecurityData item)
        {
            try
            {
                m_dbCommand.CommandText = string.Format(ADD_STOCK,
                                                        item.SecurityID,
                                                        item.ShortName,
                                                        item.Name,
                                                        item.IsinNumber,
                                                        item.TradedIn,
                                                        item.SecurityType,
                                                        item.Sector
                                                        );
                m_dbCommand.ExecuteNonQuery();
            }
            catch
            {
                return;
            }
        }
        public List<Pair<int,string>> Securities
        {
            get
            {
                string query = "SELECT securityID,Name from stocksdb.securities ORDER BY Name ASC";
                MySqlCommand command = new MySqlCommand(query,m_dbConnection);
     //           m_dbCommand.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                List<Pair<int,string>> values = new List<Pair<int, string>>();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    values.Add(new Pair<int, string>(id, name));

                }  
                //values = from row in reader
                //         select new { row.securityID, row.Name };
                reader.Close();
                
                return values;

            }
        }

        public Pair<DateTime, DateTime> GetDatesBoundries(int stockID)
        {
            MySqlDataReader reader = null;
            try
            {
                string query = "SELECT MIN(DateUpdated) , MAX(DateUpdated) from stocksdb.stockstradedata where StockID=" + stockID;
                MySqlCommand command = new MySqlCommand(query, m_dbConnection);
                reader = command.ExecuteReader();
                if (reader.Read())
                    return new Pair<DateTime, DateTime>(reader.GetDateTime(0), reader.GetDateTime(1));
            }
            catch
            {

            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return null;
        }


        internal List<Pair<DateTime, double>> SValuesForDates(int stockID, System.DateTime startDate, System.DateTime endDate)
        {
            string sstartDate = startDate.ToString("yyyy-MM-dd");
            string sendDate = endDate.ToString("yyyy-MM-dd");
            string query = String.Format("SELECT ClosePrice,DateUpdated from stockstradedata WHERE StockID = {0} AND DateUpdated<= '{1}' AND DateUpdated>='{2}' ORDER BY DateUpdated ASC",stockID,sendDate,sstartDate);

            MySqlCommand command = new MySqlCommand(query, m_dbConnection);
            //           m_dbCommand.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            List<Pair<DateTime, double>> values = new List<Pair<DateTime, double>>();
            while (reader.Read())
            {
                double value  = reader.GetDouble(0);
                DateTime date = reader.GetDateTime(1);
                values.Add(new Pair<DateTime, double>(date,value));

            }
            reader.Close();

            return values;
        }
 
        internal DataSet GetPriceVolume(int stockID, System.DateTime startDate, System.DateTime endDate)
        {
            string sstartDate = startDate.ToString("yyyy-MM-dd");
            string sendDate = endDate.ToString("yyyy-MM-dd");
            string query = String.Format("SELECT ClosePrice,volume,DateUpdated from stockstradedata WHERE StockID = {0} AND DateUpdated<= '{1}' AND DateUpdated>='{2}' ORDER BY DateUpdated ASC", stockID, sendDate, sstartDate);
            //MySqlCommand command = new MySqlCommand(query, m_dbConnection);
            m_dbConnection.Close();
            MySqlDataAdapter da = new MySqlDataAdapter(query, m_dbConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        internal void UpdateTradeExInfoToDB(TradeResultExt item)
        {
            try
            {
                m_dbCommand.CommandText = string.Format(INSERT_TD_Ext_STATEMENT,
                                                    item.Volume,
                                                    item.PercentageDaily,
                                                    item.PercentageMonthly,
                                                    item.SecurityID,
                                                    item.Date.ToString("yyyy-MM-dd")
                                                    );
                m_dbCommand.ExecuteNonQuery();
            }
            catch
            {
                return;
            }
        }
    }
}
