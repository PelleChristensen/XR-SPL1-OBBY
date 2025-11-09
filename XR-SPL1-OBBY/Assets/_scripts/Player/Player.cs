using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public enum PlayerState { DEAD, ARRIVED, STARTED };
    private SpawnPoint spawnpoint;
    public static UnityAction<PlayerState> playerstate;

    #region 
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("on trigger enter: " + other.gameObject.tag);
        string tag = other.gameObject.tag;

        switch (tag)
        {
            case "FALLZONE":
                playerstate?.Invoke(PlayerState.DEAD);
                break;

            case "SPAWNPOINT":
                spawnpoint = other.gameObject.GetComponent<SpawnPoint>();
                spawnpoint.ActivateSpray();
                break;

            case "GOAL":
                playerstate?.Invoke(PlayerState.ARRIVED);
                break;

            case "STARTTRIGGER":
                playerstate?.Invoke(PlayerState.STARTED);
                break;     

        }
    }

    private void Respawn()
    {
        transform.position = spawnpoint.transform.position;
        spawnpoint.ActivateSpray();
    }
    #endregion

    void Awake()
    {
        GameManager.gamestatechange += OnGameStateChange;
    }

    void OnDisable()
    {
        GameManager.gamestatechange -= OnGameStateChange;
    }

    void OnEnable()
    {
        GameManager.gamestatechange += OnGameStateChange;
    }

    private void OnGameStateChange(GameManager.EventType newstate)
    {
        switch (newstate)
        {
            case GameManager.EventType.GAMEOVER:
            case GameManager.EventType.RESPAWN : 
                Respawn();
            break; 
            
        }
    }
}
