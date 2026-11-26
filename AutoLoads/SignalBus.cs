using Godot;
using System;

public partial class SignalBus : Node
{
    public static SignalBus Instance { get; private set; }
    [Signal] public delegate void UpdateScoreLabelEventHandler();
    [Signal] public delegate void DeleteInstanceEventHandler();
    [Signal] public delegate void UpdateTheUIEventHandler();
    [Signal] public delegate void ShowEcoUpgradesEventHandler();
    [Signal] public delegate void BlockFurtherUpgradesEventHandler();
    [Signal] public delegate void HideUIEventHandler();
    [Signal] public delegate void WinningUpgradeBoughtEventHandler(bool win);
    [Signal] public delegate void EndEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }

}
