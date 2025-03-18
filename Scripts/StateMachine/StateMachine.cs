using Godot;
using System;
using System.Collections.Generic;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/05/2025]
 * [basic state machine]
 */

public partial class StateMachine : Node
{
	[Export] public NodePath initialState;

	private Dictionary<string, State> _states;
	private State _currentState;

	/// <summary>
	/// sets up stat machine
	/// </summary>
    public override void _Ready()
    {
        _states = new Dictionary<string, State>();
		foreach (Node node in GetChildren())
		{
			if (node is State s)
			{
				_states[node.Name] = s;
				s.sm = this;
				s.Ready();
				s.Exit();
			}
		}

		_currentState = GetNode<State>(initialState);
		_currentState.Enter();
    }

	/// <summary>
	/// transitions between states
	/// </summary>
	/// <param name="key">which state to change to</param>
	public void TransitionTo(string key)
	{
		if (!_states.ContainsKey(key) || _currentState == _states[key])
		{
			return;
		}

		_currentState.Exit();
		_currentState = _states[key];
		_currentState.Enter();
	}
}
