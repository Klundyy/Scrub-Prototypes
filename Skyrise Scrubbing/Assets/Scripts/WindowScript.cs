using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D myCollider;
    [SerializeField] public GameObject player;
    private Collider2D playerCollider;
    [SerializeField] private Color dirty = new(0, 255, 120);
    [SerializeField] private Color clean = new(0, 255, 255);
    [SerializeField] private GameObject[] shines;
    readonly private float[] shineStartYs = new float[2];
    private float alpha;
    private float timeScale = 2f;
    private float chanceDirty = 0.7f;
    private bool doShine = false;
    private float shineProgress = 0f;
    private float shineTime = 0.8f;
    private float shinePathLength = 3.4f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        myCollider = this.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponentInChildren<Collider2D>();
        if(Random.Range(0f,1f) > chanceDirty) {
            alpha = 1f;
            spriteRenderer.color = clean;
        } else {
            alpha = 0f;
            spriteRenderer.color = dirty;
        }
        for (int i = 0; i < shines.Length; i++) {
            GameObject shine = shines[i];
            shineStartYs[i] = shine.transform.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //replace with window cleaning condition
        if(Input.GetMouseButton(0) && myCollider.bounds.Intersects(playerCollider.bounds) && alpha < 1f) {
            alpha = Mathf.Clamp(alpha + Time.deltaTime/timeScale, 0, 1);
            if(alpha == 1f) {
                doShine = true;
            }
            Debug.Log("" + alpha);
        }
        spriteRenderer.color = Color.Lerp(dirty, clean, alpha);
        if(doShine) {
            shineProgress += Time.deltaTime/shineTime;
            Shine(shineProgress); 
        }
    }

    //I hate it here
    private void Shine(float alpha) {
        if (alpha < 1) {
            for (int i = 0; i < shines.Length; i++) {
                GameObject shine = shines[i];
                shine.transform.position = new Vector3(shine.transform.position.x, shineStartYs[i] - alpha * shinePathLength, shine.transform.position.z);
            }
        } else {
            for (int i = 0; i < shines.Length; i++) {
                GameObject shine = shines[i];
                shine.transform.position = new Vector3(shine.transform.position.x, shineStartYs[i] - shinePathLength, shine.transform.position.z);
            }
            doShine = false;
        }
    }
}
