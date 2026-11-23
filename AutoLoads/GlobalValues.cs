using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Godot;

public partial class GlobalValues : Node
{
    public static GlobalValues Instance { get; private set; }

    public bool EcoMode { get; set; } = false;

    private int _score = 0;
    public int Score => _score;

    public bool DoubleDrops { get; set; } = false;
    public bool HasFurnace { get; set; } = false;

    private int _moneyPerSecond = 0;
    public int MoneyPerSecond => _moneyPerSecond;

    private float _speed = 500f;
    public float Speed => _speed;
    public double Multiplier { get; set; } = 1.0;

    private float _slowdown = 0.1f;
    public float Slowdown => _slowdown;

    private float _delpoint;
    public float DelPoint => _delpoint;


    public float WoodChance { get; set; }
    public float StoneChance { get; set; }
    public float IronChance { get; set; }
    public float GoldChance { get; set; }
    public float DiamondChance { get; set; }


    public int WoodCost { get; set; } = 1;       // Wood / Timber = $1
    public int StoneCost { get; set; } = 3;      // Stone / Rocks = $3
    public int IronCost { get; set; } = 10;      // Iron Ore = $10
    public int GoldCost { get; set; } = 30;      // Gold Ore = $30
    public int DiamondCost { get; set; } = 90;   // Diamond = $90
    public int EmeraldCost { get; set; } = 270;  // Emerald = $270



    public void ChangeMoneyPerSecond(int amount) => _moneyPerSecond += amount;
    public void CutMoneyPerSecond(int time)
    {
        switch (time)
        {
            case 1:
                _moneyPerSecond /= 3;
                break;
            case 2:
                _moneyPerSecond /= 2;
                break;
            case 3:
                _moneyPerSecond = 0;
                break;
            default:
                break;
        }
    }
    public void SetDelPoint(Vector2 position) => _delpoint = position.Y;
    public void UpgradeSpeed() => _speed += 25f;
    public void UpgradeSlowdown() => _slowdown += 0.15f;
    public void ResetSpeed()
    {
        _speed = 400f;
        _slowdown = 0.1f;
    }
    public void AddScore(int amount, bool adding)
    {
        if (adding) _score += amount;
        else _score -= amount;

        SignalBus.Instance.EmitSignal(SignalBus.SignalName.UpdateTheUI);
    }
    public string NormaliseMoney(long amount)
    {
        if (amount >= 1000 && amount <= 999999)
            return Math.Round(((float)amount / 1000), 2).ToString() + "k";
        else if (amount >= 1000000 && amount <= 999999999)
            return Math.Round(((float)amount / 1000000), 2).ToString() + "m";
        else if (amount >= 1000000000 && amount <= 999999999999)
            return Math.Round(((float)amount / 1000000000), 2).ToString() + "b";
        else if (amount >= 1000000000000 && amount <= 999999999999999)
            return Math.Round(((float)amount / 1000000000000), 2).ToString() + "t";
        else if (amount >= 1000000000000000 && amount <= 999999999999999999)
            return Math.Round(((float)amount / 1000000000000000), 2).ToString() + "q";
        else
            return amount.ToString();
    }



    public override void _Ready()
    {
        Instance = this;
    }

}
