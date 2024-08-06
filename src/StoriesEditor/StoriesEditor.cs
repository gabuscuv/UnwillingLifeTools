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
	private StoryStats _storyStats;
	private List<NarrativeStory> narrativeStoryList;
	private List<NarrativePoints> narrativePointsList;

	public override async void _Ready()
	{
		var storyLoader = GetNode<StoryLoader>("StoryLoader");
		storyLoader.StoryLoaderEvent += LoadStory;
		storyLoader.StorySaverEvent += SaveStory;
		storyDescription = GetNode<StoryDescription>("StoryDescription");
		_storyStats = GetNode<StoryStats>("StoryStats");
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

	private void LoadStory(NarrativeIds currentNarrative)
	{
		var w = narrativePointsList
				.Where(
				p =>
					p.CharacterId == currentNarrative.CharacterId &&
					p.StoryId == currentNarrative.StoryId
				).ToList();
		_storyStats
		.SetStoryStats(
			w.Count != 0 ? w : GenerateEmptyEntry(currentNarrative)
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

	private void SaveStory(NarrativeIds currentNarrative)
	{
		Debug.WriteLine("Hi!");
		SaveDescription(currentNarrative);
		CSVTools.SaveNarrativeStory(narrativeStoryList);
		SaveStats(currentNarrative);
		CSVTools.SaveNarrativePoints(narrativePointsList);
	}

	private void SaveStats(NarrativeIds currentNarrative)
	{
		var storyStats = this._storyStats.GetStoryStats();
		storyStats.Select(e =>
		{
			e.CharacterId = currentNarrative.CharacterId;
			e.StoryId = currentNarrative.StoryId;
			return e;
		});
		foreach (var story in storyStats

				)
		{
			IEnumerable<NarrativePoints> list = narrativePointsList.Where(p =>
						p.CharacterId == currentNarrative.CharacterId &&
						p.StoryId == currentNarrative.StoryId &&
						p.Choice == story.Choice
						);

			if (list.Any())
			{
				narrativePointsList
				[
					narrativePointsList.IndexOf(list.First())
				] = story;
			}
			else
			{
				narrativePointsList.Add(story);
			}
		}

	}



	private void SaveDescription(NarrativeIds currentNarrative)
	{
		var storySDescription = storyDescription.GetStoryDescription();
		storySDescription.CharacterId = currentNarrative.CharacterId;
		storySDescription.StoryId = currentNarrative.StoryId;

		IEnumerable<NarrativeStory> list = narrativeStoryList.Where(p =>
					p.CharacterId == currentNarrative.CharacterId &&
					p.StoryId == currentNarrative.StoryId
					);

		if (list.Any())
		{
			narrativeStoryList
			[
				narrativeStoryList.IndexOf(list.First())
			] = storySDescription;
		}
		else
		{
			narrativeStoryList.Add(storySDescription);
		}
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif