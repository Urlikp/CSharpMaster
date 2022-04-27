using System.Windows;

namespace CSharpMaster.Views
{
    /// <summary>
    /// Interaction logic for DeathWindow.xaml
    /// </summary>
    public partial class DeathWindow : Window
    {
        public DeathWindow()
        {
            InitializeComponent();
        }

        private void AcceptDeathButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
