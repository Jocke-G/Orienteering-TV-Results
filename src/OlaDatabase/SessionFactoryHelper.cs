using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using OlaDatabase.Entities;

namespace OlaDatabase
{
    public class SessionFactoryHelper
    {
        private static OlaConfiguration _configuration;
        private static ISessionFactory _sessionFactory;

        public static ISession OpenSession(OlaConfiguration configuration)
        {
            _configuration = configuration;
            return SessionFactory.OpenSession();
        }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }

        private static ISessionFactory SessionFactory => _sessionFactory ?? (_sessionFactory = CreateSessionFactory());

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure().Database(
            MySQLConfiguration.Standard.ConnectionString(
            c => c.Server(_configuration.Server)
                .Username(_configuration.Username)
                .Password(_configuration.Password)
                .Database(_configuration.Database))
            )
            .ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EventEntity>())
            .BuildSessionFactory();
        }
    }
}
