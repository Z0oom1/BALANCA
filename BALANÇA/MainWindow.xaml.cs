using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BALANÇA
{
    public partial class MainWindow : Window
    {
        private readonly Page pageRecebimento;
        // private readonly Page pageConfiguracoes; // Futuramente

        public MainWindow()
        {
            InitializeComponent();
            pageRecebimento = new RecebimentoPage();
            MainFrame.Content = pageRecebimento;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton button)
            {
                switch (button.Name)
                {
                    case "RecebimentoRadio":
                        MainFrame.Navigate(pageRecebimento);
                        break;
                    case "CarregamentoRadio":
                    case "ConfiguracoesRadio":
                        MessageBox.Show($"Página '{button.Name.Replace("Radio", "")}' ainda não implementada.");
                        break;
                }
            }
        }

        // --- LÓGICA PARA OS BOTÕES DA JANELA ---
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Permite arrastar a janela clicando em qualquer ponto (neste caso, na borda)
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}