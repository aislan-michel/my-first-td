using Godot;
using System;
using System.Collections.Generic;

public partial class Path : Node2D
{
	public List<Vector2> GetPoints()
	{
		var points = new List<Vector2>();

		foreach (Node child in GetChildren())
		{
			if (child is Marker2D marker)
			{
				points.Add(marker.GlobalPosition);
			}
		}

		return points;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
