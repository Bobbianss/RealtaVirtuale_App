using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkFlyingToTheBin : MonoBehaviour
{
	public Transform binTransform;
	public float throwSpeed = 1f;
	public float throwHeight = 3f;
	public bool throwJunk = false;
	private Vector3 junkPosition;
	private float normalizingDistance;


	void Update()
    {
		if (!throwJunk)
		{
			junkPosition = this.gameObject.transform.position;
			Vector3 relativePos = junkPosition - binTransform.position;
			normalizingDistance = Vector3.Magnitude(Vector3.Scale(relativePos, new Vector3(1f, 0f, 1f)));
		}

		if (throwJunk)
		{
			this.gameObject.transform.position += Vector3.Normalize(-this.gameObject.transform.position + binTransform.position) * throwSpeed * Time.deltaTime;
			
			float horizDistance = Vector3.Magnitude(Vector3.Scale(this.gameObject.transform.position - binTransform.position, new Vector3(1f, 0f, 1f))) * Time.deltaTime;
			float parabulaVar = horizDistance / normalizingDistance; //0 quando parte, 1 quando arriva
			float parabula = ((4f * parabulaVar) - (4f * Mathf.Pow(parabulaVar,2f))) * throwHeight ; 
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y+parabula, this.gameObject.transform.position.z);
			if (parabulaVar <= 0.001f)
				Destroy(this.gameObject);
		}
		
    }
}
