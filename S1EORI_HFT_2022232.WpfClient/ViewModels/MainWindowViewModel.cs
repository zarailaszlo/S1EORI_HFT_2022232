using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace S1EORI_HFT_2022232.WpfClient.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand UserCommand { get; set; }
        public ICommand GitRepositoryCommand { get; set; }
        public ICommand CommitCommand { get; set; }
        public ICommand NoNCRUDCommand { get; set; }

        public MainWindowViewModel()
        {
            UserCommand = new RelayCommand(
                () => new UserWindow().ShowDialog()
                );

            GitRepositoryCommand = new RelayCommand(
                () => new GitRepositoryWindow().ShowDialog()
                );

            CommitCommand = new RelayCommand(
                () => new CommitWindow().ShowDialog()
                );

            NoNCRUDCommand = new RelayCommand(
                () => new NoNCRUDWindow().ShowDialog()
                );
        }
    }
}
