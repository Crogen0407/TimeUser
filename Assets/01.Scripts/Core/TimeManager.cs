using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoSingleton<TimeManager>
{
	private float _maxTime;
	public float totalUsedTime = 0;
	[SerializeField] private float _currentTime;
	[SerializeField] private ClockContainer clockContainer;
	private Clock[] _clocks;
	public float mulValue = 10f;

    public float CurrentTime 
	{ 
		get=>_currentTime; 
		set 
		{
			if (value <= 0) _currentTime = 0;
			clockContainer.SetTimer(_currentTime, _maxTime);
			_currentTime = value;
		} 
	}

	public void InitTime(float maxTime, Clock[] clocks)
	{
		_currentTime = _maxTime = maxTime;
		clockContainer.SetTimer(_currentTime, _maxTime);
		_clocks = clocks;
	}

	private void Update()
	{
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			CurrentTime -= Time.deltaTime * mulValue;
			totalUsedTime += Time.deltaTime * mulValue;
			for (int i = 0; i < _clocks.Length; ++i)
			{
				_clocks[i].IsUseTime = true;
			}
		}
		else
		{
			for (int i = 0; i < _clocks.Length; ++i)
			{
				_clocks[i].IsUseTime = false;
			}
		}
	}
}
