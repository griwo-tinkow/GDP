using Godot;
using System;

public partial class MainMenu : Control
{
    private Button _quit;
    private Button _start;
    private Button _settings;
    private Button _cred;
    private AnimationPlayer _anim;


    public override void _Ready()
    {
        _quit = GetNode<Button>("%Quit");
        _start = GetNode<Button>("%Play");
        _settings = GetNode<Button>("%Settings");
        _cred = GetNode<Button>("%Credits");
        _anim = GetNode<AnimationPlayer>("%anim");
        _quit.Pressed += Quit;
        _start.Pressed += Start;
        _settings.Pressed += Settings;
        _cred.Pressed += Credits;

        _anim.Play("fading");
    }

    private async void Quit()
    {
        AudioManager.Instance.PlayBuy();
        _anim.PlayBackwards("fading");
        await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
        GetTree().Quit();
    }
    private async void Settings()
    {
        AudioManager.Instance.PlayBuy();
        _anim.PlayBackwards("fading");
        await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
        GetTree().ChangeSceneToFile("res://MainMenu/Settings.tscn");
    }
    private async void Credits()
    {
        AudioManager.Instance.PlayBuy();
        _anim.PlayBackwards("fading");
        await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
        GetTree().ChangeSceneToFile("res://MainMenu/Credits.tscn");
    }
    private async void Start()
    {
        {
            AudioManager.Instance.PlayBuy();
            _anim.PlayBackwards("fading");
            await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
            GetTree().ChangeSceneToFile("res://main.tscn");

        }
    }
}

