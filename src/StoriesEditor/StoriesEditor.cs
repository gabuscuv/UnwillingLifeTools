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
    private StoryLoader storyLoader;
    private StoryStats storyStats;
    private IEnumerable<NarrativeStory> narrativeStoryList;
    private IEnumerable<NarrativePoints> narrativePointsList;
    private NarrativeIds currentNarrative;

    public override void _Ready()
	{
		storyLoader = GetNode<StoryLoader>("StoryLoader");
		//StoryLoader = GetNode<StoryLoader>("StoryDescription");
		storyStats = GetNode<StoryStats>("StoryStats");
		storyLoader.storyLoaderEventHandler += LoadStory;
		narrativeStoryList = CSVTools.LoadNarrativeStoryCSV().ToList();
		narrativePointsList = CSVTools.LoadNarrativePointsCSV().ToList();
	}

    private void LoadStory(NarrativeIds s)
    {
		throw new NotImplementedException();

		currentNarrative = s;
		storyStats
		.SetStoryStats(
			narrativePointsList
				.Where(
				p => 
					p.CharacterId == currentNarrative.CharacterId && 
					p.StoryId == currentNarrative.StoryId
				).ToList()
		);

		narrativeStoryList
		.Where(
		p => 
			p.CharacterId == currentNarrative.CharacterId && 
			p.StoryId == currentNarrative.StoryId
		).First();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
#endif