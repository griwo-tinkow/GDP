using Godot;
using System;
namespace UpgradeClasses.Upgrades
{
    public partial class ConveyorSpeed : Node
    {
        private static int _level;
        private static int _maxLevel = 14;
        private int _price = 320;

        public int Price => _price;
        public static int Level { get; private set; } = _level;
        public static int MaxLevel { get; private set; } = _maxLevel;
        public string UName = "Express Conveyors";
        public String UDesc = "Items fall faster by 5% per upgrade";
        public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/Conveyor.png");

        public void BuyUpgrade()
        {
            if (GlobalValues.Instance.Score < _price || Level >= _maxLevel) return;
            else
            {
                GlobalValues.Instance.UpgradeSpeed();
                Level++;
                GlobalValues.Instance.AddScore(_price, false);
                _price = (int)Math.Round(_price * 1.45);
            }
        }

    }
}
