using SAED.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAED.Web.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task<ICollection<Avaliacao>> GetAvaliacoesAsync();
    }
}
