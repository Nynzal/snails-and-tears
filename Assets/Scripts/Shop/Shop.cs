using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] private Card[] _allPurchasableCards;
    
    [SerializeField] private ShopCardItem[] _shopCardItems;
    private ShopObject[] _shopObjects;

    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(4));
        
        _player = FindObjectOfType<Player>();
        _player.UpdateGoods(new [] {0,0,0});
        
        // Cards to buy
        _shopObjects = new ShopObject[_shopCardItems.Length];
        for (int i = 0; i < _shopCardItems.Length; i++)
        {
            _shopObjects[i] = new ShopObject();
            _shopObjects[i].card = _allPurchasableCards[Random.Range(0,_allPurchasableCards.Length)];
            _shopObjects[i].prize = _shopObjects[i].card.prize + Random.Range(-4, 5);
            _shopObjects[i].costType = (Goods.Type) Random.Range(0, 3);

            _shopCardItems[i].Initialize(
                this, 
                i, 
                _shopObjects[i].card, 
                "" + _shopObjects[i].prize + " " + Goods.Name[(int)_shopObjects[i].costType]);
        }
        
        // Goods to trade / buy
        
    }

    public bool OnCardBuyButton(int index)
    {
        if (!_player.HasEnoughGoodsOf(_shopObjects[index].costType, _shopObjects[index].prize))
        {
            return false;
        }
        
        _player.BuyCard(_shopObjects[index].card, _shopObjects[index].costType, _shopObjects[index].prize);
        
        return true;
    }

    public void Embark()
    {
        EventManager.Instance.StartNextEncounter();
    }

    private struct ShopObject
    {
        public Card card;
        public int prize;
        public Goods.Type costType;
    }
}
