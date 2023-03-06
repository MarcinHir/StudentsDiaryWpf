using StudentsDiaryWpf.Commands;
using StudentsDiaryWpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentsDiaryWpf.View_Models
{
    internal class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            RefreshStudentCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);

            Students = new ObservableCollection<Student>
            {
                new Student 
                {
                    FirstName = "Marcin", 
                    LastName="Hir", 
                    Technology="4,5,5",
                    Group = new Group {Id = 1},
                },
                new Student { FirstName = "Andrzej", LastName = "Nowak", Technology = "1,3,2" },
                new Student { FirstName = "Marek", LastName = "Kowalski", Technology = "1,2,4" },
                new Student { FirstName = "Leszek", LastName = "Wejchert", Technology = "1,5,1" },
            };

            InitGroups();
        }

       

        public ICommand RefreshStudentCommand { get; set; }
        private Student _selectedStudent;
        private ObservableCollection<Student> _students;
        private ObservableCollection<Group> _groups;
        private int _selectedGroupId;

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


        public Student SelectedStudent 
        { 
            get { return _selectedStudent;  }
            set 
            { 
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Student> Students
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

            MessageBox.Show("RefreshStudents");
        }
            
        private bool CanRefreshStudents(object obj)
        {
            return true;
        }
        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group {Id = 0, Name="Wszystkie"},
                new Group {Id = 1, Name="1A"},
                new Group {Id = 2, Name="2A"}
            };

            SelectedGroupId = 0;
        }
    }
}
