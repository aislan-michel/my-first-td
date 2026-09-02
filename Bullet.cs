using Godot;

public partial class Bullet : Node2D
{
	private Enemy _target;

	private int _damage;
	private float _speed;

	public void Initialize(Enemy target, int damage, float speed)
	{
		_target = target;
		_damage = damage;
		_speed = speed;
	}

	public override void _Process(double delta)
	{
		if (_target == null || !IsInstanceValid(_target))
		{
			QueueFree();
			return;
		}

		Vector2 direction = GlobalPosition.DirectionTo(_target.GlobalPosition);

		float movement = _speed * (float)delta;

		GlobalPosition += direction * movement;

		if (GlobalPosition.DistanceTo(_target.GlobalPosition) < 10f)
		{
			HitTarget();
		}
	}

	private void HitTarget()
	{
		_target.TakeDamage(_damage);

		QueueFree();
	}
}
