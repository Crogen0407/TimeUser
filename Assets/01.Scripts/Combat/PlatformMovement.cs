using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
	[SerializeField] private Transform _targetTrm;
	[SerializeField] private LineRenderer _lineRenderer;

    [Header("Follow Axis")]
    [SerializeField] private bool _FollowAxisX;
    [SerializeField] private bool _FollowAxisY;

	private void FixedUpdate()
	{
		if (_FollowAxisX && _FollowAxisY) return;

		_lineRenderer.SetPosition(0, Vector3.zero);
		_lineRenderer.SetPosition(1, _targetTrm.position - transform.position);

		if (_FollowAxisX)
		{
			transform.position = new Vector2(_targetTrm.position.x, transform.position.y); 
		}
		else if (_FollowAxisY)
		{
			transform.position = new Vector2(transform.position.x, _targetTrm.position.y);
		}
	}
}
