using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventManager();
            }

            return _instance;
        }
    }

    // ------------------ UI Display calls
    public event Action<int[]> ResourceChange;
    public void OnResourceChange(int[] resources)
    {
        ResourceChange?.Invoke(resources);
    }
    
    public event Action ShowDeckDisplay;
    public void OnShowingDeckDisplay()
    {
        ShowDeckDisplay?.Invoke();
    }
    
    
    // --------------- Game Progression / Encounters
    public event Action NextEncounter;
    public void StartNextEncounter()
    {
        NextEncounter?.Invoke();
    }

    public event Action<int> UpdateTollCost;
    public void OnUpdateTollCost(int cost)
    {
        UpdateTollCost?.Invoke(cost);
    }

    public event Action<int> UpdatePatience;
    public void OnUpdatePatience(int patience)
    {
        UpdatePatience?.Invoke(patience);
    }
}
