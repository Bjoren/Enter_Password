extends Label


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	var current_level = Globals.get_current_level() + 1
	text = "LEVEL " + str(current_level)
	$Particles2D.emitting = true
	$Tween.interpolate_property(modulate, Color(255, 255, 255, 255), Color(255, 255, 255, 0), Tween.TRANS_LINEAR, Tween.EASE_IN)
	$Tween.connect("tween_all_completed", self, "queue_free")
