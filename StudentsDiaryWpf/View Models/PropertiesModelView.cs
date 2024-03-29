﻿using StudentsDiaryWpf.Commands;
using StudentsDiaryWpf.Models;
using StudentsDiaryWpf.Models.Domains;
using StudentsDiaryWpf.Models.Wrappers;
using StudentsDiaryWpf.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace StudentsDiaryWpf.View_Models
{
    internal class PropertiesModelView : ViewModelBase
    {
        private DbProperties _dbProperties;
        private readonly bool _IsPropertiesChangedWhileRunning;

        public DbProperties DbProperties {
            get
            {
                return _dbProperties;
            }
            set
            {
                _dbProperties = value;
                OnPropertyChanged();
            }
        }
        public PropertiesModelView(bool IsPropertiesChangedWhileRunning)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            DefaultCommand = new RelayCommand(Default);
            _dbProperties = new DbProperties();
            _IsPropertiesChangedWhileRunning = IsPropertiesChangedWhileRunning;
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand DefaultCommand { get; set; }
        

        private void Close(object obj)
        {
            if (_IsPropertiesChangedWhileRunning)
                CloseWindow(obj as Window);
            else
                Application.Current.Shutdown();
        }
        
        private void Confirm(object obj)
        {
             if (!DbProperties.IsValid)
            {
                return;
            }

            _dbProperties.Save();
            RestartApp();
        }

        private void RestartApp()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Default(object obj)
        {
            _dbProperties.ServerAddress = "(local)";
            _dbProperties.ServerName = "SQLEXPRESS";
            _dbProperties.DbName = "StudentsDiary";
            _dbProperties.Login = "Marcin";
            _dbProperties.Password = "123";
            OnPropertyChanged();
            CloseWindow(obj as Window);
            var propertiesWindow = new PropertiesView(true);
            propertiesWindow.Show();




        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
