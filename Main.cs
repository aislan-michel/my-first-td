using Godot;
using System;

public partial class Main : Node2D
{
	public override void _Ready()
	{
		var path = GetNode<Path>("Path");
		var enemy = GetNode<Enemy>("Enemy");

		enemy.SetPath(path.GetPoints());
	}
}
