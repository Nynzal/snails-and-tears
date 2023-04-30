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
    }

    private void OnDisable()
    {
        EventManager.Instance.NextEncounter -= OnEmbarkToNextEncounter;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Random.Range(_patienceRange[0], _patienceRange[1]));
    }

    private void ProceedToFinalOrderDelivery()
    {
        
    }
}
