using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ScreenFadeManager : MonoSingleton<ScreenFadeManager>
{
	private bool _isFadding;

	[SerializeField] private Material _screenFadeMat;
	[SerializeField] private float _fadeDuration= 3f;
	private int _screenFadeValueID;

	public float GetFadeDuration() => _fadeDuration;

	private void Awake()
	{
		_screenFadeValueID = Shader.PropertyToID("_Value");
		_screenFadeMat.SetFloat(_screenFadeValueID, 0);
	}

	[ContextMenu("FadeScreen")]
	public void FadeScreen(bool fadeScreenActive)
	{
		if (_isFadding) return;
		_isFadding = true;

		_screenFadeMat.SetFloat(_screenFadeValueID, (float)Convert.ToInt32(fadeScreenActive));
		float endValue = (float)Convert.ToInt32(!fadeScreenActive);

		DOTween.To(()=>_screenFadeMat.GetFloat(_screenFadeValueID),
			x=>_screenFadeMat.SetFloat(_screenFadeValueID, x),
			endValue, _fadeDuration).SetUpdate(true).OnComplete(()=> 
			{
				_isFadding = false;
			}).SetEase(Ease.InQuint);
	}
}
