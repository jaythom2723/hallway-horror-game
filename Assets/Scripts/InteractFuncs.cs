using UnityEngine;

public static class InteractFuncs
{
    public delegate void InteractCallbackFunc(GameObject obj, GameObject ply);

    public static void InteractDoorknob(GameObject obj, GameObject ply)
    {
        float mouseY = Input.GetAxis("Mouse Y");
        float pushForce = 5.0f;

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        rb.AddForce(new Vector3(pushForce * mouseY, 0.0f, 0.0f));
    }

    public static void InteractKey(GameObject obj, GameObject ply)
    {
        Debug.Log("Key");
    }

    public static void InteractNote(GameObject obj, GameObject ply)
    {
        Debug.Log("Note");
    }

    public static void InteractArtifact(GameObject obj, GameObject ply)
    {
        Debug.Log("Artifact");
    }

    public static void InteractRelic(GameObject obj, GameObject ply)
    {
        Debug.Log("Relic");
    }
};