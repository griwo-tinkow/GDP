using Godot;
using System;
namespace UpgradeClasses.Upgrades
{
    public partial class OpenAFactory : Node
    {
        private static int _level;
        private int _increase = 650;
        private int _price = 2350;
        private int startingPrice = 2350;

        public int Price => _price;
        public static int Level { get; private set; } = _level;
        public string UName = "Factory";
        public string UDesc = "A whole factory pumping 650$ per second for you";
        public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/factory.png");

        public void BuyUpgrade()
        {
            if (GlobalValues.Instance.Score < _price) return;
            else
            {
                Level++;
                GlobalValues.Instance.AddScore(_price, false);
                _price = (int)Math.Round(_price * 1.5);
                GlobalValues.Instance.ChangeMoneyPerSecond(_increase);
            }

        }
        public void Reset()
        {
            _level = 0;
            _price = startingPrice;
        }
    }
}
