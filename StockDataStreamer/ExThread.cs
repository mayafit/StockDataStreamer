using System;
using System.Threading;

namespace StockDataStreamer
{
    public class ExThread
    {
        #region Class Members

        private Delegate m_threadMethod;
        private object m_threadReturnValue;
        private object[] m_params;
        private Thread m_thread;

        public Delegate ThreadMethod
        {
            set
            {
                m_threadMethod = value;
            }
        }
        #endregion
        #region Events
        public event EventHandler<ExThreadEventArgs> Completed;

        private void OnCompleted()
        {
            if(Completed != null)
                Completed(this, new ExThreadEventArgs(m_threadReturnValue));
        }
        #endregion
        #region Constructors

        public ExThread()
        {
            
        }
        public ExThread(Delegate threadMethod)
        {
            m_threadMethod = threadMethod;
        }

        public ExThread(Delegate threadMethod, object[] threadparams)
        {
            m_threadMethod = threadMethod;
            m_params = threadparams;
        }



        #endregion

        #region RunMethods

        private void Run()
        {
            m_threadReturnValue = m_threadMethod.DynamicInvoke(m_params);
            OnCompleted();
        }

        public void Start(){
            m_thread = new Thread(Run);
            m_thread.Start();
        }
        #endregion
    }
    public class ExThreadEventArgs : EventArgs
    {
        private object m_returnData;
        public object ReturnedData
        {
            get
            {
                return m_returnData;
            }
        }
        public ExThreadEventArgs(object data)
        {
            m_returnData = data;
        }
    }
}
