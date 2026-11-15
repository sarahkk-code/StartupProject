
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class SceneSwitcher : MonoBehaviour
{
    public TMP_Dropdown sceneDropdown;

    private string[] scenes =
    {
        "gameLevel",
        "gameLevelAuto",
        "loseScene",
        "winScene",
        "offmesh",
        "paths"
    };

    private void Start()
    {
        if (sceneDropdown == null)
        {
            Debug.LogError("Scene Dropdown is not assigned!");
            return;
        }

        sceneDropdown.ClearOptions();
        sceneDropdown.AddOptions(new List<string>(scenes));

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

        Time.timeScale = 1f;
        SceneManager.LoadScene(scenes[index]);
    }
}
