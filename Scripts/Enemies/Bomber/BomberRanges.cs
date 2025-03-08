using Godot;
using System;
using System.Net;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/08/2025]
 * [switches bool in _bomberEnemyScript
 * (could technically use a state machine, 
 * but it's 2 states)]
 */

public partial class BomberRanges : Node2D
{
    [Export] private Node2D _bomber;
    [Export] private BomberEnemyScript _bomberEnemyScript;

    //distance to swithch run to deploy
    [Export] private float _disRunSwitch = 500f;

    //distance to swithch deploy to run
    [Export] private float _disDeploySwitch = 500f;

    public void CheckState()
    {
        if (_bomberEnemyScript.InRun)
        {
            CheckDistance(_disRunSwitch);
        }
        else
        {
            CheckDistance(_disDeploySwitch);
        }
    }

    private void CheckDistance(float dis)
    {
        if (_bomber.GlobalPosition.DistanceTo(GameManager.instance.currentPlayer.GlobalPosition) < dis)
        {
            _bomberEnemyScript.InRun = !_bomberEnemyScript.InRun;
        }
    }
}
