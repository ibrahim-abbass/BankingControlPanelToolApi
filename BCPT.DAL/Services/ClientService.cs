﻿using BCPT.ABSTACTION;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.BAL
{
    public class ClientService : IClinetService
    {
        public bool DeleteClient(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public ClientDto GetClientByIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public ICollection<ClientDto> GetClients()
        {
            throw new NotImplementedException();
        }

        public ClientDto UpdateClient(Guid clientId, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
