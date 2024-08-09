using BCPT.ABSTACTION;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.BAL
{
    public interface IClientService
    {
        Task<Response> AddClient(InsertClientRequest client);
        Task<Response<ICollection<Client>>> GetClients();
        Task<Response<ICollection<Client>>> FilterClients(FilterRequest filter);
        Task<Response<ICollection<Client>>> SortClients(string sortBy, bool isAsc);
        Task<Response> DeleteClient(Guid id);
        Task<Response> UpdateClient(Guid id, UpdateClientRequest client);
    }
}
