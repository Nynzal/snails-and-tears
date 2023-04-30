using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void StartGame()
    {
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}