using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour
{
    public GameObject map;
    [HideInInspector]
    public bool isOnMap;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOnMap = !isOnMap;
            if (isOnMap)
            {
                map.SetActive(true);
            }
            else
            {
                map.SetActive(false);
            }
        }
    }
}
