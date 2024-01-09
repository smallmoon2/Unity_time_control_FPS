using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Play", LoadSceneMode.Additive);
    }
    public void QuitGame()
    {
        Debug.Log("Quit!!");
        Application.Quit();
    }
}
