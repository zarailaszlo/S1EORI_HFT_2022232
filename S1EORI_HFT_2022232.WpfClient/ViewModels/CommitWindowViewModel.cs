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
    public class CommitWindowViewModel : ObservableRecipient
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Commit> Commits { get; set; }

        private Commit selectedCommit;
        public Commit SelectedCommit
        {
            get { return selectedCommit; }
            set
            {
                if (value != null)
                {
                    //SetProperty(ref selectedCommit, new Commit()
                    selectedCommit = new Commit()
                    {
                        IdCommit = value.IdCommit,
                        Hash = value.Hash,
                        Message = value.Message,
                        CommittedDate = value.CommittedDate,
                        GitRepositoryId = value.GitRepositoryId,
                        UserId = value.UserId,

                    };
                    OnPropertyChanged();
                    (DeleteCommitCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateCommitCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public ICommand CreateCommitCommand { get; set; }

        public ICommand DeleteCommitCommand { get; set; }

        public ICommand UpdateCommitCommand { get; set; }
        public CommitWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Commits = new RestCollection<Commit>("http://localhost:58988/", "Commit", "hub");

                CreateCommitCommand = new RelayCommand(() =>
                {
                    Commits.Add(new Commit()
                    {
                        IdCommit = SelectedCommit.IdCommit,
                        Hash = SelectedCommit.Hash,
                        Message = SelectedCommit.Message,
                        CommittedDate = SelectedCommit.CommittedDate,
                        GitRepositoryId = SelectedCommit.GitRepositoryId,
                        UserId = SelectedCommit.UserId,
                    });
                });
                //nem működik
                UpdateCommitCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Commits.Update(SelectedCommit);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });

                DeleteCommitCommand = new RelayCommand(() =>
                {
                    Commits.Delete(SelectedCommit.IdCommit);
                }
                , () =>
                {
                    return SelectedCommit != null;
                }
                );
                SelectedCommit = new Commit();
            }
        }
    }   
}
