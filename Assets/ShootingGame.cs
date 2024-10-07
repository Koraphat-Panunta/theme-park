using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShootingGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Gun;
    public RawImage crosshair;
    public Canvas canvas;
    public bool ShootAble;
    private void Start()
    {
        crosshair.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&ShootAble == true)
        {
            ApplyForceAtMousePosition();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("ShootingRange Stay");
        if(other.TryGetComponent<PlayerController>(out PlayerController P))
        {
            ShootAble = true;
            ConstraintSource constraint = new ConstraintSource();
            constraint.sourceTransform = P.weaponSocket.transform;
            constraint.weight = 1;

            ParentConstraint parentConstraint = Gun.GetComponent<ParentConstraint>();
            parentConstraint.AddSource(constraint);
            parentConstraint.constraintActive = true;
            parentConstraint.translationAtRest = Vector3.zero;
            parentConstraint.rotationAtRest = Vector3.zero;
            parentConstraint.constraintActive = true;
            crosshair.enabled = true;
            // Get the mouse position in screen space
            Vector2 mousePosition = Input.mousePosition;

            // Convert the screen space position to canvas space
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), mousePosition, canvas.worldCamera, out canvasPosition);

            // Set the position of the RawImage in canvas space
            crosshair.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController P))
        {
            ShootAble = false;
            ParentConstraint parentConstraint = Gun.GetComponent<ParentConstraint>();
            parentConstraint.RemoveSource(0);
            parentConstraint.constraintActive = false;
            parentConstraint.translationAtRest = Vector3.zero;
            parentConstraint.rotationAtRest = Vector3.zero;
            parentConstraint.constraintActive = false;
            crosshair.enabled = false;
        }
    }
    void ApplyForceAtMousePosition()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Perform the raycast and check if it hits the object with the Rigidbody
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit name ="+hit.transform.gameObject.name);
            Rigidbody rb = hit.rigidbody;
            // Check if the hit object is this object
            // Get the world position where the mouse clicked
            Vector3 hitPoint = hit.point;

            // Calculate the direction to apply the force (can be customized as needed)
            Vector3 forceDirection = ray.direction.normalized;

            // Apply force at the point of the mouse click
            rb.AddForceAtPosition(forceDirection * 120, hitPoint, ForceMode.Impulse);
          

            // Optional: Debug to visualize the force being applied
            Debug.DrawRay(hitPoint, forceDirection * 2f, Color.red, 2f);
        }
        else
        {
            
        }
    }
}
