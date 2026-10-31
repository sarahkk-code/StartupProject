
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Hazard : MonoBehaviour
{
    private bool playerHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (playerHit)
            return;

        if (other.CompareTag("Player"))
        {
            playerHit = true;
            StartCoroutine(LoadLoseScene());
        }
    }

    private IEnumerator LoadLoseScene()
    {
        Debug.Log("GAME OVER!");

        yield return new WaitForSeconds(2f);

        Time.timeScale = 1f;
        SceneManager.LoadScene("loseScene");
    }
}
