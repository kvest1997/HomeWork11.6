using HM11._6.Models.Clients;
using HM11._6.Models.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM11._6.Models
{
    /// <summary>
    /// Модель банка
    /// </summary>
    public class Bank
    {
       
        public string Name { get; private set; }

        public IClientFileRepository ClientRepository { get; set; }
        private readonly WorkerBase _worker;

        public Bank(string name, IClientFileRepository clientsRepository, WorkerBase worker)
        {
            Name = name;
            ClientRepository = clientsRepository;
            _worker = worker;
        }
        /// <summary>
        /// Получение информации о клиентах в список
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClientInfo> GetClientInfos()
        {
            var clientsInfo = new List<ClientInfo>();

            foreach (var client in ClientRepository)
            {
                clientsInfo.Add(_worker.GetClientInfo(client));
            }
            return clientsInfo;
        }

        /// <summary>
        /// Добавление клиента в список
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            ClientRepository.AddClient(client);
        }

        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="client"></param>
        public void EditClient(Client client)
        {
            ClientRepository.EditClient(client);
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="client"></param>
        public void DeleteClient(Client client)
        {
            ClientRepository.DeleteClient(client.Id);
        }
    }
}
