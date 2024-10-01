using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    [Header("Character References")]
    [SerializeField] private CharacterController playerCharacterController;
    [SerializeField] private MouseLook playerMouseLook;

    private Vector3 movementInput;
    private Vector2 lookInput;

    [Header("Smooth")]
    public float smooth = 10f;
    public float smoothRot = 12f;

    [Header("Sway")]
    public float step = 0.01f;
    public float maxStepDistance = 0.06f;
    Vector3 swayPos; // store our sway position (result of sway)

    [Header("SwayRotation")]
    public float rotationStep = 4f;
    public float maxRotationStep = 5f;
    Vector3 swayEulerRot; // store our value

    [Header("Bobbing")]
    public float speedCurve;

    [Header("Bob Rotation")]
    public Vector3 multiplier;
    Vector3 bobEulerRotation;

    float curveSin { get => Mathf.Sin(speedCurve); }
    float curveCos { get => Mathf.Cos(speedCurve); }

    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.01f;

    Vector3 bobPostition;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacterController = GameObject.Find("Player").GetComponent<CharacterController>();
        playerMouseLook = GameObject.Find("PlayerHeadCamera").GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        Sway();
        SwayRotation();
        BobOffset();
        BobRotation();

        CompositePositionRotation();
    }

    void GetInput()
    {
        lookInput.x = Input.GetAxis("Mouse X") * playerMouseLook.mouseSensitivity * Time.deltaTime;
        lookInput.y = Input.GetAxis("Mouse Y") * playerMouseLook.mouseSensitivity * Time.deltaTime;

        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.z = Input.GetAxis("Vertical");
        movementInput = movementInput.normalized;
    }

    void Sway ()
    {
        Vector3 invertLook = lookInput * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

        swayPos = invertLook;
    }

    void SwayRotation()
    {
        Vector2 invertLook = lookInput * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }

    void BobOffset()
    {
        speedCurve += Time.deltaTime * (playerCharacterController.isGrounded ? playerCharacterController.velocity.magnitude : 1f) + 0.01f;

        bobPostition.x =
            (curveCos * bobLimit.x * (playerCharacterController.isGrounded ? 1 : 0))
            - (movementInput.x * travelLimit.x);

        bobPostition.y =
            (curveSin * bobLimit.y)
            - (playerCharacterController.velocity.y * travelLimit.y);

        bobPostition.z =
            -(movementInput.y * travelLimit.z);
    }

    void BobRotation()
    {
        bobEulerRotation.x = (movementInput != Vector3.zero ? multiplier.x * (Mathf.Sin(2 * speedCurve)) : 
                                                              multiplier.x * (Mathf.Sin(2 * speedCurve) / 2)); // pitch
        bobEulerRotation.y = (movementInput != Vector3.zero ? multiplier.y * curveCos : 0); // yaw
        bobEulerRotation.z = (movementInput != Vector3.zero ? multiplier.z * curveCos * movementInput.x : 0);
    }

    void CompositePositionRotation()
    {
        // position
        transform.localPosition =
            Vector3.Lerp(transform.localPosition,
            swayPos + bobPostition,
            Time.deltaTime * smooth);

        // rotation
        transform.localRotation =
            Quaternion.Slerp(transform.localRotation,
            Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobEulerRotation),
            Time.deltaTime * smoothRot);
    }
}
