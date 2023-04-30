using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Inventory
    [SerializeField] private CardDeck _deck;
    [SerializeField] private int[] _goods;

    // Objective
    private Goods.Order _finalOrder;
    [SerializeField] private int[] _orderRange;
    
    
    // Encounter
    private Encounter _encounter;
    private CardHandDisplay _cardHandDisplay;


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
    
    
    // ---------------- PLAYING AT ENCOUNTER
    public void InitializeHand(CardHandDisplay handArea, Encounter encounter)
    {
        _encounter = encounter;
        _cardHandDisplay = handArea;
        
        _cardHandDisplay.SetPlayerReference(this);
        _cardHandDisplay.DisplayCards(_deck.DrawOpeningHand(4));
        
        EventManager.Instance.OnResourceChange(_goods);
    }

    public bool TryPlayCard(Card card)
    {
        return true;
    }
}
