using NHibernate;

namespace MeosDatabase.Session
{
    public interface ISessionFactoryCreator
    {
        void OpenSession();
        ISession GetSession();
        void Close();
    }
}
