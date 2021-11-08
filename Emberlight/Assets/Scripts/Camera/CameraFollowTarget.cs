using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private float movement_speed;
    // Start is called before the first frame update
    
    Vector3 movementVector = new Vector3();
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void Update()
    {
        movementVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            movementVector += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movementVector += Vector3.back;
        if (Input.GetKey(KeyCode.A))
            movementVector += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            movementVector += Vector3.right;
        movementVector.Normalize();
        
        transform.position += movement_speed *  Time.deltaTime * (Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * movementVector) ;
        
        if (Input.GetMouseButton(2))
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0), Space.World);
        }
    }
}
