using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/08/2025]
 * [script for bomber movement]
 */

public partial class BomberMovement : BaseEnemyMovement
{
    [Export] private BomberEnemyScript _bomberEnemyScript;

    [Export] private float _runSpeed = 120f;

    [Export] private float _deploySpeed = 60f;

    /// <summary>
    /// bomber constantly runs away from player
    /// </summary>
    /// <param name="delta"></param>
    public override void Move(float delta)
    {
        if (_bomberEnemyScript.InRun)
        {
            //runs away
        }
        else
        {
            //deploys and wanders
        }
    }
}
