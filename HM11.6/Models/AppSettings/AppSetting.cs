﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM11._6.Models
{
    public class AppSetting
    {
        private string _clientRepositoryFile;
        public AppSetting()
        {
            Debug.WriteLine($"{this.GetType().Name} конструктор по умолчанию");

            _clientRepositoryFile = string.Empty;
        }

       /// <summary>
       /// Создание файла для настроек
       /// </summary>
        public string ClientRepositoryFile
        {
            get
            {
                if (string.IsNullOrEmpty(_clientRepositoryFile))
                {
                    _clientRepositoryFile = @"clientsRep.json";
                }
                return _clientRepositoryFile;
            }
            set { _clientRepositoryFile = value; }
        }
    }
}
