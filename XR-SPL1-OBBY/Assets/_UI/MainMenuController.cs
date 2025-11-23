using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class MainMenuController : MonoBehaviour
{

    public VisualElement ui;

    public Button playButton;
    public Button highscorebutton;
    public Button exitbutton;
    
    public enum UIMessage { STARTGAME, EXITGAME }; 
    public static UnityAction<UIMessage> mainmenumessages; 

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;

        GameManager.gamestatechange += OnGameStateChange;

        playButton = ui.Q<Button>("startbutton");
        playButton.clicked += OnPlayButtonClicked;

        exitbutton = ui.Q<Button>("exitbutton");
        exitbutton.clicked += OnExitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        Debug.Log("Playbutton pressed");
        mainmenumessages.Invoke(UIMessage.STARTGAME);
        ui.style.display = DisplayStyle.None; 
    }

    private void OnGameStateChange(GameManager.EventType newstate)
    {
        switch (newstate)
        {
            case GameManager.EventType.GAMEOVER:
               // ui.style.display = DisplayStyle.Flex; 
            break; 
            
        }
    }

    private void OnExitButtonClicked ()
    {
        //mainmenumessages.Invoke(UIMessage.EXITGAME); 
    }

}
