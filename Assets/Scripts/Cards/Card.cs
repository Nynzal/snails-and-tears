using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public enum Effect
    {
        COST_G0,
        COST_G1,
        COST_G2,
        TOLL_REDUCTION_FLAT,
        
    }
    
    public int _cost;
    public string _name;

    public string _description;

    public Effect[] effects;

    public int[] effectValues;

    public int prize;
}
