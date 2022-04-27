using CSharpMaster.Support;

namespace CSharpMaster.ViewModels
{
    internal class MonsterViewModel : ViewModelBase
    {
        private readonly string _monsterIcon;
        private readonly int _maxHealth;
        private int _currentHealth;
        private readonly int _damage;
        private readonly int _armor;
        private const int Percent = 100;

        public MonsterViewModel(string icon, int health, int damage, int armor)
        {
            _monsterIcon = icon;
            _maxHealth = health;
            _currentHealth = health;
            _damage = damage;
            _armor = armor;
        }

        public string MonsterIcon => _monsterIcon;

        public int MaxHealth => _maxHealth;

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

        public int Damage => _damage;

        public int Armor => _armor;
    }
}
