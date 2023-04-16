using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class CardController : MonoBehaviour, IPointerEnterHandler,  IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public Card card;
    public Image illustration, image;
    public TextMeshProUGUI cardName, health, manaCost, damage;
    private Transform originalParent;

    private void Awake()
    {
            image = GetComponent<Image>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void Initialize(Card card, int ownerID)
    {
        this.card = new Card(card)
        {
            ownerID = ownerID
        };
        illustration.sprite = card.illustration;
        cardName.text = card.cardName;
        manaCost.text = card.manaCost.ToString();
        damage.text = card.damage.ToString();
        health.text = card.health.ToString();
        originalParent = transform.parent;
        if (card.health == 0) health.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(originalParent.name == $"Player{card.ownerID + 1}PlayArea" || TurnManager.instance.currentPlayerTurn != card.ownerID)
        {

        }
        else
        {
        transform.SetParent(transform.root);
        image.raycastTarget = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (originalParent.name == $"Player{card.ownerID + 1}PlayArea" || TurnManager.instance.currentPlayerTurn != card.ownerID)
        {

        }
        else
        {
            image.raycastTarget = true;
            AnalysePointerUp(eventData);
        }
    }

    private void AnalysePointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.name == $"Player{card.ownerID + 1}PlayArea")
            if (PlayerManager.instance.FindPlayerByID(card.ownerID).mana >= card.manaCost)
             {
                PlayCard(eventData.pointerEnter.transform);

                PlayerManager.instance.SpendMana(card.ownerID, card.manaCost);
            }
            else
            {
                ReturnToHand();
            }
       else
        {
            ReturnToHand();
        }
      
    }
   

    private void PlayCard(Transform playArea)
    {
        transform.SetParent(playArea);
        originalParent = playArea;
        transform.localPosition = Vector3.zero;
        CardManager.instance.PlayCard(this, card.ownerID);

    }

    public void Damage(int amount)
    {
        card.health -= amount;
        health.text = card.health.ToString();
    }

    private void ReturnToHand()
    {
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent == originalParent) return;

        transform.position = eventData.position;
    }
}
