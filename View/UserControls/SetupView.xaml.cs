using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Szakdolgozat.View.UserControls
{
    /// <summary>
    /// Interaction logic for SetupView.xaml
    /// </summary>
    public partial class SetupView : UserControl
    {
        Regex regex;

        public SetupView()
        {
            InitializeComponent();
            regex = new Regex("[0-9]+");
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
