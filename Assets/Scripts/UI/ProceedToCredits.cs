using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProceedToCredits : MonoBehaviour
{
    public void ProceedToCreditsButton()
    {
        SceneManager.LoadScene("ThanksForPlaying");
    }
}
