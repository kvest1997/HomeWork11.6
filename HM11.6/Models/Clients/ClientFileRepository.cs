using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace HM11._6.Models.Clients
{
    /// <summary>
    /// Модель репозитория с клиентами
    /// </summary>
    public class ClientFileRepository : IClientFileRepository
    {
        private static int Id;
        static ClientFileRepository()
        {
            Id = 0;
        }

        private static int NextId() => ++Id;


        private List<Client> _clients;

        public List<Client> Clients => _clients;

        public int Count => Clients.Count();

        private readonly string _path;

        public ClientFileRepository(string path)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            _path = path;

            if (File.Exists(_path))
            {
                Load();
                return;
            }

            File.Create(_path);
            NoClientsForLoad();
        }

        /// <summary>
        /// Получение перечесление клиентов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Client> GetAllClients() => Clients;

        /// <summary>
        /// Поиск клиента по ID
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Client GetClientById(int clientId)
        {
            if (_clients.Any(c => c.Id == clientId))
            {
                return _clients.FirstOrDefault(c => c.Id == clientId);
            }
            return null;
        }

        /// <summary>
        /// Добавление клиента в репозиторий
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            if (client is null)
            {
                return;
            }
            client.Id = NextId();
            _clients.Add(client);
            Save();
        }

        /// <summary>
        /// Редактирование клиента в репозитории
        /// </summary>
        /// <param name="client"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void EditClient(Client client)
        {
            if (!_clients.Any(c =>c.Id == client.Id))
            {
                throw new ArgumentOutOfRangeException(nameof(client), "Такого клиента нет в списке");
            }

            _clients[_clients.IndexOf(_clients.First(c => c.Id == client.Id))] = client;
            Save();
        }

        /// <summary>
        /// Удаление клиента из репозитория
        /// </summary>
        /// <param name="idClient"></param>
        public void DeleteClient(int idClient)
        {
            if (_clients.Any(c=>c.Id==idClient))
            {
                _clients.Remove(_clients.FirstOrDefault(c => c.Id == idClient));
            }
            else
            {
                Debug.WriteLine("Данного клиента нет!");
            }

            Save();
        }

        /// <summary>
        /// Полная отчистка всего репозитория
        /// </summary>
        public void Clear()
        {
            if (Clients is null)
            {
                return;
            }
            Clients.Clear();
        }

        /// <summary>
        /// Сохранение Репозитория в файле
        /// </summary>
        private void Save()
        {
            string json = JsonConvert.SerializeObject(_clients);
            File.WriteAllText(_path, json);
        }

        /// <summary>
        /// Загрузка из файла в репозиторий
        /// </summary>
        private void Load()
        {
            string data = File.ReadAllText(_path);

            if (string.IsNullOrEmpty(data))
            {
                NoClientsForLoad();
                return;
            }
            _clients = JsonConvert.DeserializeObject<List<Client>>(data);

            if (_clients is null)
            {
                NoClientsForLoad();
                return;
            }

            Id = Count > 0 ? _clients.Max(c => c.Id) : 0;
        }

        /// <summary>
        /// Создание репозитория если при загрузки данных нет
        /// </summary>
        private void NoClientsForLoad()
        {
            _clients = new List<Client>();
            Id = 0;
        }

        public IEnumerator<Client> GetEnumerator()
        {
            for (int i = 0; i < Clients.Count(); i++)
            {
                yield return Clients[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
