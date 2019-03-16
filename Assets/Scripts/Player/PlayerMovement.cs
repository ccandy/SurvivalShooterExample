using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private Vector3     movement;
    private Animator    playerAnim;
    private Rigidbody   playerRB;
    private float       floorMask;
    private float       camRayLenght = 100;
    private Camera      playerCam;


    void Awake()
    {
        floorMask   = LayerMask.GetMask("Floor");

        playerAnim  = gameObject.GetComponent<Animator>();
        playerRB    = gameObject.GetComponent<Rigidbody>();

        playerCam   = Camera.main;
        if(playerCam == null)
        {
            Debug.LogError("There is no main cam");
        }
    }


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animation(h, v);

    }


    private void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRB.MovePosition(gameObject.transform.position + movement);
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        /*if(Physics.Raycast(camRay, out floorHit, camRayLenght, floorMask))
        {

        }*/

        if (Physics.Raycast(camRay.origin, camRay.direction, out floorHit, camRayLenght))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRB.MoveRotation(newRotation);
        }
    }

    private void Animation(float h, float v)
    {
        bool walking = (h != 0f) || (v != 0f);
        //Debug.Log("is walking" + walking);
        playerAnim.SetBool("isWalking", walking);
    }
    
}
