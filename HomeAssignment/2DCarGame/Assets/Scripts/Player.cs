using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] int health = 50;
    [SerializeField] float moveSpeed = 10f;

    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] [Range(0, 1)] float playerDeathSoundVolume = 0.75f;
    
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] [Range(0, 1)] float gameOverSoundVolume = 0.75f;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 1f;

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

    public int GetHealth()
    {
        return health;
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
        // Access the Damage Dealer from the "other" object which hit the player
        //and depending on the obstacle damage reduce health

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);

        // Destroy the obstacle when it hits the player
        Destroy(other.gameObject);

        // Create an explosion particle
        GameObject explosion = Instantiate(deathVFX, other.transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        // Subtracts damage from health
        health -= damageDealer.GetDamage();

        FindObjectOfType<HealthDisplay>().UpdateHealth();

        // Play playerDeathSound at the Camera position using the playerDeathSoundVolume
        AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);

        // Loads the die method if health reaches 0
        if (health <= 0 && FindObjectOfType<GameSession>().GetPoints() < 100)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);

        // Create an explosion particle
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position, gameOverSoundVolume);

        // Find the object of type Level from the hierarchy and load its method LoadGameOver()
        FindObjectOfType<Level>().LoadGameOver();
    }
}
