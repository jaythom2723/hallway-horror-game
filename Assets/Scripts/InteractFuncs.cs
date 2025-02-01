using System.Collections.Generic;
using UnityEngine;

public static class InteractFuncs
{
    public delegate void InteractCallbackFunc(GameObject obj, GameObject ply);

    public static void InteractDoorknob(GameObject obj, GameObject ply)
    {
        float mouseY = -Input.GetAxis("Mouse Y");
        float pushForce = 5.0f;

        Debug.Log(pushForce * mouseY);

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        rb.AddForce(new Vector3(pushForce * mouseY, 0.0f, 0.0f));
    }

    // the inspection system before an item gets shipped to its designated area.
    private static void GeneralInteract(GameObject obj, GameObject ply)
    {

    }

    public static void InteractKey(GameObject obj, GameObject ply)
    {
        GeneralInteract(obj, ply);

        Debug.Log("Key");
    }

    public static void InteractNote(GameObject obj, GameObject ply)
    {
        GeneralInteract(obj, ply);

        Debug.Log("Note");
    }

    public static void InteractArtifact(GameObject obj, GameObject ply)
    {
        GeneralInteract(obj, ply);

        Debug.Log("Artifact");
    }

    public static void InteractRelic(GameObject obj, GameObject ply)
    {
        GeneralInteract(obj, ply);

        Debug.Log("Relic");
    }
}; 