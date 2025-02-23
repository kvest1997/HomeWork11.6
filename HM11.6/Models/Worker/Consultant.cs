﻿using HM11._6.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM11._6.Models.Worker
{
    public class Consultant : WorkerBase
    {
        /// <summary>
        /// Конструктор с правами для консультанта
        /// </summary>
        public Consultant()
        {
            UserAccess = new UserAccess(
                new CommnadsAccess
                {
                    AddClient = false,
                    EditClient = true,
                    DelClient = false
                },
                new EditFieldAccess()
                {
                    FirstName = false,
                    LastName = false,
                    Surname = false,
                    PassportData = false,
                    PhoneNumber = true
                });
        }

        /// <summary>
        /// Переопределение метода чтобы консультанту скрыть данные паспорта
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public override ClientInfo GetClientInfo(Client client)
        {
            var clientInfo = new ClientInfo(client)
            {
                PassportSerie = "****",
                PassportNumber = "******"
            };

            return clientInfo;
        }


        public override string ToString()
        {
            return "Консультант";
        }
    }
}
