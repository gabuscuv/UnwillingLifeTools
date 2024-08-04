#if TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using UnwillingLife.Data;

namespace UnwillingLife.Tools;
public delegate void StoryLoaderEventHandler(NarrativeIds s);
[Tool]
public partial class StoryLoader : HBoxContainer
{
	
	public StoryLoaderEventHandler storyLoaderEventHandler;
    private List<NarrativeBase> NarrativeProfile;
	private OptionButton CharacterOptionButton;
    private SpinBox SB_StoryIndex;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		CharacterOptionButton = GetNode<OptionButton>("MainBody/OB_CharacterList");
		SB_StoryIndex = GetNode<SpinBox>("MainBody/HBoxContainer/SB_StoryIndex");
		NarrativeProfile = CSVTools.LoadNarrativeProfile().ToList<NarrativeBase>();
		RefreshCharacterList();
		GetNode<OptionButton>("MainBody/B_Load").Pressed += LoadButtonPressed;
	}

    private void LoadButtonPressed()
    {

		storyLoaderEventHandler.Invoke(new()
		{
			CharacterId =
			NarrativeProfile.ElementAt(CharacterOptionButton.GetIndex()).CharacterId,
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