using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
	public GameObject objectHolder;
	GameObject thing;
	public Animator animator;
	public float pickUpRecoveryTime;
	public bool pickUpInput;
	private bool canPickUp = true;


	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()  //prende gli input
    {
		pickUpInput = Input.GetKeyDown(KeyCode.E);

		animator.SetBool("Input raccolta", false);


		if (pickUpInput)
		{
			if(thing == null)
			{
				if (canPickUp)
				{
					animator.SetBool("Input raccolta",true);
					TurnOffColliderAndPickupForSeconds(pickUpRecoveryTime);
				}
				
				Debug.Log("prendo");
			}
			else
			{
				DropThing();
				Debug.Log("Lascio");
			}
		}
    }

	public void OnTriggererCollisionEnter(Collider maybeJunk) //quando il becco tocca l'oggetto, chiama la funzione pickup e salva l'oggetto
	{
		if (maybeJunk.gameObject.tag == "Oggetto da raccogliere" && canPickUp)
		{
			thing = maybeJunk.gameObject;
			PickUpSuccess();
		}

		MaybeDropTheJunk(maybeJunk);
	}

	private void OnTriggerEnter(Collider collider) //quando il gabbiano si avvicina al cestino, mette l'oggetto nel cestino
	{
		MaybeDropTheJunk(collider);
	}
	
	void PickUpSuccess() //l'oggetto viene attaccato al becco, vengono "spenti" rb e collider, viene interrotta l'animazione di raccolta,
		          //viene detto all'anim che il gabbiano ha un oggetto in bocca
	{
		thing.transform.SetParent(objectHolder.transform, true);
		thing.GetComponent<Collider>().enabled = false;
		thing.GetComponent<Rigidbody>().isKinematic = true;
		animator.SetTrigger("Interruzione animazione raccolta");
		animator.SetBool("Oggetto in bocca", true);
	}

	void DropThing() //L'oggetto viene staccato, vengono riattivati i suoi collider e rb 
	{
		thing.transform.SetParent(null, true);
		thing.GetComponent<Collider>().enabled = true;
		thing.GetComponent<Rigidbody>().isKinematic = false;
		thing = null;
		animator.SetBool("Oggetto in bocca", false);
		animator.SetTrigger("Lascia oggetto");
		StartCoroutine(TurnOffColliderAndPickupForSeconds(pickUpRecoveryTime));
	}
	
	private void MaybeDropTheJunk(Collider maybeBinArea)
	{
		Debug.Log("check 1");

		if (thing != null && maybeBinArea.tag == "Cestino")
		{
			Debug.Log("check 2");
			if (thing.GetComponent<JunkScript>().junkTag == maybeBinArea.gameObject.GetComponent<JunkScript>().junkTag)
			{
				Debug.Log("check 3");
				DunkTheJunk();
			}
		}
		return;
	}

	IEnumerator TurnOffColliderAndPickupForSeconds(float time)
	{
		objectHolder.GetComponent<Collider>().enabled = false;
		canPickUp = false;
		yield return new WaitForSeconds(time);
		objectHolder.GetComponent<Collider>().enabled = true;
		canPickUp = true;
	}

	void DunkTheJunk()
	{
		thing.transform.SetParent(null, true);
		thing.GetComponent<Collider>().enabled = true;
		thing.GetComponent<Rigidbody>().isKinematic = false;
		thing.GetComponent<JunkFlyingToTheBin>().throwJunk = true;
		thing = null;
		animator.SetBool("Oggetto in bocca", false);
		animator.SetTrigger("Lascia oggetto");

		StartCoroutine(TurnOffColliderAndPickupForSeconds(pickUpRecoveryTime));
	}
}
