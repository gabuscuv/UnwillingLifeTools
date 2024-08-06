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
	private SpinBox SB_Stress;
	private SpinBox SB_Concern;
	private SpinBox SB_MentalStability;
    public ENarrativeChoice Choice;

    private string GetNodeName(string name)
	{
		return $"HBC_{name}/SB_{name}";
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Label>("Label").Text = StatNameTitle;

		SB_Stress = GetNode<SpinBox>(GetNodeName("Stress"));
		SB_Concern = GetNode<SpinBox>(GetNodeName("Concern"));
		SB_MentalStability = GetNode<SpinBox>(GetNodeName("MentalStability"));

	}

	public void SetStats(NarrativePoints e)
	{
		Choice = e.Choice;
		SB_Stress.Value = e.Stress;
		SB_Concern.Value = e.Concern;
		SB_MentalStability.Value = e.MentalStability;
	}

	public NarrativePoints GetStats()
	{
		return new()
		{
			Choice = Choice,
			Stress = (int)SB_Stress.Value,
			Concern = (int)SB_Concern.Value,
			MentalStability = (int)SB_MentalStability.Value,
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif