extends Label


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	var current_level = Globals.get_current_level()
	text = "LEVEL " + str(current_level)
	$Particles2D.emitting = true
	$Tween.interpolate_property(self, "modulate", Color(1, 1, 1, 1), Color(1, 1, 1, 0), 1.5, Tween.TRANS_LINEAR, Tween.EASE_IN)
	$Tween.start()

	$Tween.connect("tween_all_completed", self, "queue_free")
