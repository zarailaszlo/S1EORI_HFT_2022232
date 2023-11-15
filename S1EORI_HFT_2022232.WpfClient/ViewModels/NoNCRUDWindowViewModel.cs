using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using S1EORI_HFT_2022232.WpfClient.NonCrudwpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace S1EORI_HFT_2022232.WpfClient.ViewModels
{
    public class NoNCRUDWindowViewModel : ObservableRecipient
    {
        public ICommand GetCommitCountForRepositoryCommand { get; }
        public ICommand ReadRepositoryStatsCommand { get; }
        public ICommand GroupRepositoriesByVisibilityCommand { get; }
        public ICommand ReadUsersWithZeroRepositoriesCommand { get; }
        public ICommand ReadUsersOlderThanCommand { get; }

        public NoNCRUDWindowViewModel()
        {
            GetCommitCountForRepositoryCommand = new RelayCommand(
                () => new GetCommitCountForRepositoryWindow().ShowDialog()
                );

            ReadRepositoryStatsCommand = new RelayCommand(
                () => new ReadRepositoryStatsWindow().ShowDialog()
                );

            GroupRepositoriesByVisibilityCommand = new RelayCommand(
                () => new GroupRepositoriesByVisibilityWindow().ShowDialog()
                );

            ReadUsersWithZeroRepositoriesCommand = new RelayCommand(
                () => new ReadUsersWithZeroRepositoriesWindow().ShowDialog()
                );

            ReadUsersOlderThanCommand = new RelayCommand(
                () => new ReadUsersOlderThanWindow().ShowDialog()
                );
        }            
    }
}
