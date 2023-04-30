using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro[] _names;
    [SerializeField] private TMP_Text[] _amounts;

    private void OnEnable()
    {
        EventManager.Instance.ResourceChange += OnResourceChange;
    }

    private void OnDisable()
    {
        EventManager.Instance.ResourceChange -= OnResourceChange;
    }

    private void OnResourceChange(int[] resources)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            _amounts[i].text = "" + resources[i] + " " + Goods.Name[i];
        }
    }
}
