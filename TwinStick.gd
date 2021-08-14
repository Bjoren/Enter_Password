extends Node2D

var enemy = preload("res://Enemy.tscn")
var hint = preload("res://Hint.tscn")

var enemy_base_number = 2
var enemy_difficulty_multiplier = 1

var waves = 0

var arena_center = Vector2(960, 540)

# Called when the node enters the scene tree for the first time.
#func _ready():
#	pass

func _physics_process(_delta):
	if Globals.get_player_is_alive():
		if $Spawn_timer.is_stopped():
			spawn_enemies()
			waves += 1
			if waves % 2 == 0:
				print("Difficulty up!")
				Globals.increase_current_difficulty()
				spawn_hint()
			
			$Spawn_timer.start()
		
func spawn_enemies():
	var difficulty = Globals.get_current_difficulty()
	
	var random_position = Vector2(1200, 0).rotated(rand_range(0.0, deg2rad(360))) + arena_center
	
	for i in enemy_base_number + difficulty * enemy_difficulty_multiplier:
		var instanced_enemy = enemy.instance()
		instanced_enemy.set_position(random_position)
		self.add_child(instanced_enemy)

func spawn_hint():
	var random_position = Vector2(rand_range(60, 1860), rand_range(160, 1020))
	
	var instanced_hint = hint.instance()
	instanced_hint.position = random_position
	self.add_child(instanced_hint)
