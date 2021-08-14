extends Node

var player_position:Vector2 = Vector2.ZERO
var current_difficulty:int = 1

func get_player_position():
	return player_position
	
func set_player_position(new_position):
	player_position = new_position

func get_current_difficulty():
	return current_difficulty
