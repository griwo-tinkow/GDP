using Godot;
using System;

public partial class ClickParticles : CpuParticles2D
{
    public override void _Ready()
    {
        OneShot = true;
        Finished += QueueFree;
    }
    public void SpawnParticles() => Emitting = true;
}
