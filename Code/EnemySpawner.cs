using Godot;
using System;

public partial class EnemySpawner : Node2D
{
    [Export] public PackedScene enemyScene;
    [Export] public Path2D Path;
    [Export] public float eps = 1f;
    [Export] public float MinSpeed = 80f;
    [Export] public float MaxSpeed = 150f;

    private float spawn_rate;
    private float tus = 0;

    public override void _Ready()
    {
        spawn_rate = 1f / eps;
    }

    public override void _Process(double delta)
    {
        tus += (float)delta;
        if (tus > spawn_rate)
        {
            Spawn();
            tus = 0;
        } 
    }

    private void Spawn()
    {
        if (enemyScene == null || Path == null)
            return;

        spawn_rate = 1f / eps; 

        var enemyInstance = enemyScene.Instantiate();
        if (enemyInstance is EnemyIi enemyII)
        {
            enemyII.Path = Path;
            enemyII.Speed = (float)GD.RandRange(MinSpeed, MaxSpeed);
            AddChild(enemyII);
        }
        
    }
}