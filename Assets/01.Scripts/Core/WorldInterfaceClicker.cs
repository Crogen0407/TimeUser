using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInterfaceClicker : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsWorldInterface;

	private void FixedUpdate()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, _whatIsWorldInterface);
		if (hit.collider != null && Input.GetMouseButtonDown(0))
		{
			if(hit.transform.TryGetComponent(out WorldInterface worldInterface))
			{
				worldInterface.InteractionEvent?.Invoke();
			}
		}
	}
}
