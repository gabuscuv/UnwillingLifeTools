@tool
extends HBoxContainer

# Called when the node enters the scene tree for the first time.
func _enter_tree():
	get_node("./ProfileEditor").hide();
	get_node("./StoryEditor").hide();
	#pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_item_selected(index:int):
	match index:	
		0: 
			get_node("ProfileEditor").show();
			get_node("StoryEditor").hide();
		1: 
			get_node("ProfileEditor").hide();
			get_node("StoryEditor").show();


func _on_item_list_item_clicked(index:int, at_position:Vector2, mouse_button_index:int):
	_on_item_selected(index);
