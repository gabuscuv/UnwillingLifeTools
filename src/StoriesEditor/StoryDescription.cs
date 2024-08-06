#if TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using UnwillingLife.Data;

namespace UnwillingLife.Tools;
[Tool]
public partial class StoryDescription : VBoxContainer
{
    private TextEdit TE_description;
    private TextEdit TE_date;
    private TextEdit TE_cause;

    private string GetNodeName(string name) 
	{
		return $"HBC_{name}/TE_{name}";
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TE_description = GetNode<TextEdit>(GetNodeName("Description"));
		TE_date = GetNode<TextEdit>(GetNodeName("Date"));
		TE_cause = GetNode<TextEdit>(GetNodeName("Cause"));
	}

	public void SetStoryDescription(NarrativeStory storyDescription)
	{
		TE_cause.Text = storyDescription.Cause;
		TE_description.Text = storyDescription.Description;
		TE_date.Text = storyDescription.IssueDate;
	}
	public NarrativeStory GetStoryDescription()
	{
		return new()
		{
			Description = TE_description.Text,
			Cause = TE_cause.Text,
			IssueDate = TE_date.Text,
		};
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

#endif