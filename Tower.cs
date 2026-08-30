using Godot;
using System.Collections.Generic;

public partial class Tower : Node2D
{
	private Area2D _attackRange;
	private List<Enemy> _enemiesInRange = new();
	private Timer _attackTimer;

	public override void _Ready()
	{
		_attackRange = GetNode<Area2D>("AttackRange");
		_attackTimer = GetNode<Timer>("AttackTimer");

		_attackRange.AreaEntered += OnAreaEntered;
		_attackRange.AreaExited += OnAreaExited;

		_attackTimer.Timeout += OnAttackTimerTimeout;

		_attackTimer.Start();

		GD.Print("=== TOWER READY ===");
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Enemy enemy)
		{
			_enemiesInRange.Add(enemy);

			GD.Print($"Enemy entrou. Total no alcance: {_enemiesInRange.Count}");
		}
	}

	private void OnAreaExited(Area2D area)
	{
		if (area is Enemy enemy)
		{
			_enemiesInRange.Remove(enemy);

			GD.Print($"Enemy saiu. Total no alcance: {_enemiesInRange.Count}");
		}
	}
	
	private Enemy GetTarget()
	{
		if (_enemiesInRange.Count == 0)
		{
			return null;
		}

		return _enemiesInRange[0];
	}
	
	private void OnAttackTimerTimeout()
	{
		var target = GetTarget();

		if (target == null)
		{
			return;
		}

		const int damage = 10;

		GD.Print($"🏹 Torre atacou: {target}");

		target.TakeDamage(damage);
	}
}
