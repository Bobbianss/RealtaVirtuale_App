using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCollisionTriggerer : MonoBehaviour
{
	public PickUpObject pickUpScript;

	private void OnTriggerEnter(Collider maybeJunk)
	{
		pickUpScript.OnTriggererCollisionEnter(maybeJunk);
		Debug.Log("Il becco ha avuto la collisione");
	}
}
