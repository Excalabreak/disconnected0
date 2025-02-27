using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/15/2025]
 * [component for screen warping]
 */

public partial class ScreenWarp : Node
{
    [Export] private float _screenWarpBuffer = 50f;

    /// <summary>
    /// checks if asteroid is offscreen and warps them to the correct location
    /// </summary>
    public void CheckScreenWarp(Node2D entity, Vector2 velocity)
    {
        Vector2 resolution = GetViewport().GetVisibleRect().Size;

        if (entity.Position.X < -_screenWarpBuffer && velocity.X < 0)
        {
            entity.Position = new Vector2(resolution.X + _screenWarpBuffer, entity.Position.Y);
        }
        if (entity.Position.Y < -_screenWarpBuffer && velocity.Y < 0)
        {
            entity.Position = new Vector2(entity.Position.X, resolution.Y + _screenWarpBuffer);
        }
        if (entity.Position.X > resolution.X + _screenWarpBuffer && velocity.X > 0)
        {
            entity.Position = new Vector2(-_screenWarpBuffer, entity.Position.Y);
        }
        if (entity.Position.Y > resolution.Y + _screenWarpBuffer && velocity.Y > 0)
        {
            entity.Position = new Vector2(entity.Position.X, -_screenWarpBuffer);
        }
    }
}
