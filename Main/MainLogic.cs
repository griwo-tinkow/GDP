using Godot;
using System;
using System.Drawing;
using System.Collections.Generic;

public partial class MainLogic : Control
{
    private AnimatedSprite2D _earth;
    private TextureProgressBar _bar;
    private TextureRect _fire;
    private Vector2 currentSize;
    private Button _button;
    private Label _scoreLabel;
    private Label _moneyPerSec;
    private Marker2D _marker;
    private Control _DP;
    private AnimationPlayer _animPlayer;
    private TextureRect _bg;
    [Export] private PackedScene _clParticles;
    private Tween _clickTween;

    private float time;
    private bool _startCountdown;
    private int _timesPinged;
    float t2 = 0;
    private readonly PackedScene _itemScene = GD.Load<PackedScene>("res://Main/Products/product.tscn");

    public override void _Ready()
    {
        currentSize = Size;
        _bg = GetNode<TextureRect>("%Background");
        _earth = GetNode<AnimatedSprite2D>("%Earth");
        _fire = GetNode<TextureRect>("%Fire");
        _button = GetNode<Button>("%Button");
        _scoreLabel = GetNode<Label>("%Score");
        _moneyPerSec = GetNode<Label>("%mps");
        _marker = GetNode<Marker2D>("%SP");
        _DP = GetNode<Control>("%DP");
        _bar = GetNode<TextureProgressBar>("%PollutionBar");
        _animPlayer = GetNode<AnimationPlayer>("%anim");


        _bar.Visible = false;

        SignalBus.Instance.ShowEcoUpgrades += StartEcoPhase;
        SignalBus.Instance.WinningUpgradeBought += CloseMainScene;
        _button.Pressed += OnButtonPressed;
        SignalBus.Instance.UpdateScoreLabel += UpdateUI;
        SignalBus.Instance.UpdateTheUI += UpdateUI;

        _earth.Play("default");
        _earth.Position = new Vector2(currentSize.X / 2, currentSize.Y / 2);
        UpdateUI();
        _bar.MaxValue = 310;
        _bar.MinValue = 0;
        _animPlayer.Play("fading");
    }
    public override void _ExitTree()
    {
        SignalBus.Instance.UpdateScoreLabel -= UpdateUI;
        SignalBus.Instance.UpdateTheUI -= UpdateUI;
        SignalBus.Instance.ShowEcoUpgrades -= StartEcoPhase;
        SignalBus.Instance.WinningUpgradeBought -= CloseMainScene;

    }
    private void ClickEffects()
    {
        var instance = _clParticles.Instantiate() as Node2D;
        GetTree().Root.AddChild(instance);
        instance.GlobalPosition = _earth.GlobalPosition;
        instance.ZIndex = 150;
        if (instance is ClickParticles particleScript) particleScript.SpawnParticles();
        if (_clickTween != null && _clickTween.IsRunning())
        {
            _clickTween.Kill();
        }
        _earth.Scale = new Vector2(3.5f, 3.5f);
        _clickTween = CreateTween();
        _clickTween.SetEase(Tween.EaseType.Out);
        _clickTween.SetTrans(Tween.TransitionType.Back);

        _clickTween.TweenProperty(_earth, "scale", new Vector2(3.8f, 3.8f), 0.1f);
    }
    private void OnButtonPressed()
    {
        ClickEffects();
        UpdateUI();
        SpawnInstance(new Vector2((float)GD.RandRange(0, currentSize.X), _marker.Position.Y), (float)GD.RandRange(0, 360));

        if (GlobalValues.Instance.DoubleDrops)
        {
            SpawnInstance(new Vector2((float)GD.RandRange(0, currentSize.X), _marker.Position.Y), (float)GD.RandRange(0, 360));
            SpawnInstance(new Vector2((float)GD.RandRange(0, currentSize.X), _marker.Position.Y), (float)GD.RandRange(0, 360));
        }
        SignalBus.Instance.EmitSignal(SignalBus.SignalName.HideUI);
    }
    private void StartEcoPhase()
    {
        _bar.Visible = true;
        _earth.SelfModulate = new Godot.Color(0.647f, 0.165f, 0.165f);
        _bg.SelfModulate = new Godot.Color(0.2f, 0.2f, 0.2f);
        AudioManager.Instance.SpeedUpCurrent();
    }
    public async void CloseMainScene(bool won)
    {
        _animPlayer.PlayBackwards("fading");
        if (won) AudioManager.Instance.PlayWin();
        else AudioManager.Instance.PlayLose();
        await ToSignal(_animPlayer, AnimationPlayer.SignalName.AnimationFinished);
        if (!IsInsideTree()) return;
        if (won) GetTree().ChangeSceneToFile("res://Main/Endings/Good.tscn");
        else GetTree().ChangeSceneToFile("res://Main/Endings/Bad.tscn");

    }
    private void UpdateUI()
    {
        _scoreLabel.SelfModulate = new Godot.Color(0, 1, 0, 1);
        _moneyPerSec.SelfModulate = new Godot.Color(0, 0.39f, 0, 1);
        _scoreLabel.Text = $"{GlobalValues.Instance.NormaliseMoney(GlobalValues.Instance.Score)} $";
        _moneyPerSec.Text = $"{GlobalValues.Instance.NormaliseMoney(GlobalValues.Instance.MoneyPerSecond)} $/s";
        GlobalValues.Instance.SetDelPoint(_DP.Position);
    }
    private void SpawnInstance(Vector2 spawnPosition, float rotation = 0.0f)
    {
        var newInstance = _itemScene.Instantiate<Node2D>();
        GetTree().CurrentScene.AddChild(newInstance);
        newInstance.Position = spawnPosition + new Vector2(0, -50);
        newInstance.RotationDegrees = rotation;
    }
    public override void _PhysicsProcess(double delta)
    {
        UpdateUI();
        if (GlobalValues.Instance.EcoMode && _timesPinged < 1)
        {
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.ShowEcoUpgrades);
            _startCountdown = true;
            _timesPinged++;
        }
        if (_startCountdown)
        {
            t2 += (float)delta;
            _bar.Value = t2;
            if (_bar.Value >= _bar.MaxValue) CloseMainScene(false);
        }
        if (GlobalValues.Instance.MoneyPerSecond == 0) return;
        time += (float)delta;
        if (time >= 1)
        {
            GlobalValues.Instance.AddScore(GlobalValues.Instance.MoneyPerSecond, true);
            time = 0;
        }
    }
}

