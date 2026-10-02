using Godot;
using System;

public partial class Items : Node2D
{
    private Sprite2D _timber;
    private Sprite2D _rock1;
    private Sprite2D _rock2;
    private Sprite2D _ironRaw;
    private Sprite2D _goldRaw;
    private Sprite2D _diamondRaw;
    private Sprite2D _emeraldRaw;
    private Sprite2D _iron;
    private Sprite2D _gold;
    private Sprite2D _diamond;
    private Sprite2D _emerald;

	private int _spin = 0;

	private bool _right = false;


	private int _value = 0;

	private void SwitchItemsVisibility()
	{
		_timber.Visible = false;
		_rock1.Visible = false;
		_rock2.Visible = false;
		_ironRaw.Visible = false;
		_goldRaw.Visible = false;
		_diamondRaw.Visible = false;
		_emeraldRaw.Visible = false;
		_iron.Visible = false;
		_gold.Visible = false;
		_diamond.Visible = false;
		_emerald.Visible = false;
	}
	private void GenerateAnItem()
	{
		int generated = GD.RandRange(0,100);
		if (generated <= 65)
		{
			_timber.Visible = true;
			_value = 10;
		}
		else if (generated <= 85 && generated > 65)
		{
			if(GD.RandRange(0,100)%2 == 0)
			{
				_rock1.Visible = true;
			}
			else
			{
				_rock2.Visible = true;
			}
			_value = GD.RandRange(3,5);
		}
		else if (generated <= 94 && generated > 85)
		{
			_ironRaw.Visible = true;
			_value = 10;
		}
		else if (generated <= 97 && generated > 94)
		{
			_goldRaw.Visible = true;
			_value = 35;
		}
		else if (generated <= 99 && generated > 97)
		{
			_diamondRaw.Visible = true;
			_value = 120;
		}
		else
		{
			_emeraldRaw.Visible = true;
			_value = 400;
		}
	}
    public override void _Ready()
    {
        _timber = GetNodeOrNull<Sprite2D>("%Timber");
        _rock1 = GetNodeOrNull<Sprite2D>("%Rock1");
        _rock2 = GetNodeOrNull<Sprite2D>("%Rock2");
        _ironRaw = GetNodeOrNull<Sprite2D>("%IronRaw");
        _goldRaw = GetNodeOrNull<Sprite2D>("%GoldRaw");
        _diamondRaw = GetNodeOrNull<Sprite2D>("%DiamondRaw");
        _emeraldRaw = GetNodeOrNull<Sprite2D>("%EmeraldRaw");
        _iron = GetNodeOrNull<Sprite2D>("%Iron");
        _gold = GetNodeOrNull<Sprite2D>("%Gold");
        _diamond = GetNodeOrNull<Sprite2D>("%Diamond");
        _emerald = GetNodeOrNull<Sprite2D>("%Emerald");

		SwitchItemsVisibility();
		GenerateAnItem();
		_spin = GD.RandRange(0, 100);

		if (_spin%2 == 0)
		{
			_right = true;
		}
		else
		{
			_right = false;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		switch(_right)
		{
			case true:
				Rotation += 0.1f;
			break;

			case false:
				Rotation -= 0.1f;
			break;
		}

		if (Position.Y >= GlobalValues.Instance.DelPoint)
		{
			GlobalValues.Instance.AddScore((int)Math.Round(_value * GlobalValues.Instance.Multiplier));
			SignalBus.Instance.EmitSignal("UpdateScoreLabel");
            QueueFree();
		}
		
		float _screenHeight = GetViewportRect().Size.Y;
		float curSpeed = GlobalValues.Instance.Speed;
		if (Position.Y >= _screenHeight + 100f) curSpeed *= GlobalValues.Instance.Slowdown;
		else if (Position.Y >= _screenHeight - 100.5f)
        {
			curSpeed *= GlobalValues.Instance.Slowdown;
        }
		Position += new Vector2(0, curSpeed * (float)delta);
	}
}
