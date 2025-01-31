using UnityEngine;
using System.Collections.Generic;
using static InteractFuncs;

public class PlayerController : MonoBehaviour
{
    public float Sensitivty {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }

    public bool Dragging {
        get { return isDragging; }
        set { isDragging = value; }
    }

    private float moveSpeed = 5f;
    private float mouseSensitivity = 2f;

    private bool isDragging = false;

    private CharacterController controller;

    [SerializeField] private Camera camera;

    private SphereCollider interactionBubble;
    private List<GameObject> nearbyInteractableObjects;
    private GameObject currentInteractableObject;

    private Dictionary<string, InteractCallbackFunc> interactFuncs;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        interactionBubble = GetComponent<SphereCollider>();
        nearbyInteractableObjects = new List<GameObject>();

        interactFuncs = new Dictionary<string, InteractCallbackFunc>();
        interactFuncs.Add("Doorknob", InteractDoorknob);
        interactFuncs.Add("Key", InteractKey);
        interactFuncs.Add("Note", InteractNote);
        interactFuncs.Add("Artifact", InteractArtifact);
        interactFuncs.Add("Relic", InteractRelic);
    }

    void Update()
    {
        PlayerMovement();
        InteractionChecker();
        Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the object is not in view of the camera do nothing
        if(checkCameraVisibility(other.transform.position))
            nearbyInteractableObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentInteractableObject == other.gameObject && !checkCameraVisibility(other.transform.position))
            currentInteractableObject = null;
        nearbyInteractableObjects.Remove(other.gameObject);
    }

    private void Interact()
    {
        if (Input.GetMouseButton(0) && currentInteractableObject)
        {
            if (currentInteractableObject.tag == "Doorknob")
                isDragging = true;

            interactFuncs[currentInteractableObject.tag](currentInteractableObject, gameObject);
        } else {
            isDragging = false;
        }
    }

    private void PlayerMovement()
    {
        // player movement
        float moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxisRaw("Vertical") * moveSpeed;
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.SimpleMove(move);

        // player camera rotation
        // TODO: linearly interpolate the camera rotation
        // ^ polishing phase
        if (!isDragging) 
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            transform.Rotate(Vector3.up * mouseX);
            camera.transform.Rotate(Vector3.left * mouseY);
        }
    }

    private void InteractionChecker()
    {
        foreach (var obj in nearbyInteractableObjects)
        {
            // shoot a ray to determine the closest interactable object
            Vector3 direction = obj.transform.position - camera.transform.position;
            RaycastHit hit;

            if(Physics.Raycast(camera.transform.position, direction, out hit, interactionBubble.radius * 500.0f))
                currentInteractableObject = hit.transform.gameObject;
        }
    }

    private bool checkCameraVisibility(Vector3 pos)
    {
        Vector3 viewPos = camera.WorldToViewportPoint(pos);
        return (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0);
    }
}
