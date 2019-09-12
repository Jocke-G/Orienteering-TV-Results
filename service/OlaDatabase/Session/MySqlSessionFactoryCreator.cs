using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using OlaDatabase.Entities;
using OrienteeringTvResults.Model.Configuration;

namespace OlaDatabase
{
    public class MySqlSessionFactoryCreator: ISessionFactoryCreator
    {
        private readonly ISessionFactory _sessionFactory;

        public MySqlSessionFactoryCreator(DatabaseConfiguration databaseConfiguration)
        {
            _sessionFactory = CreateSessionFactory(databaseConfiguration);
        }

        public void OpenSession()
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        public ISession GetSession()
        {
            var session = _sessionFactory.GetCurrentSession();
            return session;
        }

        public void Close()
        {
            ISession currentSession = CurrentSessionContext.Unbind(_sessionFactory);
            currentSession.Disconnect();
            currentSession.Close();
            currentSession.Dispose();

        }

        private static ISessionFactory CreateSessionFactory(DatabaseConfiguration databaseConfiguration)
        {
            var config = Fluently.Configure().Database(
                MySQLConfiguration.Standard.ConnectionString(
                    c => c.Server(databaseConfiguration.Server)
                        .Username(databaseConfiguration.Username)
                        .Password(databaseConfiguration.Password)
                        .Database(databaseConfiguration.Database))
            )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EventEntity>())
                .CurrentSessionContext<ThreadStaticSessionContext>();
            var sessionFactory = config.BuildSessionFactory();

            return sessionFactory;
        }
    }
}
