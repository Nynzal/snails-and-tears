using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenuDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            return;
        }

        SceneManager.LoadScene(0);
    }
}
