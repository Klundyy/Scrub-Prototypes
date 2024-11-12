using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //eventually change controls
        if(Input.GetKey(KeyCode.W)) {
            this.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        if(Input.GetKey(KeyCode.A)) {
            this.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if(Input.GetKey(KeyCode.S)) {
            this.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if(Input.GetKey(KeyCode.D)) {
            this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }
}
