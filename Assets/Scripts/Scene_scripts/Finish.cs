using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{


    private void OnTriggerStay(Collider other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
