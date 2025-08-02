using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Input; // Certifique-se de que esta linha está presente

namespace BALANÇA
{
    public partial class RecebimentoPage : Page
    {
        public ObservableCollection<Operacao> Operacoes { get; set; }

        public RecebimentoPage()
        {
            InitializeComponent();
            Operacoes = new ObservableCollection<Operacao>();
            Operacoes.Add(new Operacao { DataPassagem = "27/07/2025", MateriaPrima = "Tomate", PlacaVeiculo = "ABC-1234" });
            Operacoes.Add(new Operacao { DataPassagem = "27/07/2025", MateriaPrima = "Lenha", PlacaVeiculo = "DEF-5678" });
            this.DataContext = this;

            this.Loaded += RecebimentoPage_Loaded;
        }

        private void RecebimentoPage_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
                window.StateChanged += RecebimentoPage_StateChanged;
        }

        private void RecebimentoPage_StateChanged(object? sender, EventArgs e)
        {
            if (Window.GetWindow(this)?.WindowState == WindowState.Minimized)
            {
                foreach (var op in Operacoes)
                    op.IsSituacaoPopupOpen = false;
            }
        }

        private void AdicionarButton_Click(object sender, RoutedEventArgs e)
        {
            Operacoes.Add(new Operacao { DataPassagem = DateTime.Now.ToString("dd/MM/yyyy") });
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            var itensParaRemover = Operacoes.Where(op => op != null && op.IsSelected).ToList();
            if (itensParaRemover.Any())
            {
                foreach (var item in itensParaRemover)
                {
                    if (item != null)
                        Operacoes.Remove(item);
                }
            }
            else
            {
                MessageBox.Show("Selecione uma ou mais linhas com o checkbox para remover.", "Aviso");
            }
        }

        private void BuscaInteligenteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Função de Busca Inteligente ainda não implementada.");
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var popup = button.FindName("ColorPopup") as Popup;
                if (popup != null)
                {
                    popup.IsOpen = true;
                }
            }
        }

        private void MudarSituacao_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                var operacao = menuItem.DataContext as Operacao;
                if (operacao != null)
                {
                    string? cor = menuItem.Tag?.ToString();
                    switch (cor)
                    {
                        case "Red":
                            operacao.SituacaoCor = Brushes.Tomato;
                            break;
                        case "Yellow":
                            operacao.SituacaoCor = Brushes.Gold;
                            break;
                        case "Green":
                            operacao.SituacaoCor = Brushes.LightGreen;
                            break;
                    }
                }
            }
        }

        // Corrigido: método existe e assinatura correta para o XAML
        private void SituacaoBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Implemente a lógica desejada aqui, por exemplo:
            // Alternar a propriedade IsSituacaoPopupOpen do item de dados associado

            var border = sender as FrameworkElement;
            if (border?.DataContext is Operacao operacao)
            {
                operacao.IsSituacaoPopupOpen = !operacao.IsSituacaoPopupOpen;
            }
        }

        private void SituacaoCor_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var operacao = button.DataContext as Operacao;
                if (operacao != null)
                {
                    switch (button.Tag?.ToString())
                    {
                        case "Vermelho":
                            operacao.SituacaoCor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#cc1010"));
                            break;
                        case "Amarelo":
                            operacao.SituacaoCor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffe100"));
                            break;
                        case "Verde":
                            operacao.SituacaoCor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00cc10"));
                            break;
                    }
                    operacao.IsSituacaoPopupOpen = false;
                }
            }
        }
    }
}