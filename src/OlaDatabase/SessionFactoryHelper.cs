using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using OlaDatabase.Entities;

namespace OlaDatabase
{
    public class SessionFactoryHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        private static ISessionFactory SessionFactory => _sessionFactory ?? (_sessionFactory = CreateSessionFactory());

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure().Database(
            MySQLConfiguration.Standard.ConnectionString(
            c => c.Server("192.168.1.92")
                .Username("TV")
                .Password("password")
                .Database("tt2017"))
            )
            .ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EventEntity>())
            .BuildSessionFactory();
        }
    }
}
