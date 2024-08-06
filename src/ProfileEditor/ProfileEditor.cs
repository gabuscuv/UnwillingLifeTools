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
	private List<NarrativeProfile> _narrativeProfileList;
	private ItemList IL_characterList;
    private CharacterViewer _characterViewer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_narrativeProfileList = CSVTools.LoadNarrativeProfile().ToList();
		IL_characterList = GetNode<ItemList>("CharacterBox/CharacterList");
		_characterViewer = GetNode<CharacterViewer>("CharacterViewer");
		_characterViewer.OnSaveSignal += CharacterViewer_OnSaveSignal;
		RefreshCharacterList();
	}

    private void CharacterViewer_OnSaveSignal(NarrativeProfile narrativeProfile)
    {
		
		_narrativeProfileList[
			_narrativeProfileList.FindIndex(e=>e.CharacterId==narrativeProfile.CharacterId)
			] = narrativeProfile;
			
		CSVTools.SaveNarrativeProfile(_narrativeProfileList);
    }

    private void RefreshCharacterList()
	{
		IL_characterList.Clear();
		foreach (var item in _narrativeProfileList)
		{
			IL_characterList.AddItem(item.Name);
		}
	}

	public void ClickedCharacterInCharacterList(int index, Vector2 position, int mouse_button_index) 
	{
		_characterViewer.SetCharacter(_narrativeProfileList.ElementAt(index));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif