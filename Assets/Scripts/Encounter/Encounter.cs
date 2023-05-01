using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    // Values
    private int _tollCost;
    private Goods.Type _tollType;
    private int _guardPatience;

    // Objects
    [SerializeField] private CardHandDisplay _cardHandDisplay;
    private Player _player;
    
    // Encounter Rounds
    private int _playerActionsLeft;
    

    public void InitializeEncounter(int tollCost, Goods.Type tollType, int patience)
    {
        _tollCost = tollCost;
        _tollType = tollType;
        _guardPatience = patience;
        _playerActionsLeft = 3;
        
        EventManager.Instance.OnUpdatePatience(patience);
        EventManager.Instance.OnUpdateTollCost(tollCost, tollType);

        _player = FindObjectOfType<Player>();
        _player.InitializeHand(_cardHandDisplay, this);
    }

    
    
    // ----------------- Playing Cards
    public bool CanPlayCard(Card card)
    {
        if (card._cost > _guardPatience)
        {
            return false;
        }

        /*for (int i = 0; i < card.effects.Length; i++)
        {
            switch (card.effects[i])
            {
                case Card.Effect.TOLL_REDUCTION_FLAT:
            }
        }*/
        
        return true;
    }

    public void PlayCard(Card card)
    {
        // Patience
        _guardPatience -= card._cost;
        EventManager.Instance.OnUpdatePatience(_guardPatience);
        
        // Card Effects
        for (int i = 0; i < card.effects.Length; i++)
        {
            switch (card.effects[i])
            {
                case Card.Effect.TOLL_REDUCTION_FLAT:
                    _tollCost -= card.effectValues[i];
                    if (_tollCost < 0)
                    {
                        _tollCost = 0;
                    }
                    EventManager.Instance.OnUpdateTollCost(_tollCost, _tollType);
                    break;
                case Card.Effect.NICE_WORDS:
                    _guardPatience += card.effectValues[i];
                    break;
            }
        }
        
        // Actions
        ConsumeAction();
        
        
        CheckForPatienceEnd();
    }

    public void DrawCardAction()
    {
        if (_guardPatience > 0)
        {
            _guardPatience--;
            
            _player.DrawCards(1);
            
            ConsumeAction();
            
            CheckForPatienceEnd();
        }
    }

    private void CheckForPatienceEnd()
    {
        EventManager.Instance.OnUpdatePatience(_guardPatience);
        if (_guardPatience <= 0)
        {
            _player.SetEncounterOverFlag();
            if (!_player.HasEnoughGoodsOf(_tollType, _tollCost))
            {
                // Game lost
                EventManager.Instance.OnGameOver();
            }
        }
    }

    private void ConsumeAction()
    {
        _playerActionsLeft--;
        if (_playerActionsLeft == 0)
        {
            GuardAction();
            _player.RoundEnd();
            _playerActionsLeft = 3;
        }
        EventManager.Instance.OnUpdatePlayerActions(_playerActionsLeft);
    }

    // -------------------- GUARD / AI stuff
    private void GuardAction()
    {
        // TODO guard actions
    }
    
    // -------------- Paying the Toll
    public void OnPayTollButton()
    {
        if (_player.HasEnoughGoodsOf(_tollType, _tollCost))
        {
            _player.UpdateGoods(_tollType, -_tollCost);
            
            // Encounter completed
            EventManager.Instance.OnProceedToShop();
        }
    }
}
