using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StageData
{
	public string name;
	public float time;
	public Color backgroundColor;
	public GameObject stagePrefab;
}

[CreateAssetMenu(menuName = "SO/StageData")]
public class StageDataSO : ScriptableObject
{
	public List<StageData> stageDataList;
}
