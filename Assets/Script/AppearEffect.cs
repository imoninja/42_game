
using UnityEngine;
using System.Collections;

public class AppearEffect : MonoBehaviour
{

	[SerializeField]
	private GameObject effectObject;
	[SerializeField]
	private float deleteTime;
	[SerializeField]
	private float offset;

	void Start()
	{
		var instantiateEffect = GameObject.Instantiate(effectObject, transform.position + new Vector3(0f, offset, 0f), Quaternion.identity) as GameObject;
		Destroy(instantiateEffect, deleteTime);
	}
}
