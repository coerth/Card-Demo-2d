using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour
{

    public Card card;
    public Image illustration;
    public TextMeshProUGUI cardName, health, manaCost, damage;

    private void Awake()
    {
        Initialize(card);
    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void Initialize(Card card)
    {
        illustration.sprite = card.illustration;
        cardName.text = card.cardName;
        manaCost.text = card.manaCost.ToString();
        damage.text = card.damage.ToString();
        health.text = card.health.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
