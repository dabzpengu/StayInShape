using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MatchingCardManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI selectedCardUI;
    private CardLogic selectedCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectCard(CardLogic card)
    {
        if (selectedCard != null)
        {
            Debug.Log("Cannot select card as there is already a card selected");
        }
        else
        {
            selectedCard = card;
            selectedCardUI.text = selectedCard.plantName;
        }
    }

    public void unselect()
    {
        selectedCard = null;
        selectedCardUI.text = "";
    }
}
