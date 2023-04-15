using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public int currentPlayerTurn;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartTurnGamePlay(0);
    }

    public void StartTurnGamePlay(int playerID)
    {
        currentPlayerTurn = playerID;
        StartTurn();
    }

    public void StartTurn()
    {
        GameplayUIController.instance.UpdateCurrentPlayerTurn(currentPlayerTurn);
        PlayerManager.instance.AssignTurn(currentPlayerTurn);

    }

    public void EndTurn()
    {
        currentPlayerTurn = currentPlayerTurn == 0 ? 1 : 0;
        StartTurn();
    }
}
