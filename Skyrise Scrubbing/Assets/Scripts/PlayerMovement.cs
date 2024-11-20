using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject platform;
    public GameObject leftRope;
    public GameObject rightRope;
    public float baseSpeed = 4f;
    private float playerAngle = 0f;
    private float leftPull = 0f;
    private float rightPull = 0f;
    public float pullSpeed = 0.4f;
    public float fallCoefficient = 1f;
    public float maxAngleDeg = 30f;
    public float pullDecay = 1.5f;
    private float ropeStartingHeight;
    private float ropeHorizontalOffset;
    private bool asleep = true;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        ropeStartingHeight = (leftRope.transform.localPosition.y + rightRope.transform.localPosition.y) / 2;
        ropeHorizontalOffset = leftRope.transform.localPosition.x;
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(asleep && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            asleep = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
           rightPull += pullSpeed * Time.deltaTime;
        }
        else
        {
            rightPull -= pullSpeed * Time.deltaTime * pullDecay;
        }
        leftPull = Mathf.Clamp(leftPull, 0f, 1f);
        if (Input.GetKey(KeyCode.D))
        {
            
            leftPull += pullSpeed * Time.deltaTime;
        }
        else
        {
            
            leftPull -= pullSpeed * Time.deltaTime * pullDecay;
        }
        rightPull = Mathf.Clamp(rightPull, 0f, 1f);
        playerAngle = maxAngleDeg * (rightPull - leftPull);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (asleep)
        {
            return;
        }
        rb.velocity += new Vector2(0, -baseSpeed * fallCoefficient) * Time.deltaTime;
        rb.velocity += new Vector2(0, baseSpeed * (leftPull + rightPull)) * Time.deltaTime;
        float sin = Mathf.Sin(Mathf.Deg2Rad * playerAngle);
        float ropeHeighOffset = ropeHorizontalOffset * sin;
        rightRope.transform.localPosition = new Vector3(-ropeHorizontalOffset, ropeStartingHeight - ropeHeighOffset, 0);
        leftRope.transform.localPosition = new Vector3(ropeHorizontalOffset, ropeStartingHeight + ropeHeighOffset, 0);
        platform.transform.localRotation = Quaternion.Euler(0, 0, playerAngle);

        //contentious line
        rb.velocity += new Vector2(-sin * baseSpeed, 0) * Time.deltaTime;

    }
}
