using Godot;
using System;

public partial class InfoPanel : PanelContainer
{

    private Label _info;
    private AnimationPlayer _infoanim;
    public string Intro = "Ah, welcome, boss. Abandoned facility, free resources, zero health and safety regulations—a true capitalist dream! Fire up the core and dig deep; the world won't conquer itself.";
    public string EcoBeggining = "Well, well, well... look what your unstoppable corporate greed did. The planet is officially dying, and you've got 5 years (5 minutes IRL) to go green and fix this absolute disaster before total annihilation. Tick-tock, boss!";
    public override void _Ready()
    {
        _infoanim = GetNode<AnimationPlayer>("%InfoLabel");
        _info = GetNode<Label>("%InfoText");

        _info.Text = Intro;
        _infoanim.Play("InfoPopup");
        GetTree().CreateTimer(6.5).Timeout += OnTimerTimeout;
        SignalBus.Instance.ShowEcoUpgrades += ShowEcoInfo;
    }
    public override void _ExitTree()
    {
        SignalBus.Instance.ShowEcoUpgrades -= ShowEcoInfo;
    }

    private void OnTimerTimeout() => _infoanim.PlayBackwards("InfoPopup");
    private void ShowEcoInfo()
    {
        _info.Text = EcoBeggining;
        _infoanim.Play("InfoPopup");
        GetTree().CreateTimer(6.5).Timeout += OnTimerTimeout;
    }
}
