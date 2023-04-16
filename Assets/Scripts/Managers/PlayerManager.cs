using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public static PlayerManager instance;
    
    public List<Player> players = new List<Player>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIManager.instance.UpdateValues(players[0], players[1]);
    }

    internal void AssignTurn(int currentPlayerTurn)
    {
        foreach (Player player in players)
        {
            player.myTurn = player.ID == currentPlayerTurn;
            if (player.myTurn) player.mana = 5;
        }

        
        
        
    }

    public Player FindPlayerByID(int ID)
    {
        return players.Find(x => x.ID == ID);
    }

    public void DamagePlayer(int ID, int damage)
    {
        Player player = FindPlayerByID(ID);

        player.health -= damage;

        UIManager.instance.UpdateHealthValues(players[0].health, players[1].health);

        if (player.health <= 0)
        {
            PlayerLost(ID);
        }
    }

    private void PlayerLost(int iD)
    {
        
    }

    internal void SpendMana(int ownerID, int manaCost)
    {
        FindPlayerByID(ownerID).mana -= manaCost;
        UIManager.instance.UpdateManaValues(players[0].mana, players[1].mana);
    }
}
