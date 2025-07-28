using System.ComponentModel;
using System.Windows.Media;

namespace BALANÇA
{
    public class Operacao : INotifyPropertyChanged
    {
        private Brush _situacaoCor = Brushes.Tomato; // Começa como Vermelho (Chegou)

        public bool IsSelected { get; set; }
        public string DataPassagem { get; set; } = string.Empty;
        // ... (outras propriedades) ...

        public Brush SituacaoCor
        {
            get { return _situacaoCor; }
            set
            {
                _situacaoCor = value;
                OnPropertyChanged(nameof(SituacaoCor)); // Notifica a tela da mudança de cor
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}