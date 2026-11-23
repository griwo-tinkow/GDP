using Godot;
using System;
namespace UpgradeClasses.Upgrades
{
    public partial class OpenAMexaplex : Node
    {
        private static int _level;
        private int _increase = 7520;
        private int _price = 12700;

        public int Price => _price;
        public static int Level { get; private set; } = _level;
        public string UName = "Automated Mega-Refinery";
        public string UDesc = "Fully roboticized refinery processing raw ore around the clock + 7520$/s";
        public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/Refinery.png");

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

    }
}
