using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DoorButton : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private float buttonSpeed = 2.0f;
    [SerializeField] private GameObject buttonObject;
    [Header("Door Settings")]
    [SerializeField] private float doorSpeed = 2.0f;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private GameObject doorObjectWaypoint;
    [Header("DoorCrushed Settings")]
    [SerializeField] private GameObject doorCrushObject;
    [SerializeField] private GameObject doorObjectCrushWaypoint;
    [SerializeField] private float crushSpeed = 2.0f;

    [SerializeField] private LayerMask validLayers;

    private Vector3 buttonUpPosition;
    private Vector3 buttonDownPosition;
    private Vector3 doorUpPosition;
    private Vector3 doorDownPosition;
    private Vector3 doorCrushUpPosition;
    private Vector3 doorCrushDownPosition;

    private BoxCollider triggerCollider;
    public bool isPressed { get; private set; }

    private void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();

        isPressed = false;
        buttonUpPosition = buttonObject.transform.position;
        buttonDownPosition = new Vector3(buttonUpPosition.x, buttonUpPosition.y - 0.45f, buttonUpPosition.z);

        doorUpPosition = doorObject.transform.position;
        doorDownPosition = doorObjectWaypoint.transform.position;

        doorCrushUpPosition = doorCrushObject.transform.position;
        doorCrushDownPosition = doorObjectCrushWaypoint.transform.position;
    }

    private void Update()
    {
        CheckButtonPress();

        Vector3 buttonTargetPosition = isPressed ? buttonDownPosition : buttonUpPosition;
        Vector3 doorTargetPosition = isPressed ? doorDownPosition : doorUpPosition;
        Vector3 doorCrushTargetPostion = isPressed ? doorDownPosition : doorUpPosition;

        float doorMovingSpeed = isPressed ? doorSpeed : doorSpeed * 4;

        buttonObject.transform.position = Vector3.MoveTowards(buttonObject.transform.position, buttonTargetPosition, buttonSpeed * Time.deltaTime);
        doorObject.transform.position = Vector3.MoveTowards(doorObject.transform.position, doorTargetPosition, doorMovingSpeed * Time.deltaTime);
        doorCrushObject.transform.position = Vector3.MoveTowards(doorCrushObject.transform.position, doorCrushTargetPostion, crushSpeed * Time.deltaTime);

    }

    private void CheckButtonPress()
    {
        Collider[] colliders = Physics.OverlapBox(triggerCollider.bounds.center, triggerCollider.bounds.extents, triggerCollider.transform.rotation, validLayers);

        isPressed = colliders.Length > 0;
    }
}
