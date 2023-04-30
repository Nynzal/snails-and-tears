using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    [SerializeField] private int _encountersUntilEnd;

    [SerializeField] private int[] _tollCostRange;
    [SerializeField] private int[] _patienceRange;

    private void OnEnable()
    {
        EventManager.Instance.NextEncounter += OnEmbarkToNextEncounter;
        EventManager.Instance.ProceedToShop += OnEncounterCompleted;
        EventManager.Instance.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.Instance.NextEncounter -= OnEmbarkToNextEncounter;
        EventManager.Instance.ProceedToShop -= OnEncounterCompleted;
        EventManager.Instance.GameOver -= OnGameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetEncountersLeft()
    {
        return _encountersUntilEnd;
    }

    private void OnEmbarkToNextEncounter()
    {
        if (_encountersUntilEnd == 0)
        {
            ProceedToFinalOrderDelivery();
        }

        _encountersUntilEnd--;

        StartCoroutine(PrepareNextEncounter());
    }

    private IEnumerator PrepareNextEncounter()
    {
        // Unload active Scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
        // Load Scene Encounter
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);

        while (!sceneLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(3));
        
        // Prepare values
        FindObjectOfType<Encounter>().InitializeEncounter(
            Random.Range(_tollCostRange[0], _tollCostRange[1]),
            (Goods.Type)Random.Range(0,3),
            Random.Range(_patienceRange[0], _patienceRange[1]));
    }

    private void OnEncounterCompleted()
    {
        SceneManager.UnloadSceneAsync("Encounter");
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
    }

    private void ProceedToFinalOrderDelivery()
    {
        SceneManager.UnloadSceneAsync("Shop");
        SceneManager.LoadScene("FinalEncounter", LoadSceneMode.Additive);
    }

    private void OnGameOver()
    {
        SceneManager.UnloadSceneAsync("Encounter");
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }
}
