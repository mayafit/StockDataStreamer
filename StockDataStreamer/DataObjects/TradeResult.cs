using System;
using System.Collections.Generic;
using System.Text;

namespace StockDataStreamer.DataObjects
{
    public struct TradeResult
    {

        private int m_sid;
        private int m_st;
        private int m_s;
        private int m_ss;
        private int m_tm;
        private int m_cpt;
        private int m_cp;
        private int m_ctp;
        private int m_ppe;
        private int m_sdbe;
        private int m_sdap;
        private int m_to;
        private bool m_is;
        private short m_exCode;
        private DateTime m_uTime;
        private int m_pcc;
        private int m_pc;

        public int SecurityID { 
            get
            {
                return m_sid;
            }
            set
            {
                m_sid = value;
            }
        }
        public int SecurityType
        {
            get { return m_st;}
            set { m_st = value; }

        }
        public int Sector
        {
            get
            {
                return m_s;
            }
            set
            {
                m_s = value;
            }
        }
        public int SubSector
        {
            get
            {
                return m_ss;
            }
            set
            {
                m_ss = value;
            }
        }
        public int TradeMethod
        {
            get
            {
               return m_tm;
            }
            set{
                m_tm = value;
            }
        }
        public int ClosePriceType
        {
            get
            {
                return m_ctp;
            }
            set
            {
                m_ctp = value;
            }
        }
        public int ClosePrice
        {
            get
            {
                return m_cp;
            }
            set { m_cp = value; }
        }
        public int ClosePriceTrend
        {
            get
            {
                return m_cpt;
            }
            set{
                m_cpt = value;
            }
        }
        public int PriceChange
        {
            get
            {
                return m_pc;
            }
            set{
                m_pc = value;
            }
        }
        public int PercentChange
        {
            get
            {
                return m_pcc;
            }
            set{
                m_pcc = value;
            }
        }
        public int PercentPartialExec
        {
            get
            {
                return m_ppe;
            }
            set
            {
                m_ppe = value;
            }


        }
        public int SupplyDemandBestOrders
        {
            get
            {
                return m_sdbe;
            }
            set
            {
                m_sdbe = value;
            }
        }
        public int SupplyDemandAtPrice
        {
            get
            {
                return m_sdap;
            }
            set
            {
                m_sdap = value;
            }
        }
        public int TurnOver
        {
            get
            {
                return m_to;
            }
            set
            {
                m_to = value;
            }
        }
        public bool IsSupply
        {
            get
            {
                return m_is;
            }
            set
            {
                m_is = value;
            }
        }
        public short ExCode
        {
            get
            {
                return m_exCode;
            }
            set
            {
                m_exCode = value;
            }
        }
        public DateTime UpdateTime
        {
            get
            {
                return m_uTime;
            }
            set
            {
                m_uTime = value;
            }
        }
    }

    public struct TradeResultExt
    {
        int m_sid;
        int m_v;
        int m_pcd;
        int m_pcm;
        DateTime m_date;
        public int SecurityID
        {
            get
            {
                return m_sid;
            }
            set
            {
                m_sid = value;
            }
        }
        public int Volume
        {
            get
            {
                return m_v;
            }
            set
            {
                m_v = value;
            }
        }
        public int PercentageDaily
        {
            get
            {
                return m_pcd;
            }
            set
            {
                m_pcd = value;
            }
        }
        public int PercentageMonthly
        {
            get
            {
                return m_pcm;
            }
            set
            {
                m_pcm = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return m_date;
            }
            set
            {
                m_date = value;
            }  
        }

    }
}
