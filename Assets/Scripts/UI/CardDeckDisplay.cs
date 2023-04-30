using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckDisplay : MonoBehaviour
{
    // Display stuff
    [SerializeField] private GameObject _cardsContainer;
    [SerializeField] private GameObject _cardsDisplay;
    [SerializeField] private GameObject _uiContainer;

    private GameObject[] _displayedCards;
    
    // Cards stuff
    [SerializeField] private GameObject _cardPrefab;
    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EventManager.Instance.ShowDeckDisplay += ShowDeckDisplay;
    }

    private void OnDisable()
    {
        EventManager.Instance.ShowDeckDisplay -= ShowDeckDisplay;
    }

    public void HideDeckDisplay()
    {
        _cardsDisplay.SetActive(false);
        _uiContainer.SetActive(false);
        
        for (int i = 0; i < _displayedCards.Length; i++)
        {
            Destroy(_displayedCards[i]);
        }
    }

    public void ShowDeckDisplay()
    {
        List<Card> cards = _player.GetCardDeck().GetFullDeck();
        _displayedCards = new GameObject[cards.Count];
        
        int i = 0;
        foreach (Card c in cards)
        {
            _displayedCards[i] = PlaceCard(c);
            i++;
        }
        
        _cardsDisplay.SetActive(true);
        _uiContainer.SetActive(true);
    }

    private GameObject PlaceCard(Card card)
    {
        GameObject cardGO = Instantiate(_cardPrefab, _cardsContainer.transform);
        cardGO.GetComponent<CardFiller>().InitializeCard(card);
        return cardGO;
    }
}
