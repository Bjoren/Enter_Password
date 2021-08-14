extends Area2D

var letter_cycle_time:int = 10
var letter_cycle_countdown:int = letter_cycle_time

var collected:bool = false

const ALPHABET = ["A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"]

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(_delta):
	if !collected:
		letter_cycle_countdown -= 1
		if letter_cycle_countdown == 0:
			letter_cycle_countdown = letter_cycle_time
			$Label.set_text(get_random_character())
			
		for body in get_overlapping_bodies():
			if body.is_in_group("player"):
				collect()

func collect():
	$CollisionShape2D.disabled = true
	$Particles2D.emitting = false
	$Label.text = "HINT!"
	collected = true
	
	$Sfx.play()
	
	$Tween.interpolate_property(self, "modulate", 
	Color(1, 1, 1, 1), Color(1, 1, 1, 0), 2.0, 
	Tween.TRANS_LINEAR, Tween.EASE_IN)
	
	get_node("../../Hangman/HangmanLogic").GiveHintChar()
	
	$Tween.start()
	$Tween.connect("tween_all_completed", self, "delete")

func get_random_character():
	return ALPHABET[randi()%ALPHABET.size()]
	
func delete():
	queue_free()
