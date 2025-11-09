using UnityEngine;

public class Goal : MonoBehaviour
{
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Player has arrived. Stop countdown
        }
    }
}
