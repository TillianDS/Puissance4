using UnityEngine;
using UnityEngine.InputSystem;

public class VRLocomotion : MonoBehaviour
{
    public float speed = 2.0f;
    public float rotationSpeed = 2.0f;

    public InputActionProperty moveInput; // Joystick VR
    private CharacterController characterController;

    private float rotationX = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquer la souris au centre
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        // 🎮 Déplacement au joystick (VR)
        if (moveInput.action != null)
        {
            Vector2 input = moveInput.action.ReadValue<Vector2>();
            move += transform.right * input.x + transform.forward * input.y;
        }

        // ⌨️ Déplacement clavier
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) move += transform.forward;  // Avancer
        if (Input.GetKey(KeyCode.S)) move -= transform.forward;  // Reculer
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) move -= transform.right;  // Gauche
        if (Input.GetKey(KeyCode.D)) move += transform.right;  // Droite

        // Appliquer le mouvement
        characterController.Move(move * speed * Time.deltaTime);

        // 🖱️ Rotation avec la souris (Simule la tête du joueur)
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        rotationX += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -90, 90); // Empêcher de regarder complètement derrière

        transform.Rotate(Vector3.up * mouseX);
    }
}
