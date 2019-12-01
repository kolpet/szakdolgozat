using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Szakdolgozat.View
{
    /// <summary>
    /// Interaction logic for AlgorithmView.xaml
    /// </summary>
    public partial class AlgorithmView : UserControl
    {
        Regex intRegex, doubleRegex;

        public AlgorithmView()
        {
            InitializeComponent();
            intRegex = new Regex(@"[0-9]+");
            doubleRegex = new Regex(@"[0-9]+(\.[0-9]*)?");
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !intRegex.IsMatch(e.Text);
        }

        private void DoubleValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !doubleRegex.IsMatch(e.Text);
        }
    }
}
