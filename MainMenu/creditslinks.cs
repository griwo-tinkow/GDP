using Godot;
using System;

public partial class creditslinks : RichTextLabel
{
    public override void _Ready()
    {
        MetaClicked += OnMetaClicked;
    }

    private void OnMetaClicked(Variant meta)
    {
        OS.ShellOpen(meta.AsString());
    }
}
