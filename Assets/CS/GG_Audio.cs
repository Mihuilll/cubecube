using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_Audio : MonoBehaviour
{
    [SerializeField] GameObject PrefabAudio;
    [SerializeField] AudioClip audioClip;
    public int time, Movetime;

    void Update()
    {
        if(Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0)
        {
            if (time != 0) return;
            time = Movetime;
            GameObject _temp = Instantiate(PrefabAudio);
            _temp.GetComponent<AudioSource>().clip = audioClip;
            _temp.GetComponent<AudioSource>().Play();
            Destroy(_temp,5);
        }
    }
    private void FixedUpdate()
    {
        if (time > 0)
        {
            time--;
        }
    }
}
