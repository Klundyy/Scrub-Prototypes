using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseBounce : MonoBehaviour
{
    public Vector2 speed = new Vector2(2f, 2f);
    private Vector2 direction = Vector2.one.normalized; // Diagonal direction
    public Rect bounds; // The green square area
    private Vector2 horseSize; // Size of the horse sprite

    void Start()
    {
        // Calculate the size of the horse based on the sprite renderer
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        horseSize = sr.bounds.extents; // Half width and height of the sprite
    }

    void Update()
    {
        Vector3 newPosition = transform.position + (Vector3)(direction * speed * Time.deltaTime);

        // Check bounds and reverse direction if outside
        if (newPosition.x - horseSize.x < bounds.xMin || newPosition.x + horseSize.x > bounds.xMax)
        {
            direction.x *= -1;
        }

        if (newPosition.y - horseSize.y < bounds.yMin || newPosition.y + horseSize.y > bounds.yMax)
        {
            direction.y *= -1;
        }

        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}
