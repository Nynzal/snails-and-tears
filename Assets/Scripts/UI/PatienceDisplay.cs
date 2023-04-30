using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatienceDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _patienceNumber;
    
    private void OnEnable()
    {
        EventManager.Instance.UpdatePatience += OnPatienceUpdate;
    }

    private void OnDisable()
    {
        EventManager.Instance.UpdatePatience -= OnPatienceUpdate;
    }

    private void OnPatienceUpdate(int patience)
    {
        _patienceNumber.text = "" + patience;
    }
}
