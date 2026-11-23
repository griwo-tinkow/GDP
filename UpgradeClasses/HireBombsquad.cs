using Godot;
using System;
namespace UpgradeClasses.Upgrades
{
    public partial class HireBombSquad : Node
    {
        private static int _level;
        private int _increase = 75;
        private int _price = 350;

        public int Price => _price;
        public static int Level { get; private set; } = _level;
        public string UName = "BombSquads";
        public string UDesc = "They Generate you 75$ per second";
        public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/bomb.png");

        public void BuyUpgrade()
        {
            if (GlobalValues.Instance.Score < _price) return;
            else
            {
                Level++;
                GlobalValues.Instance.AddScore(_price, false);
                GlobalValues.Instance.ChangeMoneyPerSecond(_increase);
                _price = (int)Math.Round(_price * 1.5);
            }

        }

    }
}
