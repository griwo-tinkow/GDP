using Godot;
using System;

public partial class Ending : Control
{
    private Button _end;
    private AnimationPlayer _anim;
    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("%anim");
        _end = GetNode<Button>("%Button");
        _end.Pressed += OnQuitPressed;

        _anim.Play("fading");

    }




    private async void OnQuitPressed()
    {
        _anim.PlayBackwards("fading");
        AudioManager.Instance.PlayBuy();
        await ToSignal(_anim, AnimationPlayer.SignalName.AnimationFinished);
        GetTree().ChangeSceneToFile("res://MainMenu/MainMenu.tscn");
    }
}

