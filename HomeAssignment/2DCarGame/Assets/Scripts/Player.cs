using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] float health = 50f;
    [SerializeField] float moveSpeed = 10f;

    // Variables for border around camera
    float xMin, xMax, yMin, yMax;
    float padding = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = transform.position.x + deltaX;
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);

        transform.position = new Vector2(newXPos, transform.position.y);
    }

    //reduces health whenever player collides with a gameObject which has DamageDealer component
    private void OnTriggerEnter2D(Collider2D other)
    {
        //access the Damage Dealer from the "other" object which hit the player
        //and depending on the laser damage reduce health

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        // Subtracts damage from health
        health -= damageDealer.GetDamage();

        // Destroys the player object if health reaches 0
        if (health <= 0)
            Destroy(gameObject);
    }
}
