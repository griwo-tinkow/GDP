using Godot;
using System;
namespace UpgradeClasses.Upgrades
{
    public partial class ImproveTools : Node
    {
        private static int _level;
        private static int _maxLevel = 1;
        private int _price = 750000;
        private int startingPrice = 750000;

        public int Price => _price;
        public static int Level { get; private set; } = _level;
        public static int MaxLevel { get; private set; } = _maxLevel;
        public string UName = "Better Tools";
        public string UDesc = "The tools improve your output allowing you to gain 3 items per click";
        public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/Tools.png");


        public void BuyUpgrade()
        {
            if (GlobalValues.Instance.Score < _price || Level >= _maxLevel) return;
            else
            {
                Level++;
                GlobalValues.Instance.AddScore(_price, false);
                GlobalValues.Instance.DoubleDrops = true;


            }
        }
        public void Reset()
        {
            _level = 0;
            _price = startingPrice;
        }

    }
}
