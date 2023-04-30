using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Player>().UpdateGoods(new int[] {0,0,0});
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
    }

    public void Embark()
    {
        EventManager.Instance.StartNextEncounter();
    }
}
