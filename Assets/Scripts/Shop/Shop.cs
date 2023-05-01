using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] private int[] _buyAmountRangeHigh;
    [SerializeField] private int[] _buyAmountRangeLow;
    [SerializeField] private int[] _sellAmountRange;
    
    [SerializeField] private Card[] _allPurchasableCards;
    
    [SerializeField] private ShopCardItem[] _shopCardItems;
    [SerializeField] private ShopGoodsItem[] _shopGoodsItems;
    private ShopObject[] _shopObjects;
    private GoodsTrade[] _goodsTrades;

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
        int l = _shopGoodsItems.Length;
        _goodsTrades = new GoodsTrade[l];
        int start = Random.Range(0, 3);
        for (int i = 0; i < l; i++)
        {
            _goodsTrades[i]._trades = Random.Range(1, 4);
            _goodsTrades[i]._buy = (Goods.Type)((start + i) % 3);
            _goodsTrades[i]._sell = (Goods.Type)(((start + i) + 3 + 1 -2*Random.Range(0,2))% 3);
            _goodsTrades[i]._sellAmount = Random.Range(_sellAmountRange[0], _sellAmountRange[1]);
            _goodsTrades[i]._buyAmount = Random.Range(_buyAmountRangeLow[i], _buyAmountRangeHigh[i]);
            
            _shopGoodsItems[i].Initialize(
                this, 
                i,
                "" + _goodsTrades[i]._buyAmount + " " + Goods.Name[(int)_goodsTrades[i]._buy],
                "" + _goodsTrades[i]._sellAmount + " " + Goods.Name[(int)_goodsTrades[i]._sell],
                _goodsTrades[i]._trades);
        }
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

    public bool OnGoodsBuyButton(int index)
    {
        if (!_player.HasEnoughGoodsOf(_goodsTrades[index]._sell, _goodsTrades[index]._sellAmount))
        {
            return false;
        }
        
        _player.UpdateGoods(_goodsTrades[index]._sell, -_goodsTrades[index]._sellAmount);
        _player.UpdateGoods(_goodsTrades[index]._buy, _goodsTrades[index]._buyAmount);
        
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

    private struct GoodsTrade
    {
        public int _trades;
        public Goods.Type _buy;
        public Goods.Type _sell;
        public int _buyAmount;
        public int _sellAmount;
    }
}
