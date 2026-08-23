using Godot;
using System;

public partial class Base : Node2D
{
	[Export]
	public int Health { get; private set; } = 3;

	public void TakeDamage(int damage)
	{
		Health -= damage;

		GD.Print($"Base recebeu {damage} de dano. Vida: {Health}");

		if (Health <= 0)
		{
			GD.Print("GAME OVER!");
		}
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
