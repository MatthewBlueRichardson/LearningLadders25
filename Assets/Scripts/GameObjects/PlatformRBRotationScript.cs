using UnityEngine;

public class PlayerRBRotationScript : MonoBehaviour
{
    public float maxTiltAngle = 30f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Get mouse X relative to screen centre
        float screenMid = Screen.width / 2f;
        float mouseX = Input.mousePosition.x;
        //Clamps based on screen width
        float normalisedX = (mouseX - screenMid) / screenMid;
        //Clamps between -1 and 1
        normalisedX = Mathf.Clamp(normalisedX, -1f, 1f);

        float targetAngle = -normalisedX * maxTiltAngle;

        rb.MoveRotation(targetAngle);
    }

}
