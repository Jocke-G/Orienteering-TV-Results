using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using OlaDatabase.Entities;
using OrienteeringTvResults.Model.Configuration;

namespace OlaDatabase
{
    public class SessionFactoryHelper
    {
        private static ISessionFactory _sessionFactory;
        private static DatabaseConfiguration _configuration;

        public static void Initialize(DatabaseConfiguration conf)
        {
            _configuration = conf;
            _sessionFactory = CreateSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }

        private static ISessionFactory SessionFactory => _sessionFactory ?? (_sessionFactory = CreateSessionFactory());

        private static ISessionFactory CreateSessionFactory()
        {
            var session = Fluently.Configure().Database(
            MySQLConfiguration.Standard.ConnectionString(
            c => c.Server(_configuration.Server)
                .Username(_configuration.Username)
                .Password(_configuration.Password)
                .Database(_configuration.Database))
            )
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EventEntity>())
            .BuildSessionFactory();

            return session;
        }
    }
}
