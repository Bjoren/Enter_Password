extends KinematicBody2D

export var acceleration:int = 700
export var turn_speed:float = 0.03

var velocity:Vector2 = Vector2.ZERO

var random_angle_variance:float = 0.7
var angle_timer_variance = Vector2(5, 60)

var random_angle:float = 0.0
var angle_change_timer:int = 0

func _ready():
	randomize_angle()

func _physics_process(_delta):
	var player_position = Globals.get_player_position()
	
	var new_velocity = Vector2.ZERO
	if Globals.get_player_is_alive():
		new_velocity = Vector2(-acceleration, 0).rotated(global_position.angle_to_point(player_position) + random_angle)
	
	velocity = lerp(velocity, new_velocity, turn_speed)
	move_and_slide(velocity)
	
	for slide in get_slide_count():
		var collision = get_slide_collision(slide)
		if collision.get_collider().is_in_group("player"):
			collision.collider.hurt()
	
	look_at(Globals.get_player_position())
	
	angle_change_timer -= 1
	if angle_change_timer == 0:
		randomize_angle()

func hurt():
	$CollisionShape2D.disabled = true
	$Sprite.visible = false
	$Explosion.emitting = true
	yield(get_tree().create_timer(2),"timeout")
	queue_free()

func randomize_angle():
	angle_change_timer = randi() % int(angle_timer_variance.x) + int(angle_timer_variance.y)
	random_angle = rand_range(random_angle_variance * -1, random_angle_variance)
