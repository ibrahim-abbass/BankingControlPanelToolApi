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
        ICollection<ClientDto> GetClients();
        ClientDto GetClientByIdentifier(string identifier);
        bool DeleteClient(Guid clientId);
        ClientDto UpdateClient(Guid clientId, Client client);
    }
}
