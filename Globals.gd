extends Node

var player_position:Vector2 = Vector2.ZERO
var current_difficulty:int = 1
var player_is_alive = true
var fx_manager:Node = null
var in_hacker_mode = false

func _init():
	var player_position:Vector2 = Vector2.ZERO
	var current_difficulty:int = 1
	var player_is_alive = true
	var fx_manager:Node = null
	var in_hacker_mode = false

func get_player_position() -> Vector2:
	return player_position
	
func set_player_position(new_position):
	player_position = new_position

func get_current_difficulty() -> int:
	return current_difficulty

func increase_current_difficulty():
	current_difficulty += 1
	
func reset_current_difficulty():
	current_difficulty = 1

func set_player_is_alive(is_alive:bool):
	player_is_alive = is_alive

func get_player_is_alive() -> bool:
	return player_is_alive

func is_in_hacker_mode () -> bool:
	return in_hacker_mode
