using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StudentsDiaryWpf.Commands;
using StudentsDiaryWpf.Models;
using StudentsDiaryWpf.Models.Domains;
using StudentsDiaryWpf.Models.Wrappers;
using StudentsDiaryWpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentsDiaryWpf.View_Models
{
    internal class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public MainViewModel()
        {
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            PropertiesCommand = new RelayCommand(Properties);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);

            LoadedWindow(null);
            
        }

        

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand PropertiesCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }





        private StudentWrapper _selectedStudent;
        private ObservableCollection<StudentWrapper> _students;
        private ObservableCollection<Group> _groups;
        private int _selectedGroupId;

        private async void LoadedWindow(object obj)
        {
            if (!IfConnectionToDatabaseIsValid())
            {
                var MetroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await MetroWindow.ShowMessageAsync("Łączenie z bazą danych", "Nie można połączyć z bazą danych. Zmienić ustawienia?", MessageDialogStyle.AffirmativeAndNegative);

                if (dialog == MessageDialogResult.Negative)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    var propertiesWindow = new PropertiesView(false);
                    propertiesWindow.ShowDialog();
                }
            }
            else
            {
                RefreshDiary();
                InitGroups();
            }
        }

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }


        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }


        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private bool CanRefreshStudents(object obj)
        {
            return true;
        }
        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }
        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Usuwanie ucznia",
                $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} " +
                $"{SelectedStudent.LastName}",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();


        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudent_WindowClosed;
            addEditStudentWindow.Show();
        }

        private void AddEditStudent_WindowClosed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(_repository.GetStudents(SelectedGroupId));
        }

        private void Properties(object obj)
        {
            var propertiesWindow = new PropertiesView(true);
            propertiesWindow.Closed += PropertiesWindow_Closed;
            propertiesWindow.Show();
        }

        private void PropertiesWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private bool IfConnectionToDatabaseIsValid()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
