using UnityEngine;

public class Background : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip music;                   // Музыка для текущей сцены
    public AudioClip sounds;                  // Фоновые звуки для текущей сцены

    private void Start()
    {
        // Играем фоновую музыку и звуки в начале
        AudioManager.instance.PlayBackgroundMusic(music);
        AudioManager.instance.PlayAmbientEffect(sounds);
    }
}
