using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace StockDataStreamer
{
    class HttpStreamer
    {
        string m_folderName;

        public HttpStreamer(string folderName)
        {
            m_folderName = folderName;
        }
        public bool GetStream(string fileName)
        {
            FileStream fs = null;
            try{
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.tase.co.il/FileDistribution/PackTarget/" + fileName);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                fs = new FileStream(Path.Combine(m_folderName,fileName),FileMode.OpenOrCreate);
                byte [] data = new byte[0x1000];
                int rcount = 0;
                do 
                {
                    rcount = dataStream.Read(data, 0, 0x1000);
                    fs.Write(data, 0, rcount);
                }
                while(rcount>0);
                return true;
            }
            catch{
                return false;
            }
            finally{
                if(fs != null)
                    fs.Close();
            }
        }
    }
}
