using UnityEngine;
using UnityEngine.UI;

public class ClearPlayerPrefs : MonoBehaviour
{
    public Button clearButton;  // Кнопка, на которую нужно нажать для удаления данных

    private void Start()
    {
        // Устанавливаем обработчик события на кнопку
        if (clearButton != null)
        {
            clearButton.onClick.AddListener(ClearAllPlayerPrefs);
        }
    }

    // Функция для удаления всех сохранённых данных
    private void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();  // Удаляет все сохранённые данные
        PlayerPrefs.Save();  // Сохраняет изменения
        Debug.Log("Все данные PlayerPrefs были удалены!");
    }
}
