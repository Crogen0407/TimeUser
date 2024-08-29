using Crogen.CrogenPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolingObject
{
	[SerializeField] private float _speed = 3.5f;
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public Vector2 Velocity;

	private Rigidbody2D _rigidbody2D;

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void OnPop()
	{
	}

	public void OnPush()
	{
	}

	private void FixedUpdate()
	{
		_rigidbody2D.velocity = Velocity * _speed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		this.Push();
	}
}
