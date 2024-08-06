#if TOOLS
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Godot;
using UnwillingLife.Data;

namespace UnwillingLife.Tools;
[Tool]
public partial class StoryLoader : HBoxContainer
{
	public delegate void StoryLoaderEventHandler(NarrativeIds s);

	public StoryLoaderEventHandler StoryLoaderEvent { get; set; }
	public StoryLoaderEventHandler StorySaverEvent { get; set; }

    private List<NarrativeBaseWithName> _narrativeProfileList;
	private OptionButton OB_character;
    private SpinBox SB_storyIndex;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		OB_character = GetNode<OptionButton>("MainBody/OB_CharacterList");
		SB_storyIndex = GetNode<SpinBox>("MainBody/HBoxContainer/SB_StoryIndex");
		
		GetNode<Button>("MainBody/B_Refresh").Pressed += RefreshCharacterList;
		GetNode<Button>("MainBody/B_Load").Pressed += LoadButtonPressed;
		GetNode<Button>("MainBody/B_Save").Pressed += SaveButtonPressed;
		_narrativeProfileList = CSVTools.LoadNarrativeProfile().ToList<NarrativeBaseWithName>();
	}

    private void LoadButtonPressed() => 
		StoryLoaderEvent.Invoke(GetCharacterIds());

    private void SaveButtonPressed() => 
		StorySaverEvent.Invoke(GetCharacterIds());

    private NarrativeIds GetCharacterIds() =>
		new NarrativeIds()
		{
			CharacterId =
						_narrativeProfileList
							.ElementAt(OB_character.GetSelectedId())
							.CharacterId,
			StoryId = (int)SB_storyIndex.Value
		};

    private void RefreshCharacterList()
	{
		OB_character.Clear();
		foreach (var item in _narrativeProfileList)
		{
			OB_character.AddItem(item.Name);
		}
	}

}
#endif