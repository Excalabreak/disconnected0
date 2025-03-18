using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/04/2025]
 * [Despawn after leaving screen]
 */

public partial class ScreenDespawn : Node2D
{
    [Export] private float _screenBuffer = 50f;

    /// <summary>
    /// checks if asteroid is offscreen and QueueFree's it
    /// </summary>
    public void CheckScreenDespawn(Node2D entity)
    {
        Vector2 resolution = GetViewport().GetVisibleRect().Size;

        if (entity.Position.X < -_screenBuffer ||
            entity.Position.Y < -_screenBuffer ||
            entity.Position.X > resolution.X + _screenBuffer ||
            entity.Position.Y > resolution.Y + _screenBuffer)
        {
            entity.QueueFree();
        }
    }
}
