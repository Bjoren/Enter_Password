extends Node2D

var enemy = preload("res://Enemy.tscn")

var enemy_base_number = 1
var enemy_difficulty_multiplier = 2

var waves = 0

var arena_center = Vector2(960, 540)

# Called when the node enters the scene tree for the first time.
#func _ready():
#	pass

func _physics_process(_delta):
	if $Spawn_timer.is_stopped():
		spawn_enemies()
		waves += 1
		$Spawn_timer.start()
		
func spawn_enemies():
	print("Enemies!")
	var difficulty = Globals.get_current_difficulty()
	
	var random_position = Vector2(1200, 0).rotated(rand_range(0.0, deg2rad(360))) + arena_center
	
	for i in enemy_base_number + difficulty * enemy_difficulty_multiplier:
		var instanced_enemy = enemy.instance()
		instanced_enemy.set_position(random_position)
		self.add_child(instanced_enemy)
		print("Instanced an enemy")
