using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardFiller : MonoBehaviour
{
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    
    public void InitializeCard(Card card)
    {
        _costText.text = "" + card._cost;
        _titleText.text = card._name;
        _descriptionText.text = card._description;
    }
}
