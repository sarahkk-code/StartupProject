
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused = true;
    private CanvasGroup pauseCanvasGroup;

    void Start()
    {
        if (pausePanel != null)
        {
            pauseCanvasGroup = pausePanel.GetComponent<CanvasGroup>();

            if (pauseCanvasGroup == null)
                pauseCanvasGroup = pausePanel.AddComponent<CanvasGroup>();

            // Keep the pause panel visible but let clicks pass through it.
            pauseCanvasGroup.blocksRaycasts = false;
        }

        PauseGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
