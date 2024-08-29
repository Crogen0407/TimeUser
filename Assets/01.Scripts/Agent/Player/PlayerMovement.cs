using Crogen.AgentFSM;
using Crogen.AgentFSM.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour, IMovement
{
	[Header("Events")]
	[SerializeField] private UnityEvent _jumpEvent;
	[SerializeField] private UnityEvent _forceJumpEvent;
	[SerializeField] private UnityEvent _walkEvent;
	[SerializeField] private UnityEvent _damagedEvent;

	private Rigidbody2D _rigidbody2D;
	public float moveSpeed = 5f;
	public float jumpPower = 10f;
	public Vector3 Velocity { get; set; }
	public Agent AgentBase { get; set; }

	private Transform _visualTrm;
	[SerializeField] private int _maxJumpCount = 2;
	private int _jumpCount=0;

	[SerializeField] private LayerMask _whatIsGround;

	[Header("JumpBoxCast")]
	[SerializeField] private Vector2 _offset;
	[SerializeField] private Vector2 _size = new Vector2(0.3f, 0.3f);

	public bool isDie = false;

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_visualTrm = transform.Find("Visual");
	}

	public void Initialize(Agent agent)
	{
		AgentBase = agent;
	}

	public void SetMovement(Vector3 movement, bool isRotation = true)
	{
		if (isDie) return;
		Velocity = movement * moveSpeed;
		if(isRotation)
		{
			_visualTrm.rotation = Quaternion.Euler(0, movement.x > 0 ? 1f : -1f, 0);
		}
	}

	public void StopImmediately()
	{
		Velocity = Vector3.zero;
	}

	public void GetKnockback(Vector3 force)
	{
		StopImmediately();
		Velocity = force;
		_rigidbody2D.velocity = Vector3.zero;
	}

	private void FixedUpdate()
	{
		if (Physics2D.BoxCast((Vector2)transform.position + _offset * .3f,
			_size, 0f, Vector2.one, 0f, _whatIsGround))
		{
			_jumpCount = 0;
		}

		Velocity = new Vector3(Velocity.x, 0, 0);
		_rigidbody2D.AddForce(Velocity * moveSpeed, ForceMode2D.Impulse);
		_rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -moveSpeed, moveSpeed), _rigidbody2D.velocity.y);
	}

	public void OnJump()
	{
		if (_jumpCount >= _maxJumpCount) return;
		if (_jumpCount == 1) StopImmediately();
		++_jumpCount;
		Vector3 jumpDir = new Vector3(0, jumpPower, 0);
		_rigidbody2D.AddForce(jumpDir, ForceMode2D.Impulse);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Platform"))
			transform.parent = collision.transform;
		else
			transform.parent = null;

		if (collision.transform.CompareTag("Sprike"))
		{
			_damagedEvent?.Invoke();
			isDie = true;
			StageManager.Instance.RestartCurrentStage();
		}

		if(collision.transform.CompareTag("Trampoline"))
		{
			_rigidbody2D.AddForce(Vector2.up * 50f, ForceMode2D.Impulse);
			_jumpCount = 0;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		transform.parent = null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position + (Vector3)_offset, _size);
		Gizmos.color = Color.white;
	}
}