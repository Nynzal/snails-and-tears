using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EncounterLeftDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Stations left until reaching destination: " 
                    + FindObjectOfType<EncounterManager>().GetEncountersLeft();
    }

}
