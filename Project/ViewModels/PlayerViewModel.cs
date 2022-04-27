using CSharpMaster.Support;

namespace CSharpMaster.ViewModels
{
    internal class PlayerViewModel : ViewModelBase
    {
        private const string _playerIcon = "pack://application:,,,/Images/player.png";
        private int _maxHealth = 10;
        private int _currentHealth = 10;
        private int _currentExperience = 0;
        private const int _maxExperience = 100;
        private int _upgradesRemaining = 0;
        private int _healthLevel = 1;
        private int _damageLevel = 1;
        private int _armorLevel = 1;
        private int _damage = 1;
        private int _armor = 1;
        private const int Percent = 100;
        private int _monstersKilled = 0;
        private ItemViewModel _equippedWeapon = new ItemViewModel("pack://application:,,,/Images/Weapons/fist.png", "Fists", ItemType.Weapon, 2);
        private ItemViewModel _equippedArmor = new ItemViewModel("pack://application:,,,/Images/Armor/leather_armor.png", "Leather Armor", ItemType.Armor, 1);

        public string PlayerIcon => _playerIcon;

        public int MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                OnPropertyChanged("MaxHealth");
            }
        }

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                OnPropertyChanged("HealthPercentage");
                OnPropertyChanged("CurrentHealth");
            }
        }

        public int HealthPercentage
        {
            get => Percent * _currentHealth / _maxHealth;
            set { }
        }

        public int CurrentExperience
        {
            get => _currentExperience;
            set
            {
                _currentExperience = value;
                OnPropertyChanged("CurrentExperience");
            }
        }

        public int MaxExperience => _maxExperience;

        public int UpgradesRemaining
        {
            get => _upgradesRemaining;
            set
            {
                _upgradesRemaining = value;
                OnPropertyChanged("UpgradesRemaining");
            }
        }

        public int HealthLevel
        {
            get => _healthLevel;
            set
            {
                _healthLevel = value;
                OnPropertyChanged("HealthLevel");
            }
        }

        public int DamageLevel
        {
            get => _damageLevel;
            set
            {
                _damageLevel = value;
                OnPropertyChanged("DamageLevel");
            }
        }

        public int ArmorLevel
        {
            get => _armorLevel;
            set
            {
                _armorLevel = value;
                OnPropertyChanged("ArmorLevel");
            }
        }

        public int Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                OnPropertyChanged("Damage");
            }
        }

        public int Armor
        {
            get => _armor;
            set
            {
                _armor = value;
                OnPropertyChanged("Armor");
            }
        }

        public int MonstersKilled
        {
            get => _monstersKilled;
            set
            {
                _monstersKilled = value;
                OnPropertyChanged("MonstersKilled");
            }
        }

        public ItemViewModel EquippedWeapon
        {
            get => _equippedWeapon;
            set
            {
                _equippedWeapon = value;
                OnPropertyChanged("EquippedWeapon");
            }
        }

        public ItemViewModel EquippedArmor
        {
            get => _equippedArmor;
            set
            {
                _equippedArmor = value;
                OnPropertyChanged("EquippedArmor");
            }
        }
    }
}
