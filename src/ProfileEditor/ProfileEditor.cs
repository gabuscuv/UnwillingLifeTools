#if TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using UnwillingLife.Data;

namespace UnwillingLife.Tools;
[Tool]
public partial class ProfileEditor : HBoxContainer
{
	private List<NarrativeProfile> NarrativeProfile;
	private ItemList CharacterList;
    private CharacterViewer CharacterViewer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		NarrativeProfile = CSVTools.GetNarrativeCSV().ToList();
		CharacterList = GetNode<ItemList>("CharacterBox/CharacterList");
		CharacterViewer = GetNode<CharacterViewer>("CharacterViewer");
		CharacterViewer.OnSaveSignal += CharacterViewer_OnSaveSignal;
		RefreshCharacterList();
	}

    private void CharacterViewer_OnSaveSignal(NarrativeProfile narrativeProfile)
    {
		
		NarrativeProfile[
			NarrativeProfile.FindIndex(e=>e.CharacterId==narrativeProfile.CharacterId)
			] = narrativeProfile;
			
		CSVTools.SaveNarrativeCSV(NarrativeProfile);
    }

    private void RefreshCharacterList()
	{
		CharacterList.Clear();
		foreach (var item in NarrativeProfile)
		{
			CharacterList.AddItem(item.Name);
		}
	}

	public void ClickedCharacterInCharacterList(int index, Vector2 position, int mouse_button_index) 
	{
		CharacterViewer.SetCharacter(NarrativeProfile.ElementAt(index));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif