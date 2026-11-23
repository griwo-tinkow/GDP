using Godot;
using System;
namespace UpgradeClasses.Upgrades
{
    public partial class BuyFurnaces : Node
    {
        private static int _level;
        private static int _maxLevel = 1;
        private int _price = 697500;

        public int Price => _price;
        public static int Level { get; private set; } = _level;
        public static int MaxLevel { get; private set; } = _maxLevel;
        public string UName = "Furnaces";
        public string UDesc = "Allows for Smelted Ores 3x the normal Value";
        public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/Furnace.png");


        public void BuyUpgrade()
        {
            if (GlobalValues.Instance.Score < _price || Level >= _maxLevel) return;
            else
            {
                Level++;
                GlobalValues.Instance.AddScore(_price, false);
                GlobalValues.Instance.HasFurnace = true;
            }
        }

    }
}
