using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 10f;
    public float walkSpeed = 6f;
    public float gravity = -10f;
    public float jumpHeight = 5f;
    public float groundRayDistance = 1.1f;

    private CharacterController controller; // Reference to character controller
    private Vector3 motion;// is the movement offset per frame
    public bool isJumping = false;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        bool inputRun = Input.GetKey(KeyCode.LeftShift);
        bool inputJump = Input.GetButtonDown("Jump");
        //Put horizontal & vertical input into vector
        Vector3 inputDir = new Vector3(inputH, 0f, inputV);
        // Convert local direction to world space direction(relative to Player)
        inputDir = transform.TransformDirection(inputDir);
        //if input exceeds length of 1
        if (inputDir.magnitude > 1f)
        {
            //Normalize it to 1f!
            inputDir.Normalize();
        }
        if (inputRun)
        {
            Run(inputDir.x, inputDir.z);
            print("Motion :X" + motion.x + "Motion Z:" + motion.z);//Show RunSpeed
        }
        else
        {
            Walk(inputDir.x, inputDir.z);
            print("Motion :X" + motion.x + "Motion Z:" + motion.z);//Show WalkSpeed
        }
       
        if(IsGrounded())
        {
            //if IS grounded AND press "Jump"
            if (IsGrounded()&& inputJump)
            {
                Jump(); //Make player jump
            }
            //if is NOT Grounded AND isJumping
            if(!IsGrounded()&&isJumping)
            {
                //Set jumping is false(so cant jump)
                isJumping = false;
            }
        }
        motion.y += gravity * Time.deltaTime; 

        controller.Move(motion * Time.deltaTime);
    }
    void Move(float inputH, float inputV,float speed)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);
        // Convert local direction to world space direction(relative to Player)
        //direction = transform.TransformDirection(direction);

        motion.x = direction.x * speed;
        motion.z = direction.z * speed;
    }
    public void Walk(float inputH,float inputV)
    {
        Move(inputH, inputV, walkSpeed);
    }
    public void Run(float inputH, float inputV)
    {
        Move(inputH, inputV, runSpeed);
    }
    public void Jump()
    {
        motion.y = jumpHeight;
        isJumping = true;//We are jumping
    }
    bool IsGrounded()
    {
        //Raycast below the player
        Ray groundRay = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        //If hitting something
        if (Physics.Raycast(groundRay, out hit, groundRayDistance))
        {
            return true;
        }
        return false;
        
        
    }
}
