using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopGoodsItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _buyGoodsAmountText;
    [SerializeField] private TMP_Text _sellGoodsAmountText;
    [SerializeField] private TMP_Text _tradesAvailableText;
    
    private Shop _shop;
    private int _index;

    private int _tradesAvailable;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Shop shop, int index, string buyText, string sellText, int tradesAvailable)
    {
        _shop = shop;
        _index = index;
        _buyGoodsAmountText.text = buyText;
        _sellGoodsAmountText.text = sellText;
        _tradesAvailable = tradesAvailable;
        _tradesAvailableText.text = "" + _tradesAvailable + "x";
    }

    public void OnBuyButton()
    {
        if (!_shop.OnGoodsBuyButton(_index))
        {
            return;
        }

        _tradesAvailable--;
        _tradesAvailableText.text = "" + _tradesAvailable + "x";
        
        if (_tradesAvailable <= 0)
        {
            Destroy(gameObject);
        }
    }
}
