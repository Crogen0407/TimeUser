using Crogen.AgentFSM;
using Crogen.PowerfulInput;
using System;
using UnityEngine;

class PlayerWalkState : AgentState
{
	private InputReader _inputReader;
	private Player _playerBase;

	public PlayerWalkState(Agent agentBase, StateMachine stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
	{
		_inputReader = GameManager.Instance.InputReader;
		_playerBase = agentBase as Player;
	}

	public override void Enter()
	{
		base.Enter();
		_inputReader.MoveEvent += OnMoveHandler;
		_inputReader.JumpEvent += OnJumpHandle;
	}

	public override void Exit()
	{
		_inputReader.MoveEvent -= OnMoveHandler;
		_inputReader.JumpEvent -= OnJumpHandle;
		base.Exit();
	}

	public override void UpdateState()
	{
		base.UpdateState();
	}

	private void OnMoveHandler(float dir)
	{
		_playerBase.Movement.SetMovement(new Vector3(dir, 0, 0));
		if (Mathf.Approximately(dir, 0))
			_stateMachine.ChangeState(AgentStateEnum.Idle);
	}

	private void OnJumpHandle()
	{
		_stateMachine.ChangeState(AgentStateEnum.Jump);
	}
}