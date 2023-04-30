using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods : MonoBehaviour
{
    public enum Type
    {
        TEARS,
        SNAILS,
        FLUBBER
    }

    public static string[] Name =
    {
        "Tears",
        "Snails",
        "Flubberhair"
    };

    public struct Order
    {
        public Order(int amount, Type type)
        {
            _amount = amount;
            _type = type;
        }
        
        public readonly int _amount;
        public readonly Goods.Type _type;
    }
}
