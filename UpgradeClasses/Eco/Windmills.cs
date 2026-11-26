using Godot;
using System;

public partial class WindMills : Node
{
    private int startingPrice = 500000;
    private static int _level;
    private int _increase = 650;
    private int _price = 500000;
    private static int _maxLevel = 3;

    public int MaxLevel => _maxLevel;
    public int Price => _price;
    public int Level => _level;
    public string UName = "WindMills";
    public string UDesc = "Transition to green energy and pause automatic earnings";
    public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/Windmill.png");

    public void BuyUpgrade()
    {
        if (_price > GlobalValues.Instance.Score && Level >= _maxLevel) return;
        else
        {
            _level++;
            GlobalValues.Instance.AddScore(_price, false);
            GlobalValues.Instance.CutMoneyPerSecond(_level);
        }
    }

    public void Reset()
    {
        _level = 0;
        _price = startingPrice;
    }
}
