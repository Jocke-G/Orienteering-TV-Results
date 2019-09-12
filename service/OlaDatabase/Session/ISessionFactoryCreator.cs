using NHibernate;

namespace OlaDatabase
{
    public interface ISessionFactoryCreator
    {
        void OpenSession();
        ISession GetSession();
        void Close();
    }
}