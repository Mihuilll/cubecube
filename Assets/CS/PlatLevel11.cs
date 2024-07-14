using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatLevel11 : MonoBehaviour
{
    public float Speed = 1f;


    private void OnTriggerStay(Collider other)
    {
        other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }

    void Update()
    {

        Vector3 pos = transform.position;
        pos.z += Time.deltaTime * Speed;
        transform.position = pos;
        if (pos.z >= -19.08)
        {
            Speed = 0;
        }

    }
}
