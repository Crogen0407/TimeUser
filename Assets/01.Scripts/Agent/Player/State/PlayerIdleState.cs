using Crogen.AgentFSM;
using Crogen.PowerfulInput;
using System;
using UnityEngine;

class PlayerIdleState : AgentState
{
	private InputReader _inputReader;
	private Player _playerBase;

	public PlayerIdleState(Agent agentBase, StateMachine stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
	{
		_inputReader = GameManager.Instance.InputReader;
		_playerBase = agentBase as Player;
	}

	public override void Enter()
	{
		base.Enter();
		_inputReader.MoveEvent += OnMoveStartHandle;
		_inputReader.JumpEvent += OnJumpHandle;
	}

	public override void Exit()
	{
		_inputReader.MoveEvent -= OnMoveStartHandle;
		_inputReader.JumpEvent -= OnJumpHandle;
		base.Exit();
	}

	private void OnMoveStartHandle(float dir)
	{
		_playerBase.Movement.SetMovement(new Vector3(dir, 0, 0));
		_stateMachine.ChangeState(AgentStateEnum.Walk);
	}

	private void OnJumpHandle()
	{
		_stateMachine.ChangeState(AgentStateEnum.Jump);
	}
}