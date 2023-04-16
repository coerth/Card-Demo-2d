using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public List<Card> cards = new List<Card>();
    //public List<Card> player1Deck = new List<Card>(), player2Deck = new List<Card>();
    public List<int> player1Deck = new List<int>();
    public Transform player1Hand, player2Hand;
    public CardController cardControllerPrefab;
    public List<CardController> player1Cards = new List<CardController>(), player2Cards = new List<CardController>();


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenerateCards();
        //FillDeck();
    }

   /*private void FillDeck()
    {
        foreach (Card card in cards)
        {
            player1Deck.Add(new Card(card));
            player1Deck.Add(new Card(card));
        }

        foreach (Card card in cards)
        {
            player2Deck.Add(new Card(card));
            player2Deck.Add(new Card(card));
        }
    }
   */ 

    private void GenerateCards()
    {
        foreach (Card card in cards)
        {
            CardController newCard = Instantiate(cardControllerPrefab, player1Hand);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(card, 0);
        }
        foreach (Card card in cards)
        {
            CardController newCard = Instantiate(cardControllerPrefab, player2Hand);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(card, 1);
        }
    }

    public void PlayCard(CardController card, int ID)
    {
        if (ID == 0)
        {
            player1Cards.Add(card);
        }
        else
        {
            player2Cards.Add(card);
        }
    }

    public void ProcessStartTurn(int ID)
    {
        List<CardController> cards = new List<CardController>();
        List<CardController> enemyCards = new List<CardController>();



        if (ID == 0)
        {
            cards.AddRange(player1Cards);
            enemyCards.AddRange(player2Cards);


        }
        else
        {
            cards.AddRange(player2Cards);
            enemyCards.AddRange(player1Cards);

        }

       

        foreach (CardController card in cards)
        {
            if(card != null)
            {

            if (card == null || card.card.health <= 0)
            {
                Destroy(card.gameObject);
            }
            }
        }
        
       

        foreach (CardController card in enemyCards)
        {
            if (card != null)
            {
                if (card.card.health <= 0)
            {
                Destroy(card.gameObject);
            }
            }

        }



        player1Cards.Clear();
        player2Cards.Clear();


        foreach (CardController card in cards)
        {
            if (card != null)
            {
                if (ID == 0)
                {
                    player1Cards.Add(card);

                }
                else
                {
                    player2Cards.Add(card);
                }
            }
        }

        foreach (CardController card in enemyCards)
        {
            if (card != null)
            {
                if (ID == 1)
                {
                    player1Cards.Add(card);

                }
                else
                {
                    player2Cards.Add(card);
                }
            }
        }
    }

    public void ProcessEndTurn(int ID)
    {
        List<CardController> cards = new List<CardController>();
        List<CardController> enemyCards = new List<CardController>();

        if (ID == 0)
        {
            cards.AddRange(player1Cards);
            enemyCards.AddRange(player2Cards);

        }
        else
        {
            cards.AddRange(player2Cards);
            enemyCards.AddRange(player1Cards);

        }

        foreach (CardController cardController in cards)
        {
            if (AreThereCardsWithHealth(enemyCards))
            {

                int randonEnemyCard = UnityEngine.Random.Range(0, enemyCards.Count);
                while (enemyCards[randonEnemyCard].card.health <= 0)
                {
                    randonEnemyCard = UnityEngine.Random.Range(0, enemyCards.Count);
                }
                enemyCards[randonEnemyCard].Damage(cardController.card.damage);
                cardController.Damage(enemyCards[randonEnemyCard].card.damage);
            }
            else
            {
                int enemyID = ID == 0 ? 1 : 0;
                PlayerManager.instance.DamagePlayer(enemyID, cardController.card.damage);
            }
        }
    }



    private bool AreThereCardsWithHealth(List<CardController> cards)
    {
        bool cardHasHealth = false;

        foreach (CardController card in cards)
        {
            if (card.card.health > 0)
            { cardHasHealth = true; break; }
        }

        return cardHasHealth;
    }
}
