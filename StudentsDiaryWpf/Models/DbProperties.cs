using StudentsDiaryWpf.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDiaryWpf.Models
{
    public class DbProperties :IDataErrorInfo
    {
        private bool _isServerAdressValid;
        private bool _isServerNameValid;
        private bool _isDbNameValid;
        private bool _isLoginValid;
        private bool _isPasswordValid;

        public string ServerAddress
        {
            get
            {
                return Settings.Default.ServerAdress;
            }
            set
            {
                Settings.Default.ServerAdress = value;
            }
        }
        public string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }
        public string DbName
        {
            get
            {
                return Settings.Default.DbName;
            }
            set
            {
                Settings.Default.DbName = value;
            }
        }
        public string Login
        {
            get
            {
                return Settings.Default.Login;
            }
            set
            {
                Settings.Default.Login = value;
            }
        }
        public string Password
        {
            get
            {
                return Settings.Default.Password;
            }
            set
            {
                Settings.Default.Password = value;
            }
        }

        

        public void Save()
        {
            Settings.Default.Save();
        }
        public string Error { get; set; }
        public string this[string columnName]
        {
            get
            {
                switch (columnName) 
                {
                    case nameof(ServerAddress):
                        if(string.IsNullOrWhiteSpace(ServerAddress))
                        {
                            Error = "Pole jest wymagane!";
                            _isServerAdressValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isServerAdressValid = true;
                        }
                        break;
                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Pole jest wymagane!";
                            _isServerNameValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isServerNameValid = true;
                        }
                        break;
                    case nameof(DbName):
                        if (string.IsNullOrWhiteSpace(DbName))
                        {
                            Error = "Pole jest wymagane!";
                            _isDbNameValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isDbNameValid = true;
                        }
                        break;
                    case nameof(Login):
                        if (string.IsNullOrWhiteSpace(Login))
                        {
                            Error = "Pole jest wymagane!";
                            _isLoginValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isLoginValid = true;
                        }
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            Error = "Pole jest wymagane!";
                            _isPasswordValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isPasswordValid = true;
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }
        public bool IsValid
        {
            get
            {
                return _isServerAdressValid && _isServerNameValid && _isDbNameValid && _isLoginValid && _isPasswordValid;
            }
        }
    }
}
