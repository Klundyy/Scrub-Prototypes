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
    private float alpha;
    private float timeScale = 2f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        myCollider = this.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponentInChildren<Collider2D>();
        if(Random.Range(0f,1f) > 0.5f) {
            alpha = 1f;
            spriteRenderer.color = clean;
        } else {
            alpha = 0f;
            spriteRenderer.color = dirty;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //replace with window cleaning condition
        if(Input.GetMouseButton(0) && myCollider.bounds.Intersects(playerCollider.bounds)) {
            alpha = Mathf.Clamp(alpha + Time.deltaTime/timeScale, 0, 1);
            Debug.Log("" + alpha);
        }
        spriteRenderer.color = Color.Lerp(dirty, clean, alpha);
    }
}
