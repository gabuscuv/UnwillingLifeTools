#if TOOLS
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using UnwillingLife.Data;

namespace UnwillingLife.Tools;
[Tool]
public partial class StoriesEditor : VBoxContainer
{
	private StoryLoader storyLoader;
	private StoryDescription storyDescription;
	private StoryStats storyStats;
	private IEnumerable<NarrativeStory> narrativeStoryList;
	private IEnumerable<NarrativePoints> narrativePointsList;
	private NarrativeIds currentNarrative;

	public override async void _Ready()
	{
		GetNode<StoryLoader>("StoryLoader").StoryLoaderEvent += LoadStory;
		storyDescription = GetNode<StoryDescription>("StoryDescription");
		storyStats = GetNode<StoryStats>("StoryStats");
		narrativeStoryList = CSVTools.LoadNarrativeStoryCSV().ToList();
		narrativePointsList = CSVTools.LoadNarrativePointsCSV().ToList();
	}


	private IList<NarrativePoints> GenerateEmptyEntry(NarrativeIds s) 
	{
		return Enum.GetValues<ENarrativeChoice>()
		.Select(e => new NarrativePoints() 
		{ CharacterId = s.CharacterId, StoryId = s.StoryId, Choice = e }
		).ToList();
	 }

    private void LoadStory(NarrativeIds s)
	{

		currentNarrative = s;

		var w = narrativePointsList
				.Where(
				p =>
					p.CharacterId == currentNarrative.CharacterId &&
					p.StoryId == currentNarrative.StoryId
				).ToList();
		storyStats
		.SetStoryStats(
			w.Count != 0 ? w : GenerateEmptyEntry(s)
		);
		storyDescription.SetStoryDescription(
			narrativeStoryList
				.Where(
				p =>
					p.CharacterId == currentNarrative.CharacterId &&
					p.StoryId == currentNarrative.StoryId
				).First()
		);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif