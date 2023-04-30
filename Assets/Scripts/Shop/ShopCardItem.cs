using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCardItem : MonoBehaviour
{
    [SerializeField] private CardFiller _cardFiller;
    [SerializeField] private TMP_Text _costText;

    private Shop _shop;
    private int _index;

    public void OnBuyButton()
    {
        if (_shop.OnCardBuyButton(_index))
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(Shop shop, int index, Card card, string costText)
    {
        _shop = shop;
        _index = index;
        _cardFiller.InitializeCard(card);
        _costText.text = costText;
    }
}
