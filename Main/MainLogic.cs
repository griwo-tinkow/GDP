using Godot;
using System;
using System.Drawing;
using System.Collections.Generic;

public partial class MainLogic : Control    
{
    private AnimatedSprite2D _earth;
    private TextureRect _fire;
    private Vector2 currentSize;
    private Button _button;
    private Label _scoreLabel;
    private Marker2D _marker;
    private Control _DP;

    private readonly PackedScene _itemScene = GD.Load<PackedScene>("res://Main/Products/product.tscn");
   
    public override void _Ready()
    {

        
        currentSize = Size; 
        _earth = GetNode<AnimatedSprite2D>("%Earth");
        _fire = GetNode<TextureRect>("%Fire");
        _button = GetNode<Button>("%Button");
        _scoreLabel = GetNode<Label>("%Score");
        _marker = GetNode<Marker2D>("%SP");
        _DP = GetNode<Control>("%DP");


        _button.Pressed += OnButtonPressed;
        SignalBus.Instance.UpdateScoreLabel += UpdateUI;

        _earth.Play("default"); 
        _earth.Position = new Vector2(currentSize.X / 2, currentSize.Y / 2);
        UpdateUI();
    }
    public override void _ExitTree()
    {
        SignalBus.Instance.UpdateScoreLabel -= UpdateUI;
    }

    private void OnButtonPressed()
    {
        UpdateUI();
        SpawnInstance(new Vector2((float)GD.RandRange(0, currentSize.X), _marker.Position.Y), (float)GD.RandRange(0, 360));

    }
    private void UpdateUI()
    {
        _scoreLabel.Text = $"Score: {GlobalValues.Instance.Score}";
        GlobalValues.Instance.SetDelPoint(_DP.Position);
    }
    private void SpawnInstance(Vector2 spawnPosition, float rotation = 0.0f)
    {
        var newInstance = _itemScene.Instantiate<Node2D>();
        GetTree().CurrentScene.AddChild(newInstance);
        newInstance.Position = spawnPosition + new Vector2(0, -50);
        newInstance.RotationDegrees = rotation;
    }
}
    
