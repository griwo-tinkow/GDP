using Godot;
using System;

public partial class SlowDown : Node
{
    private int startingPrice = 500000;
    private int _price = 500000;
    private static int _level;
    private static int _maxLevel = 1;

    public int Price => _price;
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public string UName = "Supply Chain Throttling";
    public string UDesc = "Slow production to lower energy and fuel consumption";
    public Texture2D Icon = GD.Load<Texture2D>("res://Main/Textures/Conveyor.png");

    public void BuyUpgrade()
    {
        if (Level >= _maxLevel && GlobalValues.Instance.Score < _price) return;
        else
        {
            _level++;
            GlobalValues.Instance.AddScore(_price, false);
            GlobalValues.Instance.ResetSpeed();
        }
    }

    public void Reset()
    {
        {
            _level = 0;
            _price = startingPrice;
        }
    }
}
