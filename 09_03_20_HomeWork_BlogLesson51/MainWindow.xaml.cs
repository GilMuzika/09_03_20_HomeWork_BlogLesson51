using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _09_03_20_HomeWork_BlogLesson51
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel = new ViewModel();

        public MainWindow()
        {            
            InitializeComponent();
            DataContext = _viewModel;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.RuffleColors();
            

            await Task.Run(() => 
            {
                Timer locTimer = new Timer();
                locTimer.Interval = 500;
                locTimer.Elapsed += (object sender, ElapsedEventArgs e) => 
                {
                    SafeInvoke(() => { brdBorder.Background = _viewModel.BrushForBorder; });
                };
                locTimer.Start();
                
                
            });
        }

        private void SafeInvoke(Action work)
        {
            if (Dispatcher.CheckAccess())
            {
                work.Invoke();
                return;
            }
            this.Dispatcher.BeginInvoke(work);
        }
    }
}
