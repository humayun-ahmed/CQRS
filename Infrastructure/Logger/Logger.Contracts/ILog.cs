namespace Infrastructure.Logger.Contracts
{
	using System;

	public interface ILog
    {
        void Debug(string message);
        void Debug(string message, params object[] propertyValues);
        void Error(string message);
        void Error(string message, params object[] propertyValues);
        void Error(Exception exception, string messageTemplate, params object[] propertyValues);
        void Fatal(string message);
        void Fatal(string message, params object[] propertyValues);
        void Fatal(Exception exception, string message, params object[] propertyValues);
        void Information(string message);
        void Information(string message, params object[] propertyValues);

    }
}
