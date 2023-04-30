using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private List<Card> _allCards;

    private List<Card> _currentHand;
    private List<Card> _drawPile;
    private List<Card> _discardPile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Card> GetFullDeck()
    {
        return _allCards;
    }

    public int GetCurrentHandSize()
    {
        return _currentHand.Count;
    }

    public void AddCardToDeck(Card card)
    {
        _allCards.Add(card);
    }

    public void RemoveCardFromDeck(Card card)
    {
        _allCards.Remove(card);
    }

    public List<Card> DrawOpeningHand(int size)
    {
        _drawPile = new List<Card>(_allCards);
        _discardPile = new List<Card>();
        _currentHand = new List<Card>();
        
        for (int i = 0; i < size; i++)
        {
            DrawCard();
        }

        return _currentHand;
    }

    public Card DrawCard()
    {
        if (_drawPile.Count == 0)
        {
            _drawPile = _discardPile;
            _discardPile = new List<Card>();
            if (_drawPile.Count == 0)
            {
                return null;
            }
        }
        
        int c = Random.Range(0, _drawPile.Count);
        Card card = _drawPile[c];
        _currentHand.Add(card);
        _drawPile.RemoveAt(c);
        return card;
    }

    public void DiscardCard(Card card)
    {
        _discardPile.Add(card);
        _currentHand.Remove(card);
    }

    public void DiscardCard(int cardIndex)
    {
        _discardPile.Add(_currentHand[cardIndex]);
        _currentHand.RemoveAt(cardIndex);
    }

    public void BurnCard(Card card)
    {
        
    }

    public void BurnCard(int cardIndex)
    {
        
    }
}
