using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/03/2025]
 * [abstract class for enemy attacks]
 */

public abstract partial class BaseEnemyAttack : Node
{
    /// <summary>
    /// class for enemy attack patterns
    /// </summary>
    public abstract void Attack();
}
