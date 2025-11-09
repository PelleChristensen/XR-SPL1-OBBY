using UnityEngine;
using UnityEngine.Events;

public class StartTrigger : MonoBehaviour
{
    void Awake()
    {
        GameManager.gamestatechange += OnGameStateChange; 
    }

    void OnEnable()
    {
        GameManager.gamestatechange += OnGameStateChange;       
    }

    void OnDisable()
    {
        //GameManager.gamestatechange -= OnGameStateChange; 
    }

    void OnDestroy()
    {
        GameManager.gamestatechange -= OnGameStateChange; 
    }

    private void OnGameStateChange(GameManager.EventType type)
    {
        switch (type)
        {
            case GameManager.EventType.START:
                ActivateStart();
                break;

            case GameManager.EventType.GAMECOMPLETE:

            case GameManager.EventType.GAMEOVER:
                Reset();
                break;
        }
    }

    private void ActivateStart()
    {
        //Debug.Log("Startrigger removed"); 
        this.gameObject.SetActive(false);
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);    
    }
}
