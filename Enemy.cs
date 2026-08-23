using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : Node2D
{
	[Signal]
	public delegate void ReachedEndEventHandler();
	
	[Export]
	public float Speed { get; set; } = 100f;

	private List<Vector2> _path = new();
	private int _currentPoint = 0;

	public void SetPath(List<Vector2> path)
	{
		_path = path;
		_currentPoint = 0;

		if (_path.Count > 0)
		{
			GlobalPosition = _path[0];
		}
	}

	public override void _Process(double delta)
	{
		if (_currentPoint >= _path.Count)
		{
			EmitSignal(SignalName.ReachedEnd);
			QueueFree();

			return;
		}

		var target = _path[_currentPoint];

		GlobalPosition = GlobalPosition.MoveToward(
			target,
			Speed * (float)delta
		);

		if (GlobalPosition.DistanceTo(target) < 1f)
		{
			_currentPoint++;
		}
	}
}
