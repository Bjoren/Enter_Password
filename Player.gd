extends KinematicBody2D

export var player_acceleration:int = 500
export var turn_speed:float = 0.05

var velocity:= Vector2.ZERO

# Called when the node enters the scene tree for the first time.
#func _ready():
#	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process():
	var horizontal_acceleration = 0
	var vertical_acceleration = 0
	
	if Input.is_action_pressed("ui_left"):
		horizontal_acceleration -= player_acceleration
	if Input.is_action_pressed("ui_right"):
		horizontal_acceleration += player_acceleration
	if Input.is_action_pressed("ui_up"):
		vertical_acceleration -= player_acceleration
	if Input.is_action_pressed("ui_down"):
		vertical_acceleration += player_acceleration
	
	move(horizontal_acceleration, vertical_acceleration)

func move(horizontal_acceleration, vertical_acceleration):
	velocity = lerp(velocity, Vector2(horizontal_acceleration, vertical_acceleration), turn_speed)
	move_and_slide(velocity)
	look_at(get_global_mouse_position())
	get_node("/root/Globals").set_player_position(global_position)
