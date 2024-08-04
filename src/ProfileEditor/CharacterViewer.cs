#if TOOLS
using System;
using System.Collections.Generic;
using Godot;
using UnwillingLife.Data;

namespace UnwillingLife.Tools;
[Tool]
public partial class CharacterViewer : VBoxContainer
{
	public delegate void CharacterViewerEventHandler(NarrativeProfile narrativeProfile);
	
	public CharacterViewerEventHandler OnSaveSignal;
	private TextEdit TE_Name;
	private TextEdit TE_Education;
	private TextEdit TE_YoB;
	private TextEdit TE_History;
    private NarrativeProfile _narrativeProfile;

    private string GetTextEditNode(string name)
	{
		return $"HBC_{name}/TE_{name}";
	}
	public override void _Ready()
	{
		GetNode<Button>("B_Save").Pressed += Save;

		TE_Name = GetNode<TextEdit>(GetTextEditNode("Name"));
		TE_Education = GetNode<TextEdit>(GetTextEditNode("Education"));
		TE_YoB = GetNode<TextEdit>(GetTextEditNode("YoB"));
		TE_History = GetNode<TextEdit>(GetTextEditNode("History"));
	}

	private void Save()
	{
		_narrativeProfile.Name = TE_Name.Text;
		_narrativeProfile.Education = TE_Education.Text;
		_narrativeProfile.YearOfBirth = TE_YoB.Text;
		_narrativeProfile.History = TE_History.Text;
		OnSaveSignal.Invoke(_narrativeProfile);
    }

    public void SetCharacter(NarrativeProfile narrativeProfile) 
	{
		_narrativeProfile = narrativeProfile;
		TE_Name.Text = narrativeProfile.Name;
		TE_Education.Text = narrativeProfile.Education;
		TE_YoB.Text = narrativeProfile.YearOfBirth;
		TE_History.Text = narrativeProfile.History;
	}

	// Called when the node enters the scene tree for the first time.


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
#endif