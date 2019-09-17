using NHibernate;

namespace OlaDatabase.Session
{
    public interface ISessionFactoryCreator
    {
        void OpenSession();
        ISession GetSession();
        void Close();
    }
}
