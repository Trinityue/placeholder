using Godot;
using System;

public partial class SniperBullets : Node2D
{
    [Export] public float Speed = 200f;
    [Export] public float SniperDamage = 10f;

    public Node2D Target { get; set; }
    public Node2D target; // Kann Enemy oder EnemyII sein

    public override void _Ready()
    {
        // Suche das nächste Ziel beim Erzeugen
        target = GetNearestEnemy();
        if (target == null)
            QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (target == null || !IsInstanceValid(target))
        {
            QueueFree();
            return;
        }
        // @CartiPlayboi33 @Kevinischgie ich arbeite hier noch dranne das ding fuckt nur ab deswegen lass ich das jetzt einfach und mach morgen weiter
        var snipertowerscene = (PackedScene)GD.Load("res://SniperTower.tscn");
        var sniperTowerInstance = snipertowerscene.Instantiate() as SniperTower;
        GD.Print(sniperTowerInstance.AttackRange);
        // Der teil bis hier 

        Vector2 direction = (target.GlobalPosition - GlobalPosition).Normalized();
        GlobalPosition += direction * Speed * (float)delta;

        // Verschwinde, wenn Ziel erreicht
        if (GlobalPosition.DistanceTo(target.GlobalPosition) < 10f)
        {
            // Versuche TakeDamage aufzurufen, falls vorhanden
            var method = target.GetType().GetMethod("TakeDamage");
            if (method != null)
                method.Invoke(target, new object[] { SniperDamage });

            QueueFree();
        }
    }

    private Node2D GetNearestEnemy()
    {
        Node2D nearest = null;
        float nearestDist = float.MaxValue;
        foreach (var node in GetTree().GetNodesInGroup("Enemies"))
        {
            if (node is Node2D e)
            {
                float dist = GlobalPosition.DistanceTo(e.GlobalPosition);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = e;
                }
            }
        }
        return nearest;
    }
}