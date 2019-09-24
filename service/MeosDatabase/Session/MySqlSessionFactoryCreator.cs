using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MeosDatabase.Entities;
using NHibernate;
using NHibernate.Context;

namespace MeosDatabase.Session
{
    public class MySqlSessionFactoryCreator: ISessionFactoryCreator
    {
        private readonly ISessionFactory _sessionFactory;

        public MySqlSessionFactoryCreator(MeosDatabaseConfiguration conf)
        {
            _sessionFactory = CreateSessionFactory(conf);
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

        private static ISessionFactory CreateSessionFactory(MeosDatabaseConfiguration conf)
        {
            var config = Fluently.Configure().Database(
                MySQLConfiguration.Standard.ConnectionString(
                    c => c.Server(conf.Server)
                        .Username(conf.Username)
                        .Password(conf.Password)
                        .Database(conf.Database))
            )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClassEntity>())
                .CurrentSessionContext<ThreadStaticSessionContext>();
            var sessionFactory = config.BuildSessionFactory();

            return sessionFactory;
        }
    }
}
