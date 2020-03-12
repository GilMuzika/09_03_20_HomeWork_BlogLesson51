using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;

namespace _09_03_20_HomeWork_BlogLesson51
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        private Brush _brushForBorder = Brushes.Gray;
        public Brush BrushForBorder
        {
            get => _brushForBorder;
            set
            {
                if (_brushForBorder == value) return;
                _brushForBorder = value;
                OnPropertyChanged("BrushForBorder");
            }
        }
        private bool _changingBrushFlag = false;
        public bool ChangingBrushFlag
        {
            get => _changingBrushFlag;
            set
            {
                if (_changingBrushFlag == value) return;
                _changingBrushFlag = value;
                OnPropertyChanged("ChangingBrushFlag");
            }
        }
        #endregion Properties

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private void ChangeChangingBrushFlag()
        {
            ChangingBrushFlag = ChangingBrushFlag ? false : true;
        }
        private List<Brush> GetBrushes()
        {
            BrushConverter locBrushConverter = new BrushConverter();

            List<Brush> brushes = new List<Brush>();
            foreach(var s in typeof(Brushes).GetProperties())
            {

                if(s.PropertyType.Name == "SolidColorBrush")
                {
                    brushes.Add((Brush)locBrushConverter.ConvertFromString(s.Name));
                }
            }
            return brushes;
        }
        public async Task<Brush> RuffleColors()
        {                        
            return await Task.Run(() =>
            {     
                

                Random locRnd = new Random();
                List<Brush> locBrushes = GetBrushes();
                int count = 0;
                Timer locTimer = new Timer();
                locTimer.Interval = 10;
                locTimer.Elapsed += (object sender, ElapsedEventArgs e) => 
                    {
                        
                        if (count == 10) 
                        {
                            BrushForBorder = locBrushes[locRnd.Next(0, locBrushes.Count - 1)];                            
                            ChangeChangingBrushFlag();
                            count = 0;
                        }
                        if (count == 5) ChangeChangingBrushFlag();

                        count++;                        
                    };
                locTimer.Start();
                return BrushForBorder;
            });
        }



    }
}
