using Godot;
using System;
namespace UpgradeClasses.Upgrades
{
    public partial class MineDeeper : Node
    {
        private static int _level;
        private static int _maxLevel = 20;
        private int _price = 350;


        // Rows (5):    [0] Wood, [1] Stone, [2] Iron, [3] Gold, [4] Diamond
        // Columns (20): Tiers 1 through 20
        // Every column sums to exactly 99.5%
        public static readonly float[,] TierDropChances = new float[5, 20]
        {
            // Tier: 1      2      3      4      5      6      7      8      9     10     11     12     13     14     15     16     17     18     19     20
            /*Wood*/ { 75.0f, 70.0f, 65.0f, 60.0f, 55.0f, 49.0f, 43.0f, 37.0f, 31.0f, 25.0f, 20.0f, 15.0f, 11.0f,  7.0f,  4.0f,  2.0f,  1.0f,  0.5f,  0.0f,  0.0f },
            /*Stone*/{ 15.0f, 17.5f, 20.0f, 23.0f, 26.0f, 29.0f, 32.0f, 34.5f, 35.0f, 34.0f, 30.0f, 25.0f, 19.5f, 14.5f,  9.5f,  5.5f,  2.5f,  1.0f,  0.5f,  0.0f },
            /*Iron*/ {  5.0f,  6.0f,  7.5f,  9.0f, 11.0f, 13.5f, 16.0f, 19.0f, 22.5f, 26.5f, 31.0f, 36.0f, 40.5f, 44.5f, 47.5f, 49.5f, 51.0f, 52.0f, 51.0f, 50.0f },
            /*Gold*/ {  3.0f,  3.5f,  4.0f,  4.5f,  5.0f,  6.0f,  7.0f,  8.0f,  9.0f, 11.0f, 13.5f, 16.5f, 19.5f, 22.0f, 24.0f, 25.0f, 25.5f, 25.5f, 25.0f, 25.0f },
            /*Diam*/ {  1.5f,  3.0f,  3.5f,  3.5f,  2.5f,  2.0f,  1.5f,  1.0f,  2.0f,  3.0f,  5.0f,  7.0f,  9.0f, 11.5f, 14.5f, 17.5f, 19.5f, 20.5f, 23.0f, 24.5f } };

        private float Wood => TierDropChances[0, Mathf.Clamp(_level - 1, 0, 19)];
        private float Stone => TierDropChances[1, Mathf.Clamp(_level - 1, 0, 19)];
        private float Iron => TierDropChances[2, Mathf.Clamp(_level - 1, 0, 19)];
        private float Gold => TierDropChances[3, Mathf.Clamp(_level - 1, 0, 19)];
        private float Diam => TierDropChances[4, Mathf.Clamp(_level - 1, 0, 19)];
        public int Price => _price;
        public static int Level { get; private set; } = _level;
        public static int MaxLevel { get; private set; } = _maxLevel;
        public string UName = "Deeper Mines";
        public string UDesc = "Makes rare ores spawn more frequently";
        public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/Rock.png");

        public void BuyUpgrade()
        {
            GlobalValues.Instance.WoodChance = Wood;
            GlobalValues.Instance.StoneChance = Stone;
            GlobalValues.Instance.IronChance = Iron;
            GlobalValues.Instance.GoldChance = Gold;
            GlobalValues.Instance.DiamondChance = Diam;

            if (GlobalValues.Instance.Score < _price || _level >= _maxLevel) return;
            else
            {
                _level++;
                Level++;
                GlobalValues.Instance.AddScore(_price, false);
                _price = (int)Math.Round(_price * 1.5);
            }
        }

    }
}
