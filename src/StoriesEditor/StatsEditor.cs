#if TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using UnwillingLife.Data;

namespace UnwillingLife.Tools;
[Tool]
public partial class StatsEditor : VBoxContainer
{
	[Export]
	public string StatNameTitle = string.Empty;
    private object NarrativeProfile;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		GetNode<Label>("Label").Text = StatNameTitle;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif