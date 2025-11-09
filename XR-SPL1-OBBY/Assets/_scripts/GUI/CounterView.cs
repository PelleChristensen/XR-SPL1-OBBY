using UnityEngine;
using TMPro;

public class CounterView : MonoBehaviour
{
    public TMP_Text counter;

    void Awake()
    {
        CountDown.countevent += OnCountUpdate; 
    }

    void OnEnable()
    {
        CountDown.countevent += OnCountUpdate; 
    }

    void OnDisable()
    {
        CountDown.countevent -= OnCountUpdate; 
    }

    private void OnCountUpdate(CountDown.EventTypes type, float value)
    {
        switch (type)
        {
            case CountDown.EventTypes.TICK:
            case CountDown.EventTypes.START:
                UpdateText(value.ToString("F0"));
            break;
        }
    }

    public void UpdateText(string value)
    {
        //Debug.Log("On count; " + value);
        counter.text = value;
    }
}
