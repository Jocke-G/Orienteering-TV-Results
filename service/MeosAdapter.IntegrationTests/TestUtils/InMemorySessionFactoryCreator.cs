using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MeosDatabase.Entities;
using MeosDatabase.Session;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace MeosAdapter.IntegrationTests
{
    public class InMemorySessionFactoryCreator: ISessionFactoryCreator
    {
        private readonly ISessionFactory _sessionFactory;
        private Configuration _configuration;

        public InMemorySessionFactoryCreator()
        {
            _sessionFactory = CreateSessionFactory();
        }

        public void OpenSession()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory))
            {
                var session = _sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
                CreateSchema(_configuration, session);
            }
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

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClassEntity>())
                    .ExposeConfiguration(cfg =>
                    {
                        _configuration = cfg;
                    })
                    .CurrentSessionContext<ThreadStaticSessionContext>()
                    .BuildSessionFactory();
        }

        private void CreateSchema(Configuration configuration, ISession session)
        {
            var export = new SchemaExport(configuration);
            export.Execute(true, true, false, session.Connection, null);
        }
    }
}
