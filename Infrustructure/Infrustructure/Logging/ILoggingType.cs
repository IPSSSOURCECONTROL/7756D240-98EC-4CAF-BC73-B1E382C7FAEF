namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Logging
{
    /// <summary>
    /// Provides logging functionality.
    /// </summary>
    public interface ILoggingType
    {
        void Log(object logItem);
        void Error(string error);
        void Exception(string exception);
        void Exception(System.Exception exception);
        void Info(string info);
    }
}
