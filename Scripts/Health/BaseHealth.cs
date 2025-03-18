using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/17/2025]
 * [base class of for all health scripts]
 */

public abstract partial class BaseHealth : Node
{
	public abstract void Damage(int damage);
}
