
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    public TMP_Dropdown sceneDropdown;

    private string[] scenes =
    {
        "gameLevel",
        "loseScene",
        "winScene",
        "gameLevelAuto"
    };

    private void Start()
    {
        if (sceneDropdown == null)
        {
            Debug.LogError("Scene Dropdown is not assigned!");
            return;
        }

        // Make the dropdown show the scene currently being played.
        int currentIndex = System.Array.IndexOf(
            scenes,
            SceneManager.GetActiveScene().name
        );

        if (currentIndex >= 0)
        {
            sceneDropdown.SetValueWithoutNotify(currentIndex);
            sceneDropdown.RefreshShownValue();
        }
    }

    public void ChangeScene(int index)
    {
        if (index < 0 || index >= scenes.Length)
            return;

        Debug.Log("Loading scene: " + scenes[index]);

        Time.timeScale = 1f;
        SceneManager.LoadScene(scenes[index]);
    }
}
