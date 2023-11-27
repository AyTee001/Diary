using Diary.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Diary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new RecordsViewModel();
            Title.TextChanged += CreateBtn_TextChanged;
            TextContent.TextChanged += CreateBtn_TextChanged;
            TitleMod.TextChanged += ModificationBtns_TextChanged;
            TextContentMod.TextChanged += ModificationBtns_TextChanged;
        }

        private void CreateBtn_TextChanged(object sender, RoutedEventArgs e)
        {
            bool isTitleValid = !Validation.GetHasError(Title) && Title.IsEnabled && !string.IsNullOrWhiteSpace(Title.Text);
            bool isTextContentValid = (!Validation.GetHasError(TextContent) && TextContent.IsEnabled && !string.IsNullOrWhiteSpace(TextContent.Text)) || !TextContent.IsEnabled;
            CreateBtn.IsEnabled = isTitleValid && isTextContentValid;
        }

        private void ModificationBtns_TextChanged(object sender, RoutedEventArgs e)
        {
            bool isIdValid = !Validation.GetHasError(Id);
            bool isTitleValid = !Validation.GetHasError(TitleMod) && TitleMod.IsEnabled && !string.IsNullOrWhiteSpace(TitleMod.Text);
            bool isTextContentValid = (!Validation.GetHasError(TextContentMod) && TextContentMod.IsEnabled && !string.IsNullOrWhiteSpace(TextContentMod.Text)) || !TextContentMod.IsEnabled;
            UpdateBtn.IsEnabled = isTitleValid && isTextContentValid && isIdValid;
            DeleteBtn.IsEnabled = isTitleValid && isTextContentValid && isIdValid;
        }
    }
}