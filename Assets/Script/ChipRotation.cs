using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChipRotation : MonoBehaviour
{
    private Spawner spawner;
    private Vector3 initialPosition; // Position de d�part
    private float floatSpeed = 1.5f; // Vitesse de mont�e/descente
    private float floatAmplitude = 0.1f; // Amplitude du mouvement vertical
    private float rotationSpeed = 50f; // Vitesse de rotation
    private bool bISGrab = false;

    public void SetSpawner(Spawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    void Start()
    {
        // Sauvegarde la position initiale
        initialPosition = transform.position;

        // Ajout du listener pour le grab
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnGrabExit);
        grabInteractable.selectEntered.AddListener(OnGrab);

    }

    void Update()
    {
        // Fait tourner le jeton
        if(!bISGrab){
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            // Fait flotter le jeton en utilisant une sinuso�de
            float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        bISGrab = true;
        transform.position = initialPosition;
    }

    void OnGrabExit(SelectExitEventArgs args)
    {
        Debug.Log("Chip was grabbed and released!");

        spawner.SpawnChip();

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("Rigidbody found, disabling kinematic.");
            rb.isKinematic = false;
        }
        else
        {
            Debug.LogWarning("No Rigidbody found on this object!");
        }

        Destroy(this);
    }
}
