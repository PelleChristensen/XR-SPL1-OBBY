using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private StartTrigger trigger;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collission: " + other.gameObject.tag); 
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Collission");
            //trigger.triggeraction.Invoke();
        } 
    }
}
