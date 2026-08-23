using Godot;
using System;

public partial class Main : Node2D
{
	private Base _base;

	public override void _Ready()
	{
		var path = GetNode<Path>("Path");
		var enemy = GetNode<Enemy>("Enemy");

		_base = GetNode<Base>("Base");

		enemy.SetPath(path.GetPoints());

		enemy.ReachedEnd += OnEnemyReachedEnd;
	}

	private void OnEnemyReachedEnd()
	{
		_base.TakeDamage(1);
	}
}
