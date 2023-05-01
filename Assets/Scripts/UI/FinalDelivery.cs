using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDelivery : MonoBehaviour
{
    private Player _player;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.UpdateGoods(new int[]{0,0,0});
    }

    public void OnDeliverButton()
    {
        if (_player.HasEnoughForFinalOrder())
        {
            SceneManager.LoadScene("ThanksForPlaying");
        }
    }

    public void OnOhWellButton()
    {
        SceneManager.LoadScene("ThanksForPlaying");
    }
}
