using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private Material activematerial, inactivematerial;

    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Animator animator;

    //TODO BASIC assignment add the "interact" action defines in layer actions in the action assets to open and close the door
    //TODO create reference to Input Asset and the action you want to call the Interact Action 
    //TODO bind the action to 

    //HELP seek inspiration in other scripts using input system. There are alt least some. 

    //Advanced make the close door action as a different input  
    //Advanced if you got skills in the animation system make the door open in different directions depending on which side you are standing on. 

    private bool _isActivated = false;
    private bool _isDoorOpen = false; 

    //TODO remember to set the parameter for receiving the right context

    void Awake()
    {
        //TODO get the action set up for using OpenClose
    }

    private void OnEnable()
    {
        //TODO enable the action if this gameobject is enabled
        //its a bit lige what goes on in awake
    }

    private void OnDisable()
    {
         //TODO disable the action if this gameobject is disabled
    }

    //TODO this function needs to have a parameter. Find out what it is. 
    private void OpenClose()
    {
        if (_isDoorOpen)
        {
            animator.SetTrigger("Close");
            _isDoorOpen = false;
        }
        else
        {
            animator.SetTrigger("Open");
            _isDoorOpen = true;
        }
    }

    void Start()
    {
        SetMaterial(_isActivated);
    }

    //TODO write comments to explain what is happening here
    private void SetMaterial(bool isActive)
    {
        if (isActive)
        {
            mesh.material = activematerial;
        }
        else
        {
            mesh.material = inactivematerial;
        }
    }

    //TODO write comments to explain what is happening here
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActivated = true;
            SetMaterial(_isActivated);
        }
    }

    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isActivated = false;
            SetMaterial(_isActivated);
        }
    }
}
