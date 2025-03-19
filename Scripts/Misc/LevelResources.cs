using Godot;
using System;

[GlobalClass]
public partial class LevelResources : Resource
{
    [Export] public Planets _planet;
    [Export] public int[] _enemies;
}
