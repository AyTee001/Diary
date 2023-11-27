using Diary.ViewModels;
using System.Windows.Controls;

namespace Diary
{
    /// <summary>
    /// Interaction logic for Records.xaml
    /// </summary>
    public partial class Records : Page
    {
        public Records()
        {
            InitializeComponent();
            DataContext = new RecordsViewModel();
        }
    }
}
