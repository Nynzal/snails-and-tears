using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Inventory
    [SerializeField] private CardDeck _deck;
    [SerializeField] private int[] _goods;
    [SerializeField] private int _maxHandSize;

    // Objective
    private Goods.Order _finalOrder;
    [SerializeField] private int[] _orderRange;
    
    
    // Encounter Objects
    private Encounter _encounter;
    private CardHandDisplay _cardHandDisplay;
    // Encounter State
    private bool _hasEncounterEnded;


    // Start is called before the first frame update
    void Start()
    {
        GenerateFinalOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ----------------- CARDS
    public CardDeck GetCardDeck()
    {
        return _deck;
    }

    public void BuyCard(Card card, Goods.Type costType, int prize)
    {
        UpdateGoods(costType, -prize);
        _deck.AddCardToDeck(card);
    }
    
    
    // -------------------- GOODS
    public bool HasEnoughGoodsOf(Goods.Type type, int amount)
    {
        if (_goods[(int)type] >= amount)
        {
            return true;
        }

        return false;
    }
    
    public void UpdateGoods(int[] change)
    {
        for (int i = 0; i < change.Length; i++)
        {
            _goods[i] += change[i];
        }
        
        EventManager.Instance.OnResourceChange(_goods);
    }

    public void UpdateGoods(Goods.Type type, int change)
    {
        _goods[(int)type] += change;
        EventManager.Instance.OnResourceChange(_goods);
    }
    
    

    // ------------------ FINAL ORDER
    private void GenerateFinalOrder()
    {
        int r = Random.Range(_orderRange[0], _orderRange[1]+1);
        int t = Random.Range(0, 3);
        _finalOrder = new Goods.Order(r, (Goods.Type)t);
    }

    public Goods.Order GetFinalOrder()
    {
        return _finalOrder;
    }

    public bool HasEnoughForFinalOrder()
    {
        return HasEnoughGoodsOf(_finalOrder._type, _finalOrder._amount);
    }
    
    
    // ---------------- PLAYING AT ENCOUNTER
    public void InitializeHand(CardHandDisplay handArea, Encounter encounter)
    {
        _encounter = encounter;
        _cardHandDisplay = handArea;
        _hasEncounterEnded = false;
        
        _cardHandDisplay.SetPlayerReference(this);
        _cardHandDisplay.DisplayCards(_deck.DrawOpeningHand(4));
        
        EventManager.Instance.OnResourceChange(_goods);
    }

    public void SetEncounterOverFlag()
    {
        _hasEncounterEnded = true;
    }

    public void RoundEnd()
    {
        DrawCards(1);
    }

    public void DrawCards(int amount)
    {
        while (_deck.GetCurrentHandSize() < _maxHandSize && amount > 0 )
        {
            Card card = _deck.DrawCard();
            if (card != null)
            {
                _cardHandDisplay.AddCard(card);
            }
            
            amount--;
        }
    }

    public bool TryPlayCard(Card card)
    {
        if (_hasEncounterEnded)
        {
            return false;
        }

        if (!CanPayTheGoodsForCard(card))
        {
            return false;
        }

        if (!_encounter.CanPlayCard(card))
        {
            return false;
        }

        PayCardResources(card);
        _encounter.PlayCard(card);
        _deck.DiscardCard(card);
        
        return true;
    }

    private bool CanPayTheGoodsForCard(Card card)
    {
        for (int i = 0; i < card.effects.Length; i++)
        {
            switch (card.effects[i])
            {
                case Card.Effect.COST_G0:
                    if (!HasEnoughGoodsOf((Goods.Type)0, card.effectValues[i]))
                    {
                        return false;
                    }
                    break;
                case Card.Effect.COST_G1:
                    if (!HasEnoughGoodsOf((Goods.Type)1, card.effectValues[i]))
                    {
                        return false;
                    }
                    break;
                case Card.Effect.COST_G2:
                    if (!HasEnoughGoodsOf((Goods.Type)2, card.effectValues[i]))
                    {
                        return false;
                    }
                    break;
            }
        }

        return true;
    }

    private void PayCardResources(Card card)
    {
        for (int i = 0; i < card.effects.Length; i++)
        {
            switch (card.effects[i])
            {
                case Card.Effect.COST_G0:
                    UpdateGoods((Goods.Type)0, -card.effectValues[i]);
                    break;
                case Card.Effect.COST_G1:
                    UpdateGoods((Goods.Type)1, -card.effectValues[i]);
                    break;
                case Card.Effect.COST_G2:
                    UpdateGoods((Goods.Type)2, -card.effectValues[i]);
                    break;
            }
        }
    }
}
