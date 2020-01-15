using System.Windows.Controls;

namespace TriviaGame.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();

            DataContext = new UserViewModel();
        }
    }
}
