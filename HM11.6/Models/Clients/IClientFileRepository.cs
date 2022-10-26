using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM11._6.Models.Clients
{
    public interface IClientFileRepository : IEnumerable<Client>
    {
         int Count { get; }

        IEnumerable<Client> GetAllClients();

        Client GetClientById(int clientId);

        void AddClient(Client client);
        void EditClient(Client client);
        void DeleteClient(int idClient);
        void Clear();
    }
}
