using System;
using NHibernate;

namespace OlaDatabase
{
    public class SessionFactoryHelper: IDisposable
    {
        private static ISessionFactoryCreator _sessionFactoryCreator;

        public static void Initialize(ISessionFactoryCreator sessionFactoryCreator)
        {
            _sessionFactoryCreator = sessionFactoryCreator;
        }

        public SessionFactoryHelper()
        {
            _sessionFactoryCreator.OpenSession();
        }

        public static ISession GetSession()
        {
            return _sessionFactoryCreator.GetSession();
        }

        public void Dispose()
        {
            _sessionFactoryCreator.Close();
        }
    }
}
