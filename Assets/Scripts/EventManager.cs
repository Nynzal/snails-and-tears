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

    public event Action<int,Goods.Type> UpdateTollCost;
    public void OnUpdateTollCost(int cost, Goods.Type type)
    {
        UpdateTollCost?.Invoke(cost, type);
    }

    public event Action<int> UpdatePatience;
    public void OnUpdatePatience(int patience)
    {
        UpdatePatience?.Invoke(patience);
    }

    public event Action<int> UpdatePlayerActions;
    public void OnUpdatePlayerActions(int actions)
    {
        UpdatePlayerActions?.Invoke(actions);
    }

    public event Action ProceedToShop;
    public void OnProceedToShop()
    {
        ProceedToShop?.Invoke();
    }

    public event Action GameOver;
    public void OnGameOver()
    {
        GameOver?.Invoke();
    }
}
