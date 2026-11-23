using Godot;
using System;

public partial class Ui : Control
{
    private Button _pullUp;
    private bool _isDown = false;
    private AnimationPlayer _anim;
    private UpgradeDisplay _upgrade1;
    private UpgradeDisplay _upgrade2;
    private UpgradeDisplay _upgrade3;

    private UpgradeDisplay auto1;
    private UpgradeDisplay auto2;
    private UpgradeDisplay auto3;

    private bool _blocked = false;


    public override void _Ready()
    {
        _upgrade1 = GetNode<UpgradeDisplay>("%Upgrade10");
        _upgrade2 = GetNode<UpgradeDisplay>("%Upgrade11");
        _upgrade3 = GetNode<UpgradeDisplay>("%Upgrade12");
        auto1 = GetNode<UpgradeDisplay>("%Upgrade5");
        auto2 = GetNode<UpgradeDisplay>("%Upgrade6");
        auto3 = GetNode<UpgradeDisplay>("%Upgrade7");
        _pullUp = GetNode<Button>("%PullUpUpgrades");
        _anim = GetNode<AnimationPlayer>("%Pullout");
        _pullUp.Pressed += OnPullUpPressed;
        SignalBus.Instance.ShowEcoUpgrades += ForceShowUpgrades;
        SignalBus.Instance.BlockFurtherUpgrades += SwitchBlockButtonsBool;
        SignalBus.Instance.HideUI += HideTUi;
    }

    public override void _ExitTree()
    {
        SignalBus.Instance.ShowEcoUpgrades -= ForceShowUpgrades;
        SignalBus.Instance.BlockFurtherUpgrades -= SwitchBlockButtonsBool;
        SignalBus.Instance.HideUI -= HideTUi;
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
    private void HideTUi()
    {
        if (!_isDown) return;
        _anim.PlayBackwards("UI");
        _isDown = false;
    }
    private void ForceShowUpgrades()
    {


        _upgrade1.Show();
        _upgrade2.Show();
        _upgrade3.Show();

        _upgrade1.UpdateUI();
        _upgrade2.UpdateUI();
        _upgrade3.UpdateUI();
    }
    private void SwitchBlockButtonsBool() => _blocked = true;
    private void BlockButtons()
    {
        auto1.BlockButton();
        auto2.BlockButton();
        auto3.BlockButton();
    }
    float time;
    public override void _PhysicsProcess(double delta)
    {

        if (_blocked) BlockButtons();
        time += (float)delta;
        if (time >= 30)
        {
            _upgrade1.UpdateUI();
            _upgrade2.UpdateUI();
            _upgrade3.UpdateUI();
            time = 0;
        }
    }
}
