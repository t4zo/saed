using System.Threading.Tasks;

namespace SAED.ApplicationCore.Interfaces
{
    public interface IEntitySeed
    {
        Task LoadAsync();
    }
}
