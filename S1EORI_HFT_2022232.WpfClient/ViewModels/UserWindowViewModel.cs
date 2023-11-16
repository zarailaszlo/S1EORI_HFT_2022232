using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using S1EORI_HFT_2022232.Models;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace S1EORI_HFT_2022232.WpfClient.ViewModels
{
    public class UserWindowViewModel : ObservableRecipient
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<User> Users { get; set; }
        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (value != null)
                {
                    selectedUser = new User()
                    {
                        IdUser = value.IdUser,
                        Username = value.Username,
                        Password = value.Password,
                        FullName = value.FullName,
                        Email = value.Email,
                        Age = value.Age,
                    };
                    OnPropertyChanged();
                    (DeleteUserCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateUserCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public ICommand CreateUserCommand { get; set; }

        public ICommand DeleteUserCommand { get; set; }

        public ICommand UpdateUserCommand { get; set; }
        public UserWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Users = new RestCollection<User>("http://localhost:58988/", "user", "hub");
                
                CreateUserCommand = new RelayCommand(() =>
                {
                    Users.Add(new User()
                    {
                        IdUser = SelectedUser.IdUser,
                        Username = SelectedUser.Username,
                        Password = SelectedUser.Password,
                        FullName = SelectedUser.FullName,
                        Email = SelectedUser.Email,
                        Age = SelectedUser.Age
                    });                    
                });
                UpdateUserCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Users.Update(SelectedUser);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });

                DeleteUserCommand = new RelayCommand(() =>
                {
                    Users.Delete(SelectedUser.IdUser);
                }
                , () =>
                {
                    return SelectedUser != null;
                }
                );
                SelectedUser = new User();
            }
        }

    }
}
