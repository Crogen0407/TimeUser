using Crogen.PowerfulInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	[field: SerializeField] public InputReader InputReader { get; private set; }
	public Player Player { get; private set; }

	private void Awake()
	{
		Player = FindObjectOfType<Player>();
	}
}
