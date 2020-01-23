using System.Windows.Controls;

namespace TriviaGame
{
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        public FirstPage()
        {
            InitializeComponent();

            DataContext = new FirstPageViewModel();
        }
    }
}
