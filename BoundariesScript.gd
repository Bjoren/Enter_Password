extends KinematicBody2D

var north: Line2D
var south: Line2D
var east: Line2D
var west: Line2D

var defaultColor: Color
var targetColor: Color
var stepCount: float = 3.0

func _ready():
	north = get_node("LineNorth")
	south = get_node("LineSouth")
	east = get_node("LineEast")
	west = get_node("LineWest")
	
	defaultColor = north.default_color
	targetColor = Color(1,0,0,1)
	
func _process(delta):
	var northCount: int = 0
	var southCount: int = 0
	var eastCount: int = 0
	var westCount: int = 0
	
	var enemies = get_tree().get_nodes_in_group("enemies")
	
	for enemy in enemies:
		if enemy.position.y < north.position.y:
			northCount = northCount + 1
			
		if enemy.position.y > south.position.y:
			southCount = southCount + 1
			
		if enemy.position.x > east.position.x:
			eastCount = eastCount + 1
			
		if enemy.position.x < west.position.x:
			westCount = westCount + 1

	north.default_color = lerp(defaultColor, targetColor, northCount / stepCount)
	south.default_color = lerp(defaultColor, targetColor, southCount / stepCount)
	east.default_color = lerp(defaultColor, targetColor, eastCount / stepCount)
	west.default_color = lerp(defaultColor, targetColor, westCount / stepCount)
		
