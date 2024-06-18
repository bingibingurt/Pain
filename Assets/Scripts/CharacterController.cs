using UnityEngine;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;

public class CharacterController : MonoBehaviour
{
    #region variables

    private float movingSpeed = 6f;
    [Header("Main Values For Moving")]
    [SerializeField] private float defaultSpeed = 6f;
    [SerializeField] private float sneakingSpeed = 6f;
    [SerializeField] private float sprintingSpeed = 6f;
    
    [SerializeField] private float jumpingForce = 6f;
    
    private Vector2 inputVector;
    private Rigidbody rb;
    
    //things for the groundcheck
    [Header("Ground Check")]
    [SerializeField] private Transform transformRayStart;
    [SerializeField] private float rayLength = 0.5f;
    [SerializeField] private LayerMask layerGroundCheck;
    
    //things for slopecheck
    [Header("Slope Check")]
    [SerializeField] private float raySlopeLength = 0.1f;
    [SerializeField] private float maxAngleSlope = 20f;
    private bool sliding = false;

    //things for camera control
    [Header("Camera Controls")]
    [SerializeField] private Transform transformCamerFollow;
    [SerializeField] private float rotateSensivity = 1f;
    private float cameraPitch = 0f;
    private float cameraRoll = 0f;
    [SerializeField] private float maxCameraPitch = 60f;
    [SerializeField] private bool invertCameraPitch = false;
    
    //things for character rotation
    [Header("Character Rotation")]
    [SerializeField] private Transform characterBody;

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        //get the current Rigitbody on the object
        rb = GetComponent<Rigidbody>();
        //freeze it rotation so it does not goes wonky
        rb.freezeRotation = true;
        
        movingSpeed = defaultSpeed;

        //lock the Cursor in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //rotating the camera with the mouse
        //get the mousemovment since the last frame
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        
        //apply the camera pitch change
        if (invertCameraPitch)
        {
            cameraPitch = cameraPitch - mouseDelta.y * rotateSensivity;
        }
        else
        {
            cameraPitch = cameraPitch + mouseDelta.y * rotateSensivity;
        }
        
        //limit the camera pitch that it does not turn up side down
        cameraPitch = Mathf.Clamp(cameraPitch, -maxCameraPitch, maxCameraPitch);
        
        //apply the camera roll change
        cameraRoll = cameraRoll + mouseDelta.x * rotateSensivity;

        //write the values to the transform of the follow object
        transformCamerFollow.localEulerAngles = new Vector3(cameraPitch, cameraRoll, 0);
    }

    //handle movement here
    private void FixedUpdate()
    {
        //check if the character walkes on a slope
        if (SlopeCheck())
        {
            Vector3 movementDirection = new Vector3(inputVector.x * movingSpeed, rb.velocity.y,
                inputVector.y * movingSpeed);

            //rotating the movement to align to the camera facing
            movementDirection =
                Quaternion.AngleAxis(transformCamerFollow.eulerAngles.y, Vector3.up) * movementDirection;
            
            //apply movement to the character
            rb.velocity = movementDirection;

            //check if there is a movement
            if (movementDirection != Vector3.zero)
            {
                Vector3 lookdirection = movementDirection;
                lookdirection.y = 0f;
                //rotate the visual character to the movement direction
                characterBody.rotation = Quaternion.LookRotation(lookdirection);
            }
        }
    }

    void OnMove(InputValue inputValue)
    {
        //Debug.Log(inputValue.Get<Vector2>().ToString());
        inputVector = inputValue.Get<Vector2>();
    }

    void OnJump()
    {
        Debug.Log("JUMP!");
        //check if we are on ground
        if (GroundCheck())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpingForce, rb.velocity.z);
        }
    }

    void OnSneak(InputValue inputVal)
    {
        float isSneaking = inputVal.Get<float>();
        //Debug.Log(isSneaking);

        //round the float number (Mathf.RoundToInt) to the nearest integer, so that a direct comparison (==) is possible 
        if (Mathf.RoundToInt(isSneaking) == 1)
        {
            //if key is pressed, set moving speed to sneakingSpeed
            movingSpeed = sneakingSpeed;
        }
        else
        {
            //if the key is released, reset the moving speed to default
            movingSpeed = defaultSpeed;
        }
    }
    
    void OnSprint(InputValue inputVal)
    {
        float isSprinting = inputVal.Get<float>();
        //Debug.Log(isSprinting);
        
        //round the float number (Mathf.RoundToInt) to the nearest integer, so that a direct comparison (==) is possible 
        if (Mathf.RoundToInt(isSprinting) == 1)
        {
            //if key is pressed, set moving speed to sprintingSpeed
            movingSpeed = sprintingSpeed;
        }
        else
        {
            //if the key is released, reset the moving speed to default
            movingSpeed = defaultSpeed;
        }
    }

    bool GroundCheck()
    {
        //check if there is ground under the character
        return Physics.Raycast(transformRayStart.position, Vector3.down,
            rayLength, layerGroundCheck);
    }
    
    bool SlopeCheck()
    {
        RaycastHit hit;
        
        //show a ray in the editor
        Debug.DrawRay(transformRayStart.position, Vector3.down * raySlopeLength, Color.red,0.1f);
        
        //make a ray and write the collider it hits in the variable 'hit'
        Physics.Raycast(transformRayStart.position, Vector3.down, out hit,
            raySlopeLength, layerGroundCheck);
        
        //check if it hit anything
        if (hit.collider != null)
        {
            //calculate the angle from the hit object in relation to the character
            float angle = Vector3.Angle(Vector3.up, hit.normal);
            //Debug.Log(angle);
            if (angle > maxAngleSlope)
            {
                return false;
            }
        }
        
        return true;
    }
}
