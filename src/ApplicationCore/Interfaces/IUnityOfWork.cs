using System.Threading.Tasks;

namespace SAED.ApplicationCore.Interfaces
{
    public interface IUnityOfWork
    {
        public void Commit();
        public Task CommitAsync();
        public void Rollback();
    }
}
