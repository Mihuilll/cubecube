using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatLevel2 : MonoBehaviour
{
    public float Speed = 1f;
    public float maxx = 8f;
    public float minz = 0f;

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
        pos.x += Time.deltaTime * Speed;
        transform.position = pos;
        if (pos.x < maxx)
        {
            Speed = Mathf.Abs(Speed);
        }
        else if (pos.x > minz)
        {
            Speed = -Mathf.Abs(Speed);
        }
    }
}
