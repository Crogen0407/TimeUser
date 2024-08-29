using UnityEngine;

public class Clock : MonoBehaviour
{
	TimeManager _timeManager;
    [SerializeField] private Transform _minuteHandle;
    [SerializeField] private Transform _secondHandle;
    public bool IsUseTime { get; set; }

	private void Awake()
	{
		_timeManager = TimeManager.Instance;
	}

	private void Update()
	{
		if(IsUseTime)
		{
			if (_timeManager.CurrentTime <= 0) return;
			_minuteHandle.eulerAngles += Vector3.forward * 6 * Time.deltaTime * _timeManager.mulValue;
			_secondHandle.eulerAngles += Vector3.forward * 360 * Time.deltaTime * _timeManager.mulValue;
		}
	}
}