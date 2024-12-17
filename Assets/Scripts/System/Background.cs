using UnityEngine;

public class Background : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip music;                   // ������ ��� ������� �����
    public AudioClip sounds;                  // ������� ����� ��� ������� �����

    private void Start()
    {
        // ������ ������� ������ � ����� � ������
        AudioManager.instance.PlayBackgroundMusic(music);
        AudioManager.instance.PlayAmbientEffect(sounds);
    }
}
