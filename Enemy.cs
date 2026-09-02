using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : Area2D
{
	[Signal]
	public delegate void ReachedEndEventHandler();
	
	[Signal]
	public delegate void DiedEventHandler();
	
	[Export]
	public float Speed { get; set; } = 100f;

	private List<Vector2> _path = new();
	private int _currentPoint = 0;
	
	[Export]
	public int MaxHealth { get; set; } = 50;

	private int _health;
	private ProgressBar _healthBar;
	
	public void SetPath(List<Vector2> path)
	{
		_path = path;
		_currentPoint = 0;

		if (_path.Count > 0)
		{
			GlobalPosition = _path[0];
		}
	}
	
	public void TakeDamage(int damage)
	{
		_health -= damage;

		_healthBar.Value = _health;

		GD.Print($"Enemy tomou {damage} de dano. Vida: {_health}");

		if (_health <= 0)
		{
			Die();
		}
	}
	
	private void Die()
	{
		GD.Print("💀 Enemy morreu!");

		EmitSignal(SignalName.Died);

		QueueFree();
	}
	
	public override void _Ready()
	{
		_health = MaxHealth;

		_healthBar = GetNode<ProgressBar>("HealthBar");

		_healthBar.MaxValue = MaxHealth;
		_healthBar.Value = _health;
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
