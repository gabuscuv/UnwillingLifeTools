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
    private List<NarrativeBase> NarrativeProfile;
	private OptionButton CharacterOptionButton;
    private SpinBox SB_StoryIndex;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		CharacterOptionButton = GetNode<OptionButton>("MainBody/OB_CharacterList");
		SB_StoryIndex = GetNode<SpinBox>("MainBody/HBoxContainer/SB_StoryIndex");
		
		GetNode<Button>("MainBody/B_Refresh").Pressed += RefreshCharacterList;
		GetNode<Button>("MainBody/B_Load").Pressed += LoadButtonPressed;
		NarrativeProfile = CSVTools.LoadNarrativeProfile().ToList<NarrativeBase>();
	}

    private void LoadButtonPressed()
    {
		// Debug.WriteLine(@$"
		// Length: {NarrativeProfile.Count}
		// NarrativeProfile: {CharacterOptionButton.GetSelectableItem()} - {SB_StoryIndex.Value}
		// ");
		StoryLoaderEvent.Invoke(new NarrativeIds()
		{
			CharacterId =
			NarrativeProfile.ElementAt(CharacterOptionButton.GetSelectableItem()).CharacterId,
			StoryId = (int)SB_StoryIndex.Value
		});
    }

    private void RefreshCharacterList()
	{
		CharacterOptionButton.Clear();
		foreach (var item in NarrativeProfile)
		{
			CharacterOptionButton.AddItem(item.Name);
		}
	}

}
#endif