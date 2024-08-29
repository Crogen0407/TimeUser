using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoSingleton<StageManager>
{
	//Managements
	[SerializeField] private int _startStageIndex;
	private ScreenFadeManager _screenFadeManager;
	[field:SerializeField] public StageDataSO StageDataSO { get; private set; }
	[SerializeField] private CinemachineConfiner2D _cinemachineConfiner2D;
	public GameObject currentStage;
	private int _currentStageIndex = 0;

	public int GetCurrentStageIndex() => _currentStageIndex;

	private void Awake()
	{
		_screenFadeManager = ScreenFadeManager.Instance;
	}

	private void Start()
	{
		//First Stage
		//_screenFadeManager.FadeScreen(true);
		InitStage(_startStageIndex);
	}

	public async void ChangeStage(int stageIndex)
	{
		if (StageDataSO.stageDataList.Count <= stageIndex)
		{
			//_screenFadeManager.FadeScreen(false);
			await Task.Delay(5000);
			SceneManager.LoadScene("EndingScene");
		}

		Time.timeScale = 0;
		//_screenFadeManager.FadeScreen(false);
		await Task.Delay(5000);
		InitStage(stageIndex);
		//_screenFadeManager.FadeScreen(true);
		Time.timeScale = 1;
		await Task.Delay(3500);
		(GameManager.Instance.Player.Movement as PlayerMovement).isDie = false;
	}

	public void RestartCurrentStage()
	{
		ChangeStage(_currentStageIndex);
	}

	public void NextStage()
	{
		ChangeStage(++_currentStageIndex);
	}

	public void InitStage(int stageIndex)
	{
		_currentStageIndex = stageIndex;
		StageData curStageData = StageDataSO.stageDataList[stageIndex];

		//Background
		Camera.main.backgroundColor = curStageData.backgroundColor;

		//Stage
		if(currentStage!=null)
			Destroy(currentStage);
		currentStage = Instantiate(curStageData.stagePrefab, Vector3.zero, Quaternion.identity);

		//Player Move SpawnPoint
		Vector3 spawnPoint = currentStage.transform.Find("SpawnPoint").position;
		GameManager.Instance.Player.transform.position = spawnPoint;

		//CameraConfiner
		PolygonCollider2D cameraConfinerCol = currentStage.transform.Find("CameraConfiner").GetComponent<PolygonCollider2D>();
		if (cameraConfinerCol == null) return;
		_cinemachineConfiner2D.m_BoundingShape2D = cameraConfinerCol;

		//Time
		TimeManager.Instance.InitTime(curStageData.time, currentStage.transform.GetComponentsInChildren<Clock>());
	}
}
