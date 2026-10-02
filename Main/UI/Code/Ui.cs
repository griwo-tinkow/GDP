using Godot;
using System;

public partial class Ui : Control
{
    private Button _pullUp;
    private bool _isDown = false;
    private AnimationPlayer _anim;

    public override void _Ready()
    {
        _pullUp = GetNode<Button>("%PullUpUpgrades");
        _anim = GetNode<AnimationPlayer>("%Pullout");
        _pullUp.Pressed += OnPullUpPressed;
    }

    private void OnPullUpPressed()
    {
        if (!_isDown)
        {
            _anim.Play("UI");
            _isDown = true;
        }
        else
        {
            _anim.PlayBackwards("UI");
            _isDown = false;
        }
    }
}
