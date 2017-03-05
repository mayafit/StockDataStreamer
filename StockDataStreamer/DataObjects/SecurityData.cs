namespace StockDataStreamer.DataObjects
{
    public struct SecurityData
    {
        private int m_securityId;
        private string m_shortName;
        private string m_name;
        private string m_isinNumber;
        private short m_tradedIn;
        private int m_securityType;
        private int m_sector;
      

        public int SecurityID
        {
            get 
            {
                return m_securityId;
            }
            set
            {
                m_securityId = value;
            }
        }

        public string ShortName
        {
            get { return m_shortName; }
            set { m_shortName = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string IsinNumber
        {
            get { return m_isinNumber; }
            set { m_isinNumber = value; }
        }

        public short TradedIn
        {
            get { return m_tradedIn; }
            set { m_tradedIn = value; }
        }

        public int SecurityType
        {
            get { return m_securityType; }
            set { m_securityType = value; }
        }

        public int Sector
        {
            get { return m_sector; }
            set { m_sector = value; }
        }
    }
}
    