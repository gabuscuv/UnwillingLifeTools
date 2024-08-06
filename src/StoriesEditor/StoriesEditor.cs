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
	private StoryLoader _storyLoader;
	private StoryDescription _storyDescription;
	private StoryStats _storyStats;
	private List<NarrativeStory> _narrativeStoryList;
	private List<NarrativePoints> _narrativePointsList;

	public override async void _Ready()
	{
		var storyLoader = GetNode<StoryLoader>("StoryLoader");
		storyLoader.StoryLoaderEvent += LoadStory;
		storyLoader.StorySaverEvent += SaveStory;
		_storyDescription = GetNode<StoryDescription>("StoryDescription");
		_storyStats = GetNode<StoryStats>("StoryStats");
		try
		{
			_narrativeStoryList = CSVTools.LoadNarrativeStoryCSV().ToList();
		}
		catch (Exception ex)
		{
			_narrativeStoryList = new();
			Debug.Print("Warning, NarrativeStoryFile invalid: "+ex.Message);
		}
		try
		{
			_narrativePointsList = CSVTools.LoadNarrativePointsCSV().ToList();
		}
		catch (Exception ex) 
		{
			_narrativePointsList = new();
			Debug.Print("Warning, NarrativePointsFile invalid: "+ex.Message);
		}
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
		var w = _narrativePointsList
				.Where(
				p =>
					p.CharacterId == currentNarrative.CharacterId &&
					p.StoryId == currentNarrative.StoryId
				).ToList();
		_storyStats
		.SetStoryStats(
			w.Count != 0 ? w : GenerateEmptyEntry(currentNarrative)
		);
		_storyDescription.SetStoryDescription(
			_narrativeStoryList
				.Where(
				p =>
					p.CharacterId == currentNarrative.CharacterId &&
					p.StoryId == currentNarrative.StoryId
				).First()
		);
	}

	private void SaveStory(NarrativeIds currentNarrative)
	{
		SaveDescription(currentNarrative);
		CSVTools.SaveNarrativeStory(_narrativeStoryList);
		SaveStats(currentNarrative);
		CSVTools.SaveNarrativePoints(_narrativePointsList);
	}

	private void SaveStats(NarrativeIds currentNarrative)
	{
		IEnumerable<NarrativePoints> list;

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
			list = _narrativePointsList.Where(p =>
						p.CharacterId == currentNarrative.CharacterId &&
						p.StoryId == currentNarrative.StoryId &&
						p.Choice == story.Choice
						);

			if (list.Any())
			{
				_narrativePointsList
				[
					_narrativePointsList.IndexOf(list.First())
				] = story;
			}
			else
			{
				_narrativePointsList.Add(story);
			}
		}

	}



	private void SaveDescription(NarrativeIds currentNarrative)
	{
		var storySDescription = _storyDescription.GetStoryDescription();
		storySDescription.CharacterId = currentNarrative.CharacterId;
		storySDescription.StoryId = currentNarrative.StoryId;

		IEnumerable<NarrativeStory> list = _narrativeStoryList.Where(p =>
					p.CharacterId == currentNarrative.CharacterId &&
					p.StoryId == currentNarrative.StoryId
					);

		if (list.Any())
		{
			_narrativeStoryList
			[
				_narrativeStoryList.IndexOf(list.First())
			] = storySDescription;
		}
		else
		{
			_narrativeStoryList.Add(storySDescription);
		}
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif