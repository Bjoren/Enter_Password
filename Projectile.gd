extends KinematicBody2D

var one_sprite = preload("res://Assets/one.png")

var velocity:Vector2 = Vector2.ZERO

func _ready():
	if randi()%2 == 1:
		$Sprite.set_texture(one_sprite)

func _physics_process(delta):
	var collision = move_and_collide(velocity * delta)
	rotation = velocity.angle()
	
	if collision:
		var collider = collision.collider
	
		if collider.is_in_group("enemies"):
			collider.hurt()
	
		$Sprite.hide()
		$CollisionShape2D.disabled = true
		$Explosion.one_shot = true
		$Explosion.emitting = true
		yield(get_tree().create_timer(1),"timeout")
		# Particles, light etc
		queue_free()

func set_velocity(new_velocity):
	velocity = new_velocity
