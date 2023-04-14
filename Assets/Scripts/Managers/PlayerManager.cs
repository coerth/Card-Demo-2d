using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public static PlayerManager instance;
    
    public List<Player> PlayerList = new List<Player>();

    private void Awake()
    {
        instance = this;
    }

    internal void AssignTurn(int currentPlayerTurn)
    {
        Player player = PlayerList.Find(x => x.ID == currentPlayerTurn);
        player.myTurn = true;
    }
}
