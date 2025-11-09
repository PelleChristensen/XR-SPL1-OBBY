using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public enum EventType { START, GAMEOVER, RESPAWN, GAMECOMPLETE };

    public static UnityAction<EventType> gamestatechange;

    void Awake()
    {
        CountDown.countevent += OnCountEvent;
        Player.playerstate += OnPlayerStateChanged; 
    }

    void OnEnable()
    {
        CountDown.countevent += OnCountEvent;
        Player.playerstate += OnPlayerStateChanged; 
    }

    void OnDisable()
    {
        CountDown.countevent -= OnCountEvent;
        Player.playerstate -= OnPlayerStateChanged;         
    }

    private void OnPlayerStateChanged(Player.PlayerState newstate)
    {
        //Debug.Log("Gamemanager.playerstatechange: " + newstate);
        switch (newstate)
        {
            case Player.PlayerState.DEAD:
                gamestatechange.Invoke(EventType.GAMEOVER);
                break;
            case Player.PlayerState.ARRIVED:
                gamestatechange.Invoke(EventType.GAMECOMPLETE);
                break;
            case Player.PlayerState.STARTED:
                gamestatechange.Invoke(EventType.START);
            break;
        }
    }

    private void OnCountEvent(CountDown.EventTypes type, float value)
    {
        switch (type)
        {
            case CountDown.EventTypes.TIMEOUT:
                //send gameover event
                gamestatechange.Invoke(EventType.GAMEOVER); 
                break;
        }
    }
}
