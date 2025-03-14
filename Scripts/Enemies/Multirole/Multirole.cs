using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/12/2025]
 * [base script for multirole]
 */

public partial class Multirole : BaseEnemyScript
{
    [Export] private BaseEnemyAttack _enemyAttack;

    /// <summary>
    /// added to process for multirole
    /// </summary>
    /// <param name="delta"></param>
    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_enemyAttack != null)
        {
            _enemyAttack.Attack();
        }
    }
}
