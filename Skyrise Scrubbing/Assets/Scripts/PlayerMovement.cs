using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public GameObject platform;
    public GameObject leftRope;
    public GameObject rightRope;
    private Vector2 velocity = Vector2.zero;
    public float baseSpeed = 4f;
    private float playerAngle = 0f;
    private float leftPull = 0f;
    private float rightPull = 0f;
    public float pullSpeed = 0.4f;
    public float fallCoefficient = 1f;
    public float maxAngleDeg = 30f;
    private float ropeStartingHeight;
    private float ropeHorizontalOffset;
    private float playerRadius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        ropeStartingHeight = (leftRope.transform.localPosition.y + rightRope.transform.localPosition.y) / 2;
        ropeHorizontalOffset = leftRope.transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        velocity += new Vector2(0, -baseSpeed) * Time.deltaTime;
        //eventually change controls
        if(Input.GetKey(KeyCode.A)) {
            leftPull += pullSpeed * Time.deltaTime;
        } else {
            leftPull -= pullSpeed * Time.deltaTime * fallCoefficient;
        }
        leftPull = Mathf.Clamp(leftPull, 0f, 1f);
        if(Input.GetKey(KeyCode.D)) {
            rightPull += pullSpeed * Time.deltaTime;
        } else {
            rightPull -= pullSpeed * Time.deltaTime * fallCoefficient;
        }
        rightPull = Mathf.Clamp(rightPull, 0f, 1f);
        playerAngle = maxAngleDeg * (rightPull-leftPull);
        velocity += new Vector2(0, baseSpeed * (leftPull + rightPull)) * Time.deltaTime;
        float sin = Mathf.Sin(Mathf.Deg2Rad * playerAngle);
        float ropeHeighOffset = ropeHorizontalOffset * sin;
        rightRope.transform.localPosition = new Vector3(-ropeHorizontalOffset, ropeStartingHeight - ropeHeighOffset, 0);
        leftRope.transform.localPosition = new Vector3(ropeHorizontalOffset, ropeStartingHeight + ropeHeighOffset, 0);
        platform.transform.localRotation = Quaternion.Euler(0, 0, playerAngle);

        //contentious line
        velocity += new Vector2(sin * baseSpeed, 0) * Time.deltaTime;

        this.transform.Translate(velocity * Time.deltaTime);
        
        //collide with edges of screen
        
    }
}
