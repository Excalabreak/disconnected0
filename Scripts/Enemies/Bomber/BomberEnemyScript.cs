using Godot;
using System;

public partial class BomberEnemyScript : BaseEnemyScript
{
    [Export] private BomberRanges _bomberRanges;

    public bool InRun { get; set; }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_bomberRanges != null)
        {
            _bomberRanges.CheckState();
        }
    }
}
