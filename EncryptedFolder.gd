extends KinematicBody2D

var folder_damage_particles = preload("res://FolderDamage.tscn")

var max_health = 150.0
var health = max_health

func _ready():
	position = Vector2(1920/2, 1080/2)

func _physics_process(delta):
	if health == 0:
		position += Vector2(rand_range(-5, 5), rand_range(-5, 5))
	
func hurt():
	if !get_node("/root/Globals").get_player_is_alive():
		return
	
	health -= 1
	position += Vector2(rand_range(-5, 5), rand_range(-5, 5))
	$Percent.text = str(int(max(health/max_health*100, 0))) + "%"
	
	if health == 0:
		win()
	
	var damage_particle = folder_damage_particles.instance()
	add_child(damage_particle)
	damage_particle.emitting = true
	yield(get_tree().create_timer(3), "timeout")
	damage_particle.queue_free()
		
func win():
	var tween = Tween.new()
	add_child(tween)
	
	$SfxDestroy.play()
	$CollisionShape2D.disabled = true
	$Explosion.emitting = true

	$White.visible = true
	
	tween.interpolate_property($White, "modulate", Color(1, 1, 1, 0), Color(1, 1, 1, 1), 5.0, 
	Tween.TRANS_LINEAR, Tween.EASE_IN)
	tween.start()
	yield(get_tree().create_timer(5.0), "timeout")
	
	$SecretTheme.visible = true
	$White.visible = true
	
	health = max_health
	position = Vector2(1920/2, 1080/2)
	
	tween.interpolate_property($White, "modulate", Color(1, 1, 1, 1), Color(1, 1, 1, 0), 5.0, 
	Tween.TRANS_LINEAR, Tween.EASE_IN)
	tween.start()
