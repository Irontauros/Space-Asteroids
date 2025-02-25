using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject title;

    private void Start()
    {
        title.SetActive(true);
        menuPanel.SetActive(false);

        StartCoroutine(ShowMenuAfterDelay());
    }

    private IEnumerator ShowMenuAfterDelay()
    {

        yield return new WaitForSeconds(6f);

        title.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
