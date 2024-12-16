using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : SoundPolomorf
{
    [Header("Sliders for Volume Settings")]
    public Slider musicSlider;    // Слайдер для фоновой музыки
    public Slider otherSlider;    // Слайдер для остальных звуков

    private void Start()
    {
        // Загружаем настройки громкости
        LoadVolumeSettings();

        // Привязываем изменения слайдеров к методам
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        otherSlider.onValueChanged.AddListener(SetOtherVolumes);
    }

    private void LoadVolumeSettings()
    {
        // Загружаем громкость из PlayerPrefs или задаем стандартные значения
        float defaultVolume = 0.5f;
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", defaultVolume);
        otherSlider.value = PlayerPrefs.GetFloat("OtherVolume", defaultVolume);

        // Применяем громкость
        ApplyVolumeSettings();
    }

    private void ApplyVolumeSettings()
    {
        // Применяем громкость музыки
        AudioManager.instance.musicSource.volume = musicSlider.value;

        // Применяем громкость для остальных источников звука
        float otherVolume = otherSlider.value;
        AudioManager.instance.ambientSource.volume = otherVolume;
        AudioManager.instance.uiSource.volume = otherVolume;
        AudioManager.instance.eventSource.volume = otherVolume;
        AudioManager.instance.playerSource.volume = otherVolume;
    }

    // Методы для изменения громкости
    public void SetMusicVolume(float value)
    {
        AudioManager.instance.PlayUISound(clip);

        AudioManager.instance.musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetOtherVolumes(float value)
    {
        AudioManager.instance.PlayUISound(clip);

        AudioManager.instance.ambientSource.volume = value;
        AudioManager.instance.uiSource.volume = value;
        AudioManager.instance.eventSource.volume = value;
        AudioManager.instance.playerSource.volume = value;
        PlayerPrefs.SetFloat("OtherVolume", value);
    }

    private void OnApplicationQuit()
    {
        // Сохраняем настройки при выходе
        PlayerPrefs.Save();
    }
}
