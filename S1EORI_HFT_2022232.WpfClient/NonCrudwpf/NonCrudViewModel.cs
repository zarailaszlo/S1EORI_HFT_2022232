using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using S1EORI_HFT_2022232.Models.Statistics;
using S1EORI_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections;

namespace S1EORI_HFT_2022232.WpfClient.NonCrudwpf
{
    public class NonCrudViewModel : ObservableRecipient, INotifyPropertyChanged 
    {
        private NonCrudService _nonCrudService;

        public ObservableCollection<User> Users4 { get; set; }
        public ObservableCollection<User> Users5 { get; set; }
        public ObservableCollection<RepositoryStatistics> RepositoryStatistics { get; set; }
        public ObservableCollection<VisibilityGroupStatistics> VisibilityGroupStatistics { get; set; }
        public int RepositoryId { get; set; }
        public int UserAge { get; set; }
        public double CommitCount { get; set; }

        public ICommand GetCommitCountCommand { get; }
        public ICommand ReadRepositoryStatsCommand { get; }
        public ICommand GroupRepositoriesByVisibilityCommand { get; }
        public ICommand ReadUsersWithZeroRepositoriesCommand { get; }
        public ICommand ReadUsersOlderThanCommand { get; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        static RestService rest;
        public NonCrudViewModel()
        {
            if (!IsInDesignMode)
            {
                rest = new RestService("http://localhost:58986/", "User");
                _nonCrudService = new NonCrudService(rest);

                Users4 = new ObservableCollection<User>();
                Users5 = new ObservableCollection<User>();
                RepositoryStatistics = new ObservableCollection<RepositoryStatistics>();
                VisibilityGroupStatistics = new ObservableCollection<VisibilityGroupStatistics>();
                //1
                GetCommitCountCommand = new RelayCommand(() =>
                {
                    CommitCount = _nonCrudService.GetCommitCountForRepository(RepositoryId);
                    OnPropertyChanged("CommitCount");
                });
                //2
                ReadRepositoryStatsCommand = new RelayCommand(() =>
                {
                    var stats = _nonCrudService.ReadRepositoryStats();
                    foreach (var stat in stats)
                    {
                       RepositoryStatistics.Add(stat);
                       OnPropertyChanged("RepositoryStatistics");

                    }
                });
                //3
                GroupRepositoriesByVisibilityCommand = new RelayCommand(() => 
                {
                    var groups = _nonCrudService.GroupRepositoriesByVisibility();
                    foreach (var group in groups)
                    {
                        VisibilityGroupStatistics.Add(group);
                        OnPropertyChanged("VisibilityGroupStatistics");

                    }
                });
                //4
                ReadUsersWithZeroRepositoriesCommand = new RelayCommand(() => 
                {
                    var users = _nonCrudService.ReadUsersWithZeroRepositories();
                    foreach (var user in users)
                    {
                        Users4.Add(user);  
                        OnPropertyChanged("Users4");
                    }
                });
                //5
                ReadUsersOlderThanCommand = new RelayCommand(() => 
                {
                    var users = _nonCrudService.ReadUsersOlderThan(UserAge);
                    foreach (var user in users)
                    {
                        Users5.Add(user);
                        OnPropertyChanged("Users5");                        
                    }                    
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}