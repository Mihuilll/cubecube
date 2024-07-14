using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform pos;
    private void OnTriggerStay(Collider other)
    {
        other.transform.position = pos.transform.position;
    }
}
