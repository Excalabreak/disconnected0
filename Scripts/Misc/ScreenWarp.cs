using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/03/2025]
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

    /// <summary>
    /// screen warp w/o velocity, but not reccomended
    /// </summary>
    /// <param name="entity"></param>
    public void CheckScreenWarp(Node2D entity)
    {
        Vector2 resolution = GetViewport().GetVisibleRect().Size;

        if (entity.Position.X < -_screenWarpBuffer)
        {
            entity.Position = new Vector2(resolution.X + (_screenWarpBuffer/ 2), entity.Position.Y);
        }
        if (entity.Position.Y < -_screenWarpBuffer)
        {
            entity.Position = new Vector2(entity.Position.X, resolution.Y + (_screenWarpBuffer / 2));
        }
        if (entity.Position.X > resolution.X + _screenWarpBuffer)
        {
            entity.Position = new Vector2((-_screenWarpBuffer / 2), entity.Position.Y);
        }
        if (entity.Position.Y > resolution.Y + _screenWarpBuffer)
        {
            entity.Position = new Vector2(entity.Position.X, (-_screenWarpBuffer / 2));
        }
    }
}
