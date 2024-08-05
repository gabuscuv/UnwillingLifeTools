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
    private TextEdit te_Description;
    private TextEdit te_Date;
    private TextEdit te_Cause;

    private string GetNodeName(string name) 
	{
		return $"HBC_{name}/TE_{name}";
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		te_Description = GetNode<TextEdit>(GetNodeName("Description"));
		te_Date = GetNode<TextEdit>(GetNodeName("Date"));
		te_Cause = GetNode<TextEdit>(GetNodeName("Cause"));
	}

	public void SetStoryDescription(NarrativeStory storyDescription)
	{
		te_Cause.Text = storyDescription.Cause;
		te_Description.Text = storyDescription.Description;
		te_Date.Text = storyDescription.IssueDate;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

#endif