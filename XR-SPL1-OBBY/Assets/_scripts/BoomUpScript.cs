using UnityEngine;

public class BoomUpScript : MonoBehaviour
{
    float launchforce = 10f; 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * launchforce, ForceMode.Impulse);
                
                
            }


        }

        
    }

}
