namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Logging
{
    public class NLogWrapper: ILoggingType
    {
        public void Log(object logItem)
        {
            return;
        }

        public void Error(string error)
        {
            throw new System.NotImplementedException();
        }

        public void Exception(string exception)
        {
            throw new System.NotImplementedException();
        }

        public void Exception(System.Exception exception)
        {
            throw new System.NotImplementedException();
        }

        public void Info(string info)
        {
            throw new System.NotImplementedException();
        }
    }
}
