extends KinematicBody2D

var health = 40

func _ready():
	position = Vector2(100 + randi() % 1720, 200 + randi() % 880)

func _physics_process(delta):
	pass
	
func hurt():
	health -= 1
	
	if health == 0:
		print("Win")
