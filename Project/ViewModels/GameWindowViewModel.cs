using CSharpMaster.Support;
using CSharpMaster.Views;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace CSharpMaster.ViewModels
{
    internal class GameWindowViewModel : ViewModelBase
    {
        private RelayCommand _upgradeHealthCommand;
        private RelayCommand _upgradeDamageCommand;
        private RelayCommand _upgradeArmorCommand;

        private RelayCommand _attackCommand;
        private RelayCommand _useItemCommand;
        private RelayCommand _equipItemCommand;

        private ItemViewModel _selectedItem;
        private readonly PlayerViewModel _player = new PlayerViewModel();
        private MonsterViewModel _monster;

        private const string _backGroundIcon = "pack://application:,,,/Images/bg.png";
        private const string _bloodEffectIcon = "pack://application:,,,/Images/effect.png";

        private int _rewardWeapon = 0;
        private int _rewardArmor = 0;

        private const int _armorOffset = 0;
        private const int _consumablesOffset = 2;
        private const int _weaponsOffset = 4;

        public ObservableCollection<ItemViewModel> ItemList { get; set; } = new ObservableCollection<ItemViewModel>();

        private readonly List<string> _weaponNames = new List<string>
        {
            "Axe",
            "Machete",
            "Sword"
        };
        private readonly List<int> _weaponValues = new List<int>
        {
            5,
            10,
            20
        };
        private readonly List<string> _armorNames = new List<string>
        {
            "Chain Mail",
            "Plate Armor"
        };
        private readonly List<int> _armorValues = new List<int>
        {
            5,
            10
        };
        private readonly List<string> _consumableNames = new List<string>
        {
            "Bomb",
            "Health Potion"
        };
        private readonly ObservableCollection<string> _monsterIcons = new ObservableCollection<string>
        {
            "pack://application:,,,/Images/Monsters/devourer.png",
            "pack://application:,,,/Images/Monsters/goblin.png",
            "pack://application:,,,/Images/Monsters/chest.png",
            "pack://application:,,,/Images/Monsters/skeleton.png",
            "pack://application:,,,/Images/Monsters/spider.png"
        };
        private readonly ObservableCollection<string> _itemIcons = new ObservableCollection<string>
        {
            "pack://application:,,,/Images/Armor/chain_mail.png",
            "pack://application:,,,/Images/Armor/plate_armor.png",
            "pack://application:,,,/Images/Consumables/bomb.png",
            "pack://application:,,,/Images/Consumables/potion.png",
            "pack://application:,,,/Images/Weapons/axe.png",
            "pack://application:,,,/Images/Weapons/machete.png",
            "pack://application:,,,/Images/Weapons/sword.png"
        };
        private readonly List<ItemType> _itemTypeValues = new List<ItemType>((ItemType[])Enum.GetValues(typeof(ItemType)));


        private readonly Random _random = new Random();

        public GameWindowViewModel()
        {
            int monsterIndex = _random.Next(0, _monsterIcons.Count);
            Monster = new MonsterViewModel(_monsterIcons[monsterIndex], 5 + (Player.MonstersKilled * 2), 1 + Player.MonstersKilled, 1 + Player.MonstersKilled);
        }

        public ItemViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public string BackGroundIcon => _backGroundIcon;

        public string BloodEffectIcon => _bloodEffectIcon;

        public PlayerViewModel Player => _player;

        public MonsterViewModel Monster
        {
            get => _monster;
            set
            {
                _monster = value;
                OnPropertyChanged("Monster");
            }
        }

        private void MonsterResponse()
        {
            Player.CurrentHealth = Monster.Damage - Player.Armor - Player.EquippedArmor.Value > 0 ? Player.CurrentHealth - Monster.Damage + Player.Armor + Player.EquippedArmor.Value : Player.CurrentHealth;

            if (Player.CurrentHealth <= 0)
            {
                Death();
            }
            else
            {
                if (Monster.CurrentHealth <= 0)
                {
                    Player.CurrentExperience += 25;
                    Player.MonstersKilled += 1;

                    if (Player.CurrentExperience >= Player.MaxExperience)
                    {
                        Player.UpgradesRemaining += 1;
                        Player.CurrentExperience -= Player.MaxExperience;
                    }

                    Reward();

                    int monsterIndex = _random.Next(0, _monsterIcons.Count);
                    Monster = new MonsterViewModel(_monsterIcons[monsterIndex], 5 + (Player.MonstersKilled * 2), 1 + Player.MonstersKilled, 1 + Player.MonstersKilled);
                }
            }
        }

        private void Reward()
        {
            int itemTypeIndex = _random.Next(0, _itemTypeValues.Count);
            ItemType itemType = _itemTypeValues[itemTypeIndex];
            ItemViewModel newItem;
            RewardWindow rewardWindow;

            if (itemType == ItemType.Weapon)
            {
                newItem = new ItemViewModel(_itemIcons[_weaponsOffset + _rewardWeapon], _weaponNames[_rewardWeapon], itemType, _weaponValues[_rewardWeapon]);
                ItemList.Insert(0, newItem);

                _rewardWeapon++;

                if (_rewardWeapon == 3)
                {
                    _ = _itemTypeValues.Remove(ItemType.Weapon);
                }
            }
            else if (itemType == ItemType.Armor)
            {
                newItem = new ItemViewModel(_itemIcons[_armorOffset + _rewardArmor], _armorNames[_rewardArmor], itemType, _armorValues[_rewardArmor]);
                ItemList.Insert(0, newItem);

                _rewardArmor++;

                if (_rewardArmor == 2)
                {
                    _ = _itemTypeValues.Remove(ItemType.Armor);
                }
            }
            else
            {
                foreach (ItemViewModel item in ItemList)
                {
                    if (item.ItemType == itemType)
                    {
                        item.Count++;
                        ItemList.Move(ItemList.IndexOf(item), 0);
                        rewardWindow = new RewardWindow(item.ItemIcon, item.ItemName, item.ItemDetail);
                        _ = rewardWindow.ShowDialog();
                        return;
                    }
                }

                newItem = new ItemViewModel(_itemIcons[_consumablesOffset + ((int)itemType)], _consumableNames[(int)itemType], itemType);
                ItemList.Insert(0, newItem);
            }

            rewardWindow = new RewardWindow(newItem.ItemIcon, newItem.ItemName, newItem.ItemDetail);
            _ = rewardWindow.ShowDialog();
        }

        private void Death()
        {
            DeathWindow deathWindow = new DeathWindow();
            _ = deathWindow.ShowDialog();
            System.Windows.Application.Current.Shutdown();
        }

        public RelayCommand AttackCommand => _attackCommand ?? (_attackCommand = new RelayCommand(Combat, AttackCommandCanExecute));

        private void Combat(object obj)
        {
            SelectedItem = null;

            Monster.CurrentHealth = Player.Damage + Player.EquippedWeapon.Value - Monster.Armor > 0 ? Monster.CurrentHealth - Player.Damage - Player.EquippedWeapon.Value + Monster.Armor : Monster.CurrentHealth - 1;

            MonsterResponse();
        }

        private bool AttackCommandCanExecute(object obj)
        {
            return true;
        }

        public RelayCommand UseItemCommand => _useItemCommand ?? (_useItemCommand = new RelayCommand(UseItem, UseItemCommandCanExecute));

        private void UseItem(object obj)
        {
            if (SelectedItem.ItemType == ItemType.Potion)
            {
                Player.CurrentHealth = Player.MaxHealth;
            }
            else
            {
                Monster.CurrentHealth = 0;
            }

            SelectedItem.Count--;

            if (SelectedItem.Count == 0)
            {
                _ = ItemList.Remove(SelectedItem);
            }
            else
            {
                SelectedItem = null;
            }

            MonsterResponse();
        }

        private bool UseItemCommandCanExecute(object obj)
        {
            return
                SelectedItem != null && (
                    (SelectedItem.ItemType == ItemType.Potion && Player.CurrentHealth != Player.MaxHealth) ||
                    (SelectedItem.ItemType == ItemType.Bomb && Monster.CurrentHealth != 0)
                );
        }

        public RelayCommand EquipItemCommand => _equipItemCommand ?? (_equipItemCommand = new RelayCommand(EquipItem, EquipItemCommandCanExecute));

        private void EquipItem(object obj)
        {
            if (SelectedItem.ItemType == ItemType.Weapon)
            {
                ItemList.Add(Player.EquippedWeapon);
                Player.EquippedWeapon = SelectedItem;
            }
            else
            {
                ItemList.Add(Player.EquippedArmor);
                Player.EquippedArmor = SelectedItem;
            }

            _ = ItemList.Remove(SelectedItem);
        }

        private bool EquipItemCommandCanExecute(object obj)
        {
            return
                SelectedItem != null && (
                    SelectedItem.ItemType == ItemType.Weapon ||
                    SelectedItem.ItemType == ItemType.Armor
                );
        }

        public RelayCommand UpgradeHealthCommand => _upgradeHealthCommand ?? (_upgradeHealthCommand = new RelayCommand(UpgradeHealth, UpgradeCanExecute));
        public RelayCommand UpgradeDamageCommand => _upgradeDamageCommand ?? (_upgradeDamageCommand = new RelayCommand(UpgradeDamage, UpgradeCanExecute));
        public RelayCommand UpgradeArmorCommand => _upgradeArmorCommand ?? (_upgradeArmorCommand = new RelayCommand(UpgradeArmor, UpgradeCanExecute));

        private void UpgradeHealth(object obj)
        {
            Player.UpgradesRemaining--;
            Player.HealthLevel++;
            Player.MaxHealth += 10;
            Player.CurrentHealth += 10;
        }

        private void UpgradeDamage(object obj)
        {
            Player.UpgradesRemaining--;
            Player.Damage += 5;
            Player.DamageLevel += 1;
        }

        private void UpgradeArmor(object obj)
        {
            Player.UpgradesRemaining--;
            Player.Armor += 3;
            Player.ArmorLevel += 1;
        }

        private bool UpgradeCanExecute(object obj)
        {
            return Player.UpgradesRemaining > 0;
        }
    }
}
