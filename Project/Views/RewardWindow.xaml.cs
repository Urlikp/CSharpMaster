using System.Windows;

namespace CSharpMaster.Views
{
    /// <summary>
    /// Interaction logic for RewardWindow.xaml
    /// </summary>
    public partial class RewardWindow : Window
    {
        private readonly string _itemIcon;
        private readonly string _itemName;
        private readonly string _itemDetail;

        public RewardWindow(string itemIcon, string itemName, string itemDetail)
        {
            _itemIcon = itemIcon;
            _itemName = itemName;
            _itemDetail = itemDetail;
            InitializeComponent();
        }

        public string ItemIcon => _itemIcon;
        public string ItemName => _itemName;
        public string ItemDetail => _itemDetail;

        private void AcceptRewardButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
