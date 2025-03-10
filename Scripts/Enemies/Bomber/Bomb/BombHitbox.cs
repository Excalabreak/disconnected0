using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/10/2025]
 * [script for deploying bomb]
 */

public partial class BombHitbox : HitboxComponent
{
    //im not making a new script for the same thing
    [Export] private MissileExplosion _missileExplosion;

    /// <summary>
    /// hitbox for bomb to damage whatever it hits
    /// </summary>
    /// <param name="area">other</param>
    private void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent)
        {
            //explosion
            _missileExplosion.CallDeferred("AddExplosion");
        }
    }
}
