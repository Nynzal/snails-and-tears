using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardHandDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _handCardPrefab;
    
    private List<HandCard> _displayedCardContainers;
    private Player _player;

    public void SetPlayerReference(Player player)
    {
        _player = player;
        _displayedCardContainers = new List<HandCard>();
    }

    public void DisplayCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            AddCard(card);
        }
    }

    public void AddCard(Card card)
    {
        GameObject go = Instantiate(_handCardPrefab, transform);
        HandCard handCard = go.GetComponent<HandCard>();
        _displayedCardContainers.Add(handCard);
        handCard.Initialize(card, this);
    }

    private void RemoveCard(HandCard handCard)
    {
        _displayedCardContainers.Remove(handCard);
        Destroy(handCard.gameObject);
    }

    public void CardSelected(Card card, HandCard handCard)
    {
        if (_player.TryPlayCard(card))
        {
            RemoveCard(handCard);
        }
    }
}
