using UnityEngine;
using UnityEngine.UI;
public class Option : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel; 
    [SerializeField] private GameObject option;
    public void P_Close_Options()
    {
        menuPanel.SetActive(true);
        option.SetActive(false);
    }
    // настройка графики 
    /*
    *
    */
    // настройка звука 
    // MUSIC
    [SerializeField] private Slider volumeSlider; 
    void Start()
    {
       float savedVolume = PlayerPrefs.GetFloat("Songs", 0.5f);

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetSong);
        }
        SetVolume(savedVolume); 
    }
    private void SetSong(float value)
    {
        SetVolume(value);
        PlayerPrefs.SetFloat("Songs", value);
    }
    private void SetVolume(float value)
    {
        foreach (var audioSource in Progress.Inst.audioMassive)
        {
            if (audioSource != null)
            {
                audioSource.volume = value;
            }
        }
    }
    // VFX
}
