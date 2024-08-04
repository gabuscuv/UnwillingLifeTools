#if TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using UnwillingLife.Data;

namespace UnwillingLife.Tools;
[Tool]
public partial class StoriesEditor : VBoxContainer
{
    private List<NarrativeProfile> NarrativeProfile;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		NarrativeProfile = CSVTools.GetNarrativeCSV().ToList();
		RefreshCharacterList();
	}

    private void RefreshCharacterList()
	{
		CharacterList.Clear();
		foreach (var item in NarrativeProfile)
		{
			CharacterList.AddItem(item.Name);
		}
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif