using StudentsDiaryWpf.Commands;
using StudentsDiaryWpf.Models;
using StudentsDiaryWpf.Models.Domains;
using StudentsDiaryWpf.Models.Wrappers;
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
    internal class AddEditStudentModelView : ViewModelBase
    {
        private Repository _repository = new Repository();
        public AddEditStudentModelView(StudentWrapper student = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);



            if (student == null)
            {
                Student = new StudentWrapper();
            }
            else 
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }



        public ICommand CloseCommand { get; set; }

        public ICommand ConfirmCommand { get; set; }

        private StudentWrapper _student;
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
        public StudentWrapper Student
        {
            get { return _student; }
            set 
            { 
                _student = value;
                OnPropertyChanged();
            }
            
        }

        private bool _isUpdate;
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }

        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "-- brak --" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = Student.Group.Id;
        }
        
        private void Confirm(object obj)
        {
            if (!Student.IsValid)
                return;

            if (!IsUpdate)
                AddStudent();
            else
                UpdateStudent();
            
            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            _repository.UpdateStudent(Student);
        }

        private void AddStudent()
        {
            _repository.AddStudent(Student);
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);       
        }

        private void CloseWindow(Window window) 
        {
            window.Close();
        }


    }
}
