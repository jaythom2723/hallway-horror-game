using System.Collections.Generic;
using UnityEngine;

public static class InteractFuncs
{
    public delegate void InteractCallbackFunc(GameObject obj, GameObject ply);

    public static void InteractDoorknob(GameObject obj, GameObject ply)
    {
        PlayerController pc = ply.GetComponent<PlayerController>();
        pc.Dragging = Input.GetMouseButton(0);
        if(!pc.Dragging)
            return;

        float mouseY = Input.GetAxis("Mouse Y");
        float pushForce = 5.0f;

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        rb.AddForce(obj.transform.forward * (pushForce * mouseY));
    }

    // the inspection system before an item gets shipped to its designated area.
    private static bool GeneralInteract(GameObject obj, GameObject ply)
    {
        return false;
    }

    public static void InteractKey(GameObject obj, GameObject ply)
    {
        if(!GeneralInteract(obj, ply))
            return;

        // Debug.Log("Key");
    }

    public static void InteractNote(GameObject obj, GameObject ply)
    {
        if(!GeneralInteract(obj, ply))
            return;
        
        // Debug.Log("Note");
    }

    public static void InteractArtifact(GameObject obj, GameObject ply)
    {
        if(!GeneralInteract(obj, ply))
            return;

        // Debug.Log("Artifact");
    }

    public static void InteractRelic(GameObject obj, GameObject ply)
    {
        if(!GeneralInteract(obj, ply))
            return;

        // Debug.Log("Relic");
    }
}; 