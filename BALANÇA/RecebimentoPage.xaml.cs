using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BALANÇA
{
    public partial class RecebimentoPage : Page
    {
        public ObservableCollection<Operacao> Operacoes { get; set; }

        // --- LÓGICA DE UNDO/REDO ---
        private readonly Stack<List<Operacao>> undoStack = new Stack<List<Operacao>>();
        private readonly Stack<List<Operacao>> redoStack = new Stack<List<Operacao>>();
        private bool isUndoRedoOperation = false;

        public RecebimentoPage()
        {
            InitializeComponent();
            Operacoes = new ObservableCollection<Operacao>();
            Operacoes.Add(new Operacao { DataPassagem = "27/07/2025", MateriaPrima = "Tomate", PlacaVeiculo = "ABC-1234" });
            Operacoes.Add(new Operacao { DataPassagem = "27/07/2025", MateriaPrima = "Lenha", PlacaVeiculo = "DEF-5678" });
            this.DataContext = this;

            PushStateToUndoStack(); // Guarda o estado inicial
        }

        // --- Métodos de Controlo do Estado ---

        private void PushStateToUndoStack()
        {
            var snapshot = Operacoes.Select(op => op.Clone()).ToList();
            undoStack.Push(snapshot);
            redoStack.Clear();
        }

        private void RestoreState(List<Operacao> state)
        {
            isUndoRedoOperation = true;
            Operacoes.Clear();
            foreach (var op in state)
            {
                Operacoes.Add(op);
            }
            isUndoRedoOperation = false;
        }

        // --- Definições dos Métodos em Falta ---

        public void Undo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = undoStack.Count > 1;
        }

        public void Undo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var currentState = undoStack.Pop();
            redoStack.Push(currentState);
            var previousState = undoStack.Peek();
            RestoreState(previousState);

            // ===============================================
            // ADIÇÃO: Devolve o foco à tabela após a operação.
            // ===============================================
            RecebimentoDataGrid.Focus();
        }

        public void Redo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = redoStack.Count > 0;
        }

        public void Redo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var futureState = redoStack.Pop();
            undoStack.Push(futureState);
            RestoreState(futureState);

            // ===============================================
            // ADIÇÃO: Devolve o foco à tabela após a operação.
            // ===============================================
            RecebimentoDataGrid.Focus();
        }

        public void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!isUndoRedoOperation)
            {
                Dispatcher.BeginInvoke(new Action(() => PushStateToUndoStack()), System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        // --- Eventos dos Botões ---

        private void AdicionarButton_Click(object sender, RoutedEventArgs e)
        {
            Operacoes.Add(new Operacao { DataPassagem = DateTime.Now.ToString("dd/MM/yyyy") });
            PushStateToUndoStack();
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
                PushStateToUndoStack();
            }
            else
            {
                MessageBox.Show("Selecione uma ou mais linhas para remover.", "Aviso");
            }
        }

        // --- Outros Métodos (mantidos para compatibilidade) ---
        private void BuscaInteligenteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Função de Busca Inteligente ainda não implementada.");
        }

        private void SituacaoBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Operacao operacao)
            {
                operacao.IsSituacaoPopupOpen = !operacao.IsSituacaoPopupOpen;
            }
        }

        private void SituacaoCor_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Operacao operacao && sender is Button button)
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