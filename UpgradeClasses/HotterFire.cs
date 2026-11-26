using Godot;
using System;
namespace UpgradeClasses.Upgrades
{
    public partial class HotterFire : Node
    {
        private static int _level;
        private static int _maxLevel = 6;
        private int _price = 750;
        private int startingPrice = 750;

        public int Price => _price;
        public static int Level { get; private set; } = _level;
        public static int MaxLevel { get; private set; } = _maxLevel;
        public string UName = "Hotter Fires";
        public string UDesc = "Fires Burn Hotter Destroying Items faster, +12% per Upgrade";
        public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/Fire.png");

        public void BuyUpgrade()
        {
            if (GlobalValues.Instance.Score < _price || Level >= _maxLevel) return;
            else
            {
                GlobalValues.Instance.UpgradeSpeed();
                Level++;
                GlobalValues.Instance.AddScore(_price, false);
                _price = (int)Math.Round(_price * 1.55);
            }
        }
        public void Reset()
        {
            _level = 0;
            _price = startingPrice;
        }

    }
}
