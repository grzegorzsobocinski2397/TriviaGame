using System.Windows.Controls;

namespace TriviaGame
{
    /// <summary>
    /// Interaction logic for FinalScorePage.xaml
    /// </summary>
    public partial class FinalScorePage : Page
    {
        public FinalScorePage()
        {
            InitializeComponent();

            DataContext = new FinalViewModel();
        }
    }
}
