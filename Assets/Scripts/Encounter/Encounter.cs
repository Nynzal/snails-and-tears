using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    // Values
    private float _tollCost;
    private float _guardPatience;

    // Objects
    [SerializeField] private CardHandDisplay _cardHandDisplay;
    private Player _player;

    public void InitializeEncounter(int tollCost, int patience)
    {
        _tollCost = tollCost;
        _guardPatience = patience;
        
        EventManager.Instance.OnUpdatePatience(patience);
        EventManager.Instance.OnUpdateTollCost(tollCost);

        _player = FindObjectOfType<Player>();
        _player.InitializeHand(_cardHandDisplay, this);
    }

    
    
    // ----------------- Playing Cards
    public bool CanPlayCard(Card card)
    {
        return true;
    }

    public void PlayCard(Card card)
    {
        
    }
}
