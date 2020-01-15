using System.Windows.Controls;

namespace TriviaGame.Pages
{
    /// <summary>
    /// Interaction logic for HighscoresPage.xaml
    /// </summary>
    public partial class HighscoresPage : Page
    {
        public HighscoresPage()
        {
            InitializeComponent();

            DataContext = new HighscoresViewModel();
        }
    }
}
