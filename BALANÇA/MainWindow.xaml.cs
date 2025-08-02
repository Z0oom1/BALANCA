using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BALANÇA
{
    public partial class MainWindow : Window
    {
        // Guarda as instâncias das páginas para não ter que recriá-las toda hora
        private readonly Page pageRecebimento;
        // Futuramente, podemos adicionar as outras páginas aqui
        // private readonly Page pageCarregamento;
        // private readonly Page pageConfiguracoes;

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.CanResize;

            // Cria apenas a página que já existe
            pageRecebimento = new RecebimentoPage();

            // Carrega a página inicial
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

                    // Para todos os outros botões, apenas mostramos uma mensagem
                    case "CarregamentoRadio":
                    case "FilasRadio":
                    case "CadastrosRadio":
                    case "RelatoriosRadio":
                    case "DadosRadio":
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

        // Permite arrastar a janela
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}   