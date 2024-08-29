using UnityEngine;
using Crogen.CrogenPooling;

public class Enemy : MonoBehaviour
{
	[SerializeField] private Transform _firePoint;
	[SerializeField] private PoolType _bulletPoolType;
	[SerializeField] private float _fireDelay = 5f;
	private float _curFireDelayTime = 0f;

	private void Update()
	{
		_curFireDelayTime += Time.deltaTime;
		if(_fireDelay < _curFireDelayTime)
		{
			_curFireDelayTime = 0;
			Fire();
		}
	}

	private void Fire()
	{
		Bullet bullet = gameObject.Pop(_bulletPoolType, _firePoint.position, Quaternion.identity) as Bullet;

		bullet.Velocity = transform.right;
	}
}
