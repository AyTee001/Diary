using System.Windows;

namespace Diary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Uri _recordsURI = new("/Records.xaml", UriKind.Relative);

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Source = _recordsURI;
        }
    }
}