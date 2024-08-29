using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ClockContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _fillImage;

    public void SetTimer(float currentTime, float maxTime)
	{
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);

        _timeText.text = timeSpan.ToString(@"mm\:ss");
        _fillImage.fillAmount = currentTime / maxTime;
    }
}
