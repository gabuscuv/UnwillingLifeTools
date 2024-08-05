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
    private SpinBox sb_Stress;
    private SpinBox sb_Concern;
    private SpinBox sb_MentalStability;

    private string GetNodeName(string name) 
	{
		return $"HBC_{name}/SB_{name}";
	}
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		GetNode<Label>("Label").Text = StatNameTitle;

		sb_Stress = GetNode<SpinBox>(GetNodeName("Stress"));
		sb_Concern = GetNode<SpinBox>(GetNodeName("Concern"));
		sb_MentalStability = GetNode<SpinBox>(GetNodeName("MentalStability"));

	}

	public void SetStats(NarrativePoints e) 
	{
		sb_Stress.Value = e.Stress;
		sb_Concern.Value = e.Concern;
		sb_MentalStability.Value = e.MentalStability;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif