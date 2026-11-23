using Godot;
using System;
namespace UpgradeClasses.Upgrades
{
    public partial class IncreaseCosts : Node
    {
        private static int _level;
        private double _appliedMultiplier = 0.2;
        private int _price = 150;

        public int Price => _price;
        public static int Level { get; private set; } = _level;
        public string UName = "Inflated Prices of Resources";
        public string UDesc = "You raise the prices of your minerals, +20% on upgrade";
        public Texture2D Icon = GD.Load<Texture2D>("res://Money Icons/tile017.png");

        private double AddMultiplier() => GlobalValues.Instance.Multiplier += _appliedMultiplier;

        public void BuyUpgrade()
        {
            if (GlobalValues.Instance.Score < _price) return;
            else
            {
                AddMultiplier();
                Level++;
                GlobalValues.Instance.AddScore(_price, false);
                _price = (int)Math.Round(_price * 1.25);

            }
        }

    }
}
