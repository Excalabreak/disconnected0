using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/05/2025]
 * [base state for state machine]
 */

public partial class State : Node
{
	public StateMachine sm;

	public virtual void Enter() { }
	public virtual void Exit() { }
	public virtual void Ready() { }
}
