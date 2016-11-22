using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;


    public Camera mainCamera;
    public Transform cameraTransform;
    public MotionBlur motionBlur;
    public AudioSource wind;

    public float rotateSpeedV, rotateSpeedH, moveSpeed, minTurboSpeed, maxTurboSpeed;

    public float minX, maxX, minY, maxY, minZ, maxZ;

    private float yaw, pitch;
    private float targetFOV = 70;
    private float targetCameraDistance;
    private float cameraDistance;

    private float cameraDistanceNear = 5;
    private float cameraDistanceFar = 10;

    private float targetBlurAmount;
    private float targetWindVolume;

    void Start()
    {
        instance = this;
        yaw = transform.rotation.y;
        pitch = transform.rotation.x;
        cameraDistance = targetCameraDistance = cameraDistanceNear;
    }

    void FixedUpdate()
    {
        Vector3 cameraPosition = cameraTransform.position;

        cameraDistance = Mathf.Lerp(cameraDistance, targetCameraDistance, 0.01f);
        cameraPosition = Vector3.Lerp(cameraPosition, transform.position - cameraDistance * transform.forward, 0.1f);

        cameraTransform.position = cameraPosition;

        cameraTransform.forward = Vector3.Slerp(cameraTransform.forward, transform.forward, 0.1f);

        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, 0.02f);

        motionBlur.blurAmount = Mathf.Lerp(motionBlur.blurAmount, targetBlurAmount, 0.04f);

        wind.volume = Mathf.Lerp(wind.volume, targetWindVolume, 0.01f);
    }

    void Update()
    {
        if (GameState.IsPlaying)
        {
            pitch += Input.GetAxis("Vertical") * rotateSpeedV * Time.deltaTime;
            yaw += Input.GetAxis("Horizontal") * rotateSpeedH * Time.deltaTime;

            pitch = Mathf.Clamp(pitch, -7, 7);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0);

            Vector3 position = transform.position;
            position += transform.forward * (Input.GetKey(KeyCode.X) ? Mathf.Lerp(minTurboSpeed, maxTurboSpeed, GameState.FlockPercent) : moveSpeed) * Time.deltaTime;

            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
            position.z = Mathf.Clamp(position.z, minZ, maxZ);

            transform.position = position;

            if (Input.GetKey(KeyCode.X) && !GameState.DoNotSpeedUp)
            {
                targetFOV = 140;
                targetCameraDistance = Mathf.Lerp(10, 35, GameState.FlockPercent);
                targetBlurAmount = 0.8f;
                targetWindVolume = 0.1f;
            }
            else
            {
                targetFOV = 70;
                targetCameraDistance = Mathf.Lerp(5, 10, GameState.FlockPercent);
                targetBlurAmount = 0;
                targetWindVolume = 0;
            }
        }
    }
}
