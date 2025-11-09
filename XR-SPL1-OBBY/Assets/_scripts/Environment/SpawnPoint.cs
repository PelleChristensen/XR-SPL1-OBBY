using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public ParticleSystem spray;

    public void ActivateSpray()
    {
        spray.Play();
    }
}
