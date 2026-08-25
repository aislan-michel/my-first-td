using Godot;

public partial class Tower : Node2D
{
	private Area2D _attackRange;

	public override void _Ready()
	{
		_attackRange = GetNode<Area2D>("AttackRange");

		_attackRange.AreaEntered += OnAreaEntered;
		_attackRange.AreaExited += OnAreaExited;

		GD.Print("=== TOWER READY ===");
	}

	private void OnAreaEntered(Area2D area)
	{
		GD.Print($"🎯 Área detectada: {area}");

		if (area is Enemy enemy)
		{
			GD.Print($"🎯 ENEMY ENTROU NO ALCANCE: {enemy}");
		}
	}

	private void OnAreaExited(Area2D area)
	{
		GD.Print($"Área saiu: {area}");

		if (area is Enemy enemy)
		{
			GD.Print($"Enemy saiu do alcance: {enemy}");
		}
	}
}
