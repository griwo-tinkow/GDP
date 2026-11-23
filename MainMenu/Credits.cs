using Godot;
using System;

public partial class Credits : Control
{
    private AnimationPlayer _fadeIn;
    private Button _return;
    public override void _Ready()
    {
        _fadeIn = GetNode<AnimationPlayer>("%anim");
        _return = GetNode<Button>("%return");

        _fadeIn.Play("fading");
        _return.Pressed += ReturnToMainMenu;
    }
    private async void ReturnToMainMenu()
    {

        _fadeIn.PlayBackwards("fading");
        AudioManager.Instance.PlayBuy();
        await ToSignal(_fadeIn, AnimationPlayer.SignalName.AnimationFinished);
        GetTree().ChangeSceneToFile("res://MainMenu/MainMenu.tscn");
    }
}
