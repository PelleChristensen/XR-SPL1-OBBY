using UnityEngine;
using UnityEngine.InputSystem;

public class TPSController : MonoBehaviour
{

    [Header("References")]
    public Transform character;     // your character root (rotates around Y)
    public Transform cameraPivot;   // parent of Camera (rotates around X)

    [Header("Look settings")]
    [Tooltip("Degrees per mouse unit (tune to taste).")]
    public float sensitivity = 0.12f;
    public float minPitch = -70f;
    public float maxPitch = 70f;
    public bool invertY = false;

    [Header("Cursor")]
    public bool lockCursor = true;

    float _yaw;
    float _pitch;

    public InputActionReference lookAction;


    void Awake()
    {
        if (!character) character = transform;

        if (lookAction && lookAction.action != null)
        {
            lookAction.action.Enable();
        }
    }

    void OnEnable()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Initialize yaw/pitch from current transforms so we don't snap
        if (character) _yaw = character.rotation.eulerAngles.y;
        if (cameraPivot) _pitch = NormalizeAngle(cameraPivot.localEulerAngles.x);
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (lookAction != null) lookAction.action.Disable();
    }
    
    void Update()
    {
        Vector2 look = lookAction.action.ReadValue<Vector2>();

        float ySign = invertY ? 1f : -1f; // typical FPS-style: move mouse up => look up (negative pitch)

        _yaw   += look.x * sensitivity;
        _pitch += look.y * sensitivity * ySign;
        _pitch  = Mathf.Clamp(_pitch, minPitch, maxPitch);

        if (character)
            character.rotation = Quaternion.Euler(0f, _yaw, 0f);

        if (cameraPivot)
            cameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    static float NormalizeAngle(float a)
    {
        // Convert [0..360) to [-180..180)
        a %= 360f;
        if (a > 180f) a -= 360f;
        return a;
    }

}
