using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour
{
    private Card _card;
    private CardHandDisplay _cardHandDisplay;
    [SerializeField] private CardFiller _cardFiller;


    public void Initialize(Card card, CardHandDisplay cardHandDisplay)
    {
        _card = card;
        _cardHandDisplay = cardHandDisplay;
        
        _cardFiller.InitializeCard(_card);
    }

    public void OnSelection()
    {
        _cardHandDisplay.CardSelected(_card, this);
    }
}
