using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using S1EORI_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace S1EORI_HFT_2022232.WpfClient.ViewModels
{
    public class GitRepositoryWindowViewModel : ObservableRecipient
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<GitRepository> GitRepositories { get; set; }
        private GitRepository selectedGitRepository;
        public GitRepository SelectedGitRepository
        {
            get { return selectedGitRepository; }
            set
            {
                if (value != null)
                {
                    selectedGitRepository = new GitRepository()
                    {
                        IdGitRepository = value.IdGitRepository,
                        Name = value.Name,
                        Visibility = value.Visibility,
                        CreatedDate = value.CreatedDate,
                        UserId = value.UserId,
                        
                    };
                    OnPropertyChanged();
                    (DeleteGitRepositoryCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateGitRepositoryCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public ICommand CreateGitRepositoryCommand { get; set; }

        public ICommand DeleteGitRepositoryCommand { get; set; }

        public ICommand UpdateGitRepositoryCommand { get; set; }
        public GitRepositoryWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                GitRepositories = new RestCollection<GitRepository>("http://localhost:58988/", "GitRepository", "hub");
                CreateGitRepositoryCommand = new RelayCommand(() =>
                {
                    GitRepositories.Add(new GitRepository()
                    {
                        IdGitRepository = SelectedGitRepository.IdGitRepository,
                        Name = SelectedGitRepository.Name,
                        Visibility = SelectedGitRepository.Visibility,
                        CreatedDate = SelectedGitRepository.CreatedDate,
                        UserId = SelectedGitRepository.UserId,
                                                
                    });
                });
                //nem működik
                UpdateGitRepositoryCommand = new RelayCommand(() =>
                {
                    try
                    {
                        GitRepositories.Update(SelectedGitRepository);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });

                DeleteGitRepositoryCommand = new RelayCommand(() =>
                {
                    GitRepositories.Delete(SelectedGitRepository.IdGitRepository);
                }
                , () =>
                {
                    return SelectedGitRepository != null;
                }
                );
                SelectedGitRepository = new GitRepository();
            }
        }

    }
}
