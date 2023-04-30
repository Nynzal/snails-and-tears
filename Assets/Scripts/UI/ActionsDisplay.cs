using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _actionsLeftText;

    private void OnEnable()
    {
        EventManager.Instance.UpdatePlayerActions += OnUpdateActions;
    }

    private void OnDisable()
    {
        EventManager.Instance.UpdatePlayerActions -= OnUpdateActions;
    }

    private void OnUpdateActions(int actionsLeft)
    {
        _actionsLeftText.text = "" + actionsLeft;
    }
}
