extends KinematicBody2D

var projectile = preload("res://Projectile.tscn")

export var player_acceleration:int = 500
export var turn_speed:float = 0.05

export var fire_rate: int = 15
export var projectile_speed:int = 1200

var fire_cooldown:int = 0
var velocity:= Vector2.ZERO

var is_alive:bool = true

func _physics_process(_delta):
	if is_alive:
		var horizontal_acceleration = 0
		var vertical_acceleration = 0
		
		if fire_cooldown > 0:
			fire_cooldown -= 1
		
		if Input.is_action_pressed("ui_left"):
			horizontal_acceleration -= player_acceleration
		if Input.is_action_pressed("ui_right"):
			horizontal_acceleration += player_acceleration
		if Input.is_action_pressed("ui_up"):
			vertical_acceleration -= player_acceleration
		if Input.is_action_pressed("ui_down"):
			vertical_acceleration += player_acceleration
			
		move(horizontal_acceleration, vertical_acceleration)
		
		if Input.is_action_pressed("fire") && fire_cooldown == 0:
			fire()
			fire_cooldown = fire_rate

func fire():
	var projectile_instance = projectile.instance()
	
	projectile_instance.position = global_position
	projectile_instance.set_velocity(Vector2(projectile_speed, 0).rotated(global_rotation))
	
	get_tree().get_root().add_child(projectile_instance)

func move(horizontal_acceleration, vertical_acceleration):
	velocity = lerp(velocity, Vector2(horizontal_acceleration, vertical_acceleration), turn_speed)
	move_and_slide(velocity)
	look_at(get_global_mouse_position())
	get_node("/root/Globals").set_player_position(global_position)

func hurt():
	is_alive = false
	$Hurtbox.disabled = true
	$PlayerSprite.visible = false
	$Light2D.visible = false
	$Ones.emitting = false
	$Zeroes.emitting = false
	$Explosion.emitting = true
	Globals.set_player_is_alive(false)
