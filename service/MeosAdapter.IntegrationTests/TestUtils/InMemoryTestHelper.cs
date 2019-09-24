using MeosDatabase.Entities;
using MeosDatabase.Session;
using NHibernate;

namespace MeosAdapter.IntegrationTests.TestUtils
{
    class InMemoryTestHelper
    {
        private ISessionFactoryCreator _sessionFactoryCreator;
        private ISession _session;

        internal InMemoryTestHelper()
        {
            _sessionFactoryCreator = new InMemorySessionFactoryCreator();
            SessionFactoryHelper.Initialize(_sessionFactoryCreator);
            _sessionFactoryCreator.OpenSession();
            _session = _sessionFactoryCreator.GetSession();
        }

        internal void Dispose()
        {
        }

        internal ClassEntity CreateClass(string name)
        {
            var classEntity = new ClassEntity("@", string.Empty)
            {
                Name = name,
            };
            _session.Save(classEntity);
            return classEntity;
        }

        internal void FlushAndClear()
        {
            _session.Flush();
            _session.Clear();
        }
    }
}
