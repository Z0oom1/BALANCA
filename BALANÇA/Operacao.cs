using System.ComponentModel;
using System.Windows.Media;
using System.Globalization;

namespace BALANÇA
{
    public class Operacao : INotifyPropertyChanged
    {
        private Brush _situacaoCor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#cc1010")); // Começa como Vermelho ("Chegou")

        public bool IsSelected { get; set; }
        public string DataPassagem { get; set; } = string.Empty;
        public string MateriaPrima { get; set; } = string.Empty;
        public string PlacaVeiculo { get; set; } = string.Empty;
        public string DocaEmbarque { get; set; } = string.Empty;
        public string HoraChegada { get; set; } = string.Empty;
        public string HoraEntrada { get; set; } = string.Empty;

        private string _pesoTara = string.Empty;
        public string PesoTara
        {
            get => _pesoTara;
            set
            {
                if (_pesoTara != value)
                {
                    _pesoTara = value;
                    OnPropertyChanged(nameof(PesoTara));
                    OnPropertyChanged(nameof(PesoLiquido));
                    OnPropertyChanged(nameof(DifKg));
                }
            }
        }

        private string _pesoBruto = string.Empty;
        public string PesoBruto
        {
            get => _pesoBruto;
            set
            {
                if (_pesoBruto != value)
                {
                    _pesoBruto = value;
                    OnPropertyChanged(nameof(PesoBruto));
                    OnPropertyChanged(nameof(PesoLiquido));
                    OnPropertyChanged(nameof(DifKg));
                }
            }
        }

        private string _pesoNF = string.Empty;
        public string PesoNF
        {
            get => _pesoNF;
            set
            {
                if (_pesoNF != value)
                {
                    _pesoNF = value;
                    OnPropertyChanged(nameof(PesoNF));
                    OnPropertyChanged(nameof(DifKg));
                }
            }
        }

        // Adiciona setter para permitir binding TwoWay sem erro
        private string _pesoLiquido = string.Empty;
        public string PesoLiquido
        {
            get
            {
                double bruto = ParseDouble(PesoBruto);
                double tara = ParseDouble(PesoTara);
                double liquido = bruto - tara;
                _pesoLiquido = liquido.ToString("F2", CultureInfo.InvariantCulture);
                return _pesoLiquido;
            }
            set
            {
                if (_pesoLiquido != value)
                {
                    _pesoLiquido = value;
                    OnPropertyChanged(nameof(PesoLiquido));
                    OnPropertyChanged(nameof(DifKg));
                }
            }
        }

        public string DifKg
        {
            get
            {
                double liquido = ParseDouble(PesoLiquido);
                double nf = ParseDouble(PesoNF);
                double dif = liquido - nf;
                return dif.ToString("F2", CultureInfo.InvariantCulture);
            }
        }

        public string DifPercent
        {
            get
            {
                double nf = ParseDouble(PesoNF);
                double dif = ParseDouble(DifKg);
                if (nf == 0) return "0";
                double percent = (dif / nf) * 100.0;
                return percent.ToString("F2", CultureInfo.InvariantCulture);
            }
        }

        public string Rover { get; set; } = string.Empty;
        public string HoraSaida { get; set; } = string.Empty;
        public string NotaFiscal { get; set; } = string.Empty;

        private bool _isSituacaoPopupOpen;
        public bool IsSituacaoPopupOpen
        {
            get => _isSituacaoPopupOpen;
            set { _isSituacaoPopupOpen = value; OnPropertyChanged(nameof(IsSituacaoPopupOpen)); }
        }

        public Brush SituacaoCor
        {
            get => _situacaoCor;
            set { _situacaoCor = value; OnPropertyChanged(nameof(SituacaoCor)); }
        }

        private double ParseDouble(string value)
        {
            double.TryParse(value.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double result);
            return result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Operacao Clone()
        {
            // MemberwiseClone cria uma cópia superficial, que é suficiente para este caso,
            // pois a maioria das propriedades são tipos de valor (string, bool, etc.).
            // Para o Brush, que é um tipo de referência, criamos uma nova instância.
            var clone = (Operacao)this.MemberwiseClone();
            clone.SituacaoCor = new SolidColorBrush(((SolidColorBrush)this.SituacaoCor).Color);
            return clone;
        }
    }
}
