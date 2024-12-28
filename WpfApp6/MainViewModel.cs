using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp6
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool RedEnab = true;
        private bool AlphaEnab = true;
        private bool BlueEnab = true;
        private bool GreenEnabl = true;

        private byte red = 149;
        private byte alpha = 255;
        private byte blue = 115;
        private byte green = 125;

        public ObservableCollection<ColorEntry> Colors { get; set; } = new();

        public bool IsAlphaEnabled
        {
            get => AlphaEnab;
            set { AlphaEnab = value; OnPropertyChanged(); }
        }

        public bool IsRedEnabled
        {
            get => RedEnab;
            set { RedEnab = value; OnPropertyChanged(); }
        }

        public bool IsGreenEnabled
        {
            get => GreenEnabl;
            set { GreenEnabl = value; OnPropertyChanged(); }
        }

        public bool IsBlueEnabled
        {
            get => BlueEnab;
            set { BlueEnab = value; OnPropertyChanged(); }
        }

        public byte Alpha
        {
            get => alpha;
            set { alpha = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedColor)); OnPropertyChanged(nameof(HexCode)); }
        }

        public byte Red
        {
            get => red;
            set { red = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedColor)); OnPropertyChanged(nameof(HexCode)); }
        }

        public byte Green
        {
            get => green;
            set { green = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedColor)); OnPropertyChanged(nameof(HexCode)); }
        }

        public byte Blue
        {
            get => blue;
            set { blue = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedColor)); OnPropertyChanged(nameof(HexCode)); }
        }

        public SolidColorBrush SelectedColor => new SolidColorBrush(Color.FromArgb(Alpha, Red, Green, Blue));

        public string HexCode => $"#{Alpha:X2}{Red:X2}{Green:X2}{Blue:X2}";

        public ICommand AddColorCommand { get; }
        public ICommand RemoveColorCommand { get; }

        public MainViewModel()
        {
            AddColorCommand = new RelayCommand(AddColor, CanAddColor);
            RemoveColorCommand = new RelayCommand(RemoveColor);
        }

        private void AddColor()
        {
            Colors.Add(new ColorEntry { ColorCode = HexCode, Brush = SelectedColor });
        }

        private bool CanAddColor()
        {
            return !Colors.Any(c => c.ColorCode == HexCode);
        }

        private void RemoveColor(object parameter)
        {
            if (parameter is ColorEntry color)
            {
                Colors.Remove(color);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
