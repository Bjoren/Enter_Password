extends KinematicBody2D

export var acceleration:int = 600
export var turn_speed:float = 0.03

var velocity:Vector2 = Vector2.ZERO

func _ready():
	pass # Självaste hjärtat i modermodemet

func _physics_process(_delta):
	var player_position = Globals.get_player_position()
	var new_velocity = Vector2(-acceleration, 0).rotated(global_position.angle_to_point(player_position))
	
	velocity = lerp(velocity, new_velocity, turn_speed)
	move_and_slide(velocity)
	look_at(Globals.get_player_position())
