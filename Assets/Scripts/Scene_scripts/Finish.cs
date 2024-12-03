using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField]
    private int indexScene;
    private void OnTriggerStay(Collider other)
    {
        SceneManager.LoadScene(indexScene);
    }
}
