using System.Windows.Controls;

namespace TriviaGame
{
    /// <summary>
    /// Interaction logic for ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {
        public ResultsPage()
        {
            InitializeComponent();

            DataContext = new ResultsViewModel();
        }
    }
}
