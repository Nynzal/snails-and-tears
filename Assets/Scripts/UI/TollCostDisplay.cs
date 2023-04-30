using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TollCostDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _tollCostNumber;
    [SerializeField] private Image _tollCostIcon;

    private void OnEnable()
    {
        EventManager.Instance.UpdateTollCost += OnTollCostUpdate;
    }

    private void OnDisable()
    {
        EventManager.Instance.UpdateTollCost -= OnTollCostUpdate;
    }

    private void OnTollCostUpdate(int tollCost, Goods.Type type)
    {
        _tollCostNumber.text = "" + tollCost + " " + Goods.Name[(int)type];
    }
}
