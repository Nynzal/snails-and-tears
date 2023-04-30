using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _orderText;
    [SerializeField] private Image _orderImage;
    
    // Start is called before the first frame update
    void Start()
    {
        Goods.Order order = FindObjectOfType<Player>().GetFinalOrder();

        _orderText.text = "" + order._amount + " " + Goods.Name[(int)order._type];

    }
}
