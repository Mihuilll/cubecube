using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMotion1 : MonoBehaviour
{
    
    public GameObject stair;


    private void OnTriggerStay(Collider other)
    {
        stair.SetActive(true);
    
    }
}
