#if TOOLS
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;
using UnwillingLife.Data;

namespace UnwillingLife.Tools;
[Tool]
public partial class StoryStats : HBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Debug.WriteLine("Test");
		StatsEditor statsEditor = null;
		PackedScene packedScene = ResourceLoader.Load("res://addons/UnwillingLifeTools/Components/StoriesEditor/StatsEditor.tscn") as PackedScene;
		foreach (var choice in Enum.GetValues<ENarrativeChoice>().Skip(1)) 
		{
			statsEditor = packedScene.Instantiate<StatsEditor>();
			statsEditor.Name = "SE_" + choice.ToString();
			statsEditor.StatNameTitle = "" + choice.ToString();
			AddChild(statsEditor);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif