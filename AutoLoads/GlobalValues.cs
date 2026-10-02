using Godot;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

public partial class GlobalValues : Node
{
	public static GlobalValues Instance { get; private set; }

	private int _score = 0;
	public int Score => _score;

	private float _speed = 500f;
	public float Speed => _speed;
	private double _multiplier = 1.0;
	public double Multiplier => _multiplier;

	private float _slowdown = 0.05f;
	public float Slowdown => _slowdown;

	private float _delpoint;
	public float DelPoint => _delpoint;

	public void SetDelPoint(Vector2 position) => _delpoint = position.Y;
	public void UpgradeSpeed() => _speed += 50f;
	public void UpgradeSlowdown() => _slowdown += 0.05f;
	public void AddScore(int amount) => _score += amount;
	    public override void _Ready()
	{
		Instance = this;
	}
}
