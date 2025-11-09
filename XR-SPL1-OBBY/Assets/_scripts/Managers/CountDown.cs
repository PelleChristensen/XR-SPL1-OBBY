using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    public enum EventTypes { TIMEOUT, TICK, START };

    public static UnityAction<EventTypes, float> countevent;

    private int count = 0;
    public int CountDownSeconds = 15;
    private float currenttime = 0; 
    private bool gamerunning = false; 
    private IEnumerator coroutine;

    public void ResetCounter()
    {
        currenttime = CountDownSeconds;
        countevent?.Invoke(EventTypes.TICK, count); 
    }


    void Awake()
    {
        GameManager.gamestatechange += onGameStateChange; 
    }

    private void onGameStateChange(GameManager.EventType type)
    {
        switch (type)
        {
            case GameManager.EventType.RESPAWN:

                break;
            case GameManager.EventType.GAMEOVER:
                gamerunning = false;
                ResetCounter();
                break;
            case GameManager.EventType.GAMECOMPLETE:
                gamerunning = false;
                ResetCounter();
                break;
            case GameManager.EventType.START:
                StartCountdown(); 
                break; 
        }
    }

    public void StartCountdown()
    {
        //Debug.Log("Start countdown");
        gamerunning = true;
        ResetCounter();
        //StartCoroutine(WaitAndCount());     
    }

    void Update()
    {
        if (!gamerunning) return;

        currenttime -= Time.deltaTime;

        if (currenttime <= 0)
        {
            currenttime = 0f;
            gamerunning = false;
            countevent?.Invoke(EventTypes.TIMEOUT, currenttime);
        }
        else
        {
            countevent?.Invoke(EventTypes.TICK, currenttime);
        }
    }

    private void Tick()
    {
        count--;
        if (count > 0)
        {
            Debug.Log("TICK: " + count);
            countevent?.Invoke(EventTypes.TICK, count);
            StartCoroutine(WaitAndCount());
        }
        else if (count <= 0)
        {
            Debug.Log("TIME OUT");
            countevent?.Invoke(EventTypes.TIMEOUT, 0);
        }
    }

    private IEnumerator WaitAndCount()
    {
        yield return new WaitForSecondsRealtime(1);
        if(gamerunning) Tick(); 
    }
}
