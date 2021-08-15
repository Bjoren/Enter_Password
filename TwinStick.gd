extends Node2D

var enemy = preload("res://Enemy.tscn")
var hint = preload("res://Hint.tscn")
var folder = preload("res://EncryptedFolder.tscn")

var enemy_base_number = 1
var enemy_difficulty_multiplier = 1

var waves = 0

var arena_center = Vector2(960, 540)
var boss_spawned:bool = false

# Called when the node enters the scene tree for the first time.
#func _ready():
#	pass

func _physics_process(_delta):
	if Globals.get_player_is_alive():
		if $Spawn_timer.is_stopped():
			spawn_enemies()
			waves += 1
			if waves % 2 == 0 && Globals.get_current_level() < 3:
				spawn_hint()
			if waves % 8 == 0:
				Globals.increase_current_difficulty()
			
			$Spawn_timer.start()
	else:
		kill_enemies()
		
	if Globals.get_current_level() > 2 && !boss_spawned:
		var folder_instance = folder.instance()
		self.add_child(folder_instance)
		boss_spawned = true
		
func spawn_enemies():
	var difficulty = Globals.get_current_difficulty()
	
	var random_position = Vector2(1200, 0).rotated(rand_range(0.0, deg2rad(360))) + arena_center
	
	for i in enemy_base_number + difficulty * enemy_difficulty_multiplier:
		var instanced_enemy = enemy.instance()
		instanced_enemy.set_position(random_position)
		self.add_child(instanced_enemy)

func kill_enemies():
	for enemy in get_tree().get_nodes_in_group("enemies"):
		enemy.hurt()
	for pickup in get_tree().get_nodes_in_group("pickup"):
		pickup.queue_free()

func spawn_hint():
	var random_position = Vector2(rand_range(60, 1860), rand_range(160, 1020))
	
	var instanced_hint = hint.instance()
	instanced_hint.position = random_position
	self.add_child(instanced_hint)
