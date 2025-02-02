using UnityEngine;
using System.Collections.Generic;
using static InteractFuncs;
using static RifyDev.Utility;

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

    public bool Holding {
        get { return isHolding; }
        set { isHolding = value; }
    }

    private float moveSpeed = 5f;
    private float mouseSensitivity = 2f;

    private bool isDragging = false;
    private bool isHolding = false;

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
        nearbyInteractableObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentInteractableObject == other.gameObject && !CheckCameraVisibility(camera, other.gameObject))
            currentInteractableObject = null;
        nearbyInteractableObjects.Remove(other.gameObject);
    }

    private void Interact()
    {
        if (!currentInteractableObject || currentInteractableObject.tag == "Untagged")
            return;

        // if (currentInteractableObject.tag == "Doorknob" && Input.GetMouseButton(0))
        // {
        //     isDragging = true;
        // } else if(currentInteractableObject.tag != "Doorknob" && Input.GetMouseButtonDown(0))
        // {
        //     isHolding = true;
        // } else
        // {
        //     isHolding = false;
        //     isDragging = false;
        // }

        interactFuncs[currentInteractableObject.tag](currentInteractableObject, gameObject);
        Debug.Log(isDragging);
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
        if (!isDragging && !isHolding) 
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            transform.Rotate(Vector3.up * mouseX);
            camera.transform.Rotate(Vector3.left * mouseY);
        }
    }

    private void InteractionChecker()
    {
        // if the current interactable objects leaves camera space at any time, set it to null
        if (currentInteractableObject && !CheckCameraVisibility(camera, currentInteractableObject))
            currentInteractableObject = null;

        if (nearbyInteractableObjects.Count <= 0)
            return;

        List<float> distances = new List<float>();
        List<GameObject> objs = new List<GameObject>();

        // get the distances from all nearby interactable objects
        foreach(var obj in nearbyInteractableObjects)
        {
            Vector3 direction = obj.transform.position - camera.transform.position;
            RaycastHit hit;
            bool test = Physics.Raycast(camera.transform.position, direction, out hit, interactionBubble.radius * 500.0f) && CheckCameraVisibility(camera, obj);
            if(test)
            {
                distances.Add(hit.distance);
                objs.Add(hit.transform.gameObject);
            }
        }

        if(distances.Count <= 0 && objs.Count <= 0)
            return;
        
        // sort through the distances to get the closest one to the player's camera
        BubbleSortFGO(ref distances, ref objs);
        currentInteractableObject = objs[0];
    }
}
