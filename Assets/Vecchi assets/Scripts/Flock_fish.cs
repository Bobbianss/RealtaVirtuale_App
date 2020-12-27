using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock_fish : MonoBehaviour
{
    public GameObject fish;
    public List<GameObject> flock;
    public int NumOfFish;
    public float flockWidth;
    public float speed;

    void Start()
    {
        for (int i = 0; i < NumOfFish; i++)
        {
            flock.Add(Instantiate(fish,this.transform, false));
            flock[i].transform.position = flock[i].transform.position + flockWidth * 0.5f * new Vector3(
                Mathf.PerlinNoise(i * 0.21f+0.1286f,67f/96f) - 0.5f,
                Mathf.PerlinNoise(i * 0.5f + 0.86f, 0f) - 0.5f,
                Mathf.PerlinNoise(0f, i * 0.23f + 0.4286f) - 0.5f);
        }
    }
}
