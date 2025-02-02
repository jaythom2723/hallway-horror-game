using UnityEngine;

public static class InteractFuncs
{
    private static Vector3 ogPos;
    private static Quaternion ogRot;
    public static PlayerController pc;

    public delegate void InteractCallbackFunc(GameObject obj, GameObject ply);

    public static void InteractDoorknob(GameObject obj, GameObject ply)
    {
        pc.Dragging = Input.GetMouseButton(0);
        if(!pc.Dragging)
            return;

        float mouseY = Input.GetAxis("Mouse Y");
        float pushForce = 5.0f;

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        rb.AddForce(obj.transform.forward * (pushForce * mouseY));
    }

    // pick up the object and display it to the player at the interaction marker
    private static void GeneralInteract(GameObject obj, GameObject ply)
    {
        if(!pc.Holding && Input.GetMouseButtonDown(0))
        {
            pc.Holding = true;
            ogPos = obj.transform.position;
            ogRot = obj.transform.rotation;

            obj.transform.position = pc.InteractPosition.position;
        }

        if(pc.Holding && Input.GetKeyDown(KeyCode.E))
        {
            // TODO: put the item in the inventory or journal
            // for now, exit interact mode & delete the object from the game world.
            pc.Holding = false;
            obj.transform.position = ogPos;
            obj.transform.rotation = ogRot;
            GameObject.Destroy(obj);
        } else if(pc.Holding && Input.GetKeyDown(KeyCode.Q)) // put the item down and do nothing
        {
            pc.Holding = false;
            obj.transform.position = ogPos;
            obj.transform.rotation = ogRot;
        }
    }

    public static void InteractKey(GameObject obj, GameObject ply)
    {
        GeneralInteract(obj, ply);
        if(!pc.Holding)
            return;

        // allow the player to drag the item around
        

        // Debug.Log("Key");
    }

    public static void InteractNote(GameObject obj, GameObject ply)
    {
        GeneralInteract(obj, ply);
        if(!pc.Holding)
            return;

        // Debug.Log("Note");
    }

    public static void InteractArtifact(GameObject obj, GameObject ply)
    {
        GeneralInteract(obj, ply);
        if(!pc.Holding)
            return;

        // Debug.Log("Artifact");
    }

    public static void InteractRelic(GameObject obj, GameObject ply)
    {
        GeneralInteract(obj, ply);
        if(!pc.Holding)
            return;

        // Debug.Log("Relic");
    }
}; 