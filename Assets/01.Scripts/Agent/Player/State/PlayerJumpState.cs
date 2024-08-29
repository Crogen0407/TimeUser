using Crogen.AgentFSM;
using Crogen.PowerfulInput;

class PlayerJumpState : AgentState
{
	private InputReader _inputReader;
	private Player _playerBase;

	public PlayerJumpState(Agent agentBase, StateMachine stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
	{
		_inputReader = GameManager.Instance.InputReader;
		_playerBase = agentBase as Player;
	}

	public override void Enter()
	{
		base.Enter();
		_playerBase.Movement.OnJump();
		_stateMachine.ChangeState(AgentStateEnum.Idle);
	}
}