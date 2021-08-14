extends KinematicBody2D

var max_health = 100.0
var health = max_health

func _ready():
	position = Vector2(1920/2, 1080/2)

#func _physics_process(delta):
#	pass
	
func hurt():
	health -= 1
	
	$Percent.text = str(int(health/max_health*100)) + "%"
	
	if health == 0:
		print("Win")
