using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    public Camera mainCamera;
    private GameObject heldObject;

    public float throwForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check is ray hit anything
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit object: " + hit.transform.name);

            // Pick up object
            if (Input.GetMouseButtonDown(0))
            {
                if (heldObject == null && !hit.transform.name.Equals("Plane"))
                {
                    heldObject = hit.transform.gameObject;

                    Rigidbody heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();

                    // Set the ball's velocity, angular velocity, and acceleration to zero
                    heldObjectRigidbody.velocity = Vector3.zero;
                    heldObjectRigidbody.angularVelocity = Vector3.zero;
                }
                else if (heldObject != null && hit.transform.gameObject == heldObject)
                {
                    heldObject = null;
                }
            }

            
        }

        // release the object
        else if (Input.GetMouseButtonDown(0) && heldObject != null)
        {
            heldObject = null;
        }

        // Throw object
        if (Input.GetMouseButtonDown(1) && heldObject != null)
        {
            Vector3 throwDirection = (heldObject.transform.position - mainCamera.transform.position).normalized;
            float throwDistance = Vector3.Distance(heldObject.transform.position, mainCamera.transform.position);
            Vector3 throwForceVector = throwDirection * throwDistance * throwForce;
            Rigidbody heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
            heldObjectRigidbody.AddForce(throwForceVector, ForceMode.Impulse);
            heldObject = null;
        }

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);

        // Make object come in front of camera and face upward
        if (heldObject != null)
        {
            Vector3 offset = mainCamera.transform.forward * 3f;
            heldObject.transform.position = mainCamera.transform.position + offset;
            heldObject.transform.rotation = mainCamera.transform.rotation;
            heldObject.transform.Rotate(Vector3.left, 90f);
        }
    }

}
