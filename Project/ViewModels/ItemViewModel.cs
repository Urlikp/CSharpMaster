using CSharpMaster.Support;

namespace CSharpMaster.ViewModels
{
    internal class ItemViewModel : ViewModelBase
    {
        private readonly string _itemIcon;
        private readonly string _itemName;
        private readonly ItemType _itemType;
        private readonly int _value;
        private int _count;

        public ItemViewModel(string icon, string name, ItemType type, int value = 0, int count = 1)
        {
            _itemIcon = icon;
            _itemName = name;
            _itemType = type;
            _value = value;
            _count = count;
        }

        public string ItemIcon => _itemIcon;
        public string ItemName => _itemName;
        public ItemType ItemType => _itemType;
        public int Value => _value;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged("Count");
                OnPropertyChanged("ItemDetail");
            }
        }

        public string ItemDetail
        {
            get
            {
                string detail;

                switch (_itemType)
                {
                case ItemType.Weapon:
                    detail = "Damage: " + _value;
                    break;
                case ItemType.Armor:
                    detail = "Armor: " + _value;
                    break;
                case ItemType.Bomb:
                case ItemType.Potion:
                default:
                    detail = "Count: " + _count;
                    break;
                }

                return detail;
            }
        }
    }

    public enum ItemType
    {
        Bomb,
        Potion,
        Weapon,
        Armor
    }
}
