using Godot;

public partial class EnemySpawner : Node
{
	private PackedScene _enemyScene;
	private Path _path;
	private Timer _spawnTimer;
	
	private int _currentWave = 0;
	private int _enemiesToSpawn = 0;
	private int _enemiesSpawned = 0;
	private int _aliveEnemies = 0;

	public override void _Ready()
	{
		_enemyScene = GD.Load<PackedScene>("res://Enemy.tscn");

		_path = GetParent().GetNode<Path>("Path");

		_spawnTimer = GetNode<Timer>("SpawnTimer");

		_spawnTimer.Timeout += OnSpawnTimerTimeout;

		StartNextWave();
	}

	private void SpawnEnemy()
	{
		var enemy = _enemyScene.Instantiate<Enemy>();

		AddChild(enemy);

		var points = _path.GetPoints();

		enemy.SetPath(points);

		enemy.ReachedEnd += OnEnemyReachedEnd;
		enemy.Died += OnEnemyDied;

		_aliveEnemies++;

		GD.Print($"Enemy criado. Vivos: {_aliveEnemies}");
	}
	
	private void OnEnemyDied()
	{
		_aliveEnemies--;

		GD.Print($"💀 Enemy morreu. Vivos: {_aliveEnemies}");

		CheckWaveFinished();
	}
	
	private void OnEnemyReachedEnd()
	{
		_aliveEnemies--;

		GD.Print($"Enemy chegou à base. Vivos: {_aliveEnemies}");

		CheckWaveFinished();
	}
	
	private void CheckWaveFinished()
	{
		if (_aliveEnemies > 0)
		{
			return;
		}

		if (_enemiesSpawned < _enemiesToSpawn)
		{
			return;
		}

		StartNextWave();
	}

	private void OnSpawnTimerTimeout()
	{
		SpawnEnemy();

		_enemiesSpawned++;

		if (_enemiesSpawned >= _enemiesToSpawn)
		{
			_spawnTimer.Stop();

			GD.Print($"Wave {_currentWave} terminou de spawnar.");
		}
	}
	
	private void StartNextWave()
	{
		_currentWave++;

		_enemiesToSpawn = 4 + _currentWave;
		_enemiesSpawned = 0;

		GD.Print($"=== WAVE {_currentWave} ===");
		GD.Print($"Inimigos: {_enemiesToSpawn}");

		_spawnTimer.Start();
	}
}
