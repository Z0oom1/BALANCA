using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BALANÇA
{
    public partial class RecebimentoPage : Page
    {
        public ObservableCollection<Operacao> Operacoes { get; set; }

        public RecebimentoPage()
        {
            InitializeComponent();
            Operacoes = new ObservableCollection<Operacao>();

            Operacoes.Add(new Operacao { DataPassagem = "27/07/2025", MateriaPrima = "Tomate", PlacaVeiculo = "ABC-1234", SituacaoCor = Brushes.LightGreen });

            this.DataContext = this;
        }

        private void AdicionarButton_Click(object sender, RoutedEventArgs e)
        {
            Operacoes.Add(new Operacao { DataPassagem = DateTime.Now.ToString("dd/MM/yyyy") });
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            var itensParaRemover = Operacoes.Where(op => op.IsSelected).ToList();
            if (itensParaRemover.Any())
            {
                foreach (var item in itensParaRemover)
                {
                    Operacoes.Remove(item);
                }
            }
            else
            {
                MessageBox.Show("Selecione uma ou mais linhas para remover.", "Aviso");
            }
        }

        // MÉTODO QUE ESTAVA FALTANDO
        private void BuscaInteligenteButton_Click(object sender, RoutedEventArgs e)
        {
            // A lógica para abrir a janela de busca virá aqui
            MessageBox.Show("Função de Busca Inteligente ainda não implementada.");
        }

        private void MudarSituacao_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.DataContext is Operacao operacao)
            {
                string cor = menuItem.Tag.ToString();
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
}