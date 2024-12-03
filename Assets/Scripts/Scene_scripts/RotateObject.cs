using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private float sf = 10f;
     
    private int rotationDirection;
     
    void Start()
    { 
        rotationDirection = Random.value > 0.5f ? 1 : -1;
    }
     
    void Update()
    { 
        transform.Rotate(0, rotationDirection * sf * Time.deltaTime, 0);
    }
}
