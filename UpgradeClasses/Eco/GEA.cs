using Godot;
using System;

public partial class GEA : Node
{
    private int _price = 2500000;
    private static int _level;
    private static int _maxLevel = 1;

    public int Price => _price;
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public string UName = "Green Earth Accord";
    public string UDesc = "Liquidate all assets to save the planet.";
    public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/SaveEarth.png");

    public void BuyUpgrade()
    {
        if (Level >= _maxLevel && GlobalValues.Instance.Score < _price) return;
        else
        {
            GlobalValues.Instance.AddScore(GlobalValues.Instance.Score, false);
            _level++;
        }
    }
}
