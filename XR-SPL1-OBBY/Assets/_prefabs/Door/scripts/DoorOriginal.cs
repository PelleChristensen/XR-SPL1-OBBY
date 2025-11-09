using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorOriginal : MonoBehaviour
{
    [SerializeField] private Material activematerial, inactivematerial;

    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Animator animator;

    //assignment 

    //TODO create reference to Input Asset and the action you want to call the Interact Action 

    //Advanced make the close door action as a different input  

    public InputActionReference activateaction;

    private bool _isActivated = false;
    private bool _isDoorOpen = false;

    void Awake()
    {
        activateaction.action.Enable();
        activateaction.action.performed += OpenClose; 
    }

    private void OnEnable()
    {
        //TODOenable the action if this gameobject is enabled
        //its a bit like what goes on in awake
    }

    private void OnDisable()
    {
         //TODO disable the action if this gameobject is disabled
    }

    //TODO remember to set the parameter for receiving the right context
    private void OpenClose(InputAction.CallbackContext context)
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
