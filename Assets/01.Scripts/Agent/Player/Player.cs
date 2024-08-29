using UnityEngine;
using Crogen.AgentFSM;

public class Player : Agent
{
    private Rigidbody2D _rigidbody2D;

	private void Awake()
	{
		Initialize<AgentStateEnum>();
	}
}