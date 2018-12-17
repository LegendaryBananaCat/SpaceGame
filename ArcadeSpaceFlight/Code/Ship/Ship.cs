using UnityEngine;

/// <summary>
/// Ties all the primary ship components together.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ShipPhysics))]
[RequireComponent(typeof(ShipInput))]
public class Ship : MonoBehaviour
{    
    public bool isPlayer = false;

    private ShipInput input;
    private ShipPhysics physics;

    //
    public enum Faction { Friend, Foe, Neutral };

    public Faction faction;

    public float maxHealth = 100;

    [Header("Lasers (pew pew pew)")]
    public GameObject shotPrefab;
    public float      shotDamage;
    public float      shotSpeed;
    public float      shotDuration;

    float health;

    public GameObject explosionPrefab;


    //


    // Keep a static reference for whether or not this is the player ship. It can be used
    // by various gameplay mechanics. Returns the player ship if possible, otherwise null.
    public static Ship PlayerShip { get { return playerShip; } }
    private static Ship playerShip;

    // Getters for external objects to reference things like input.
    public bool UsingMouseInput { get { return input.useMouseInput; } }
    public Vector3 Velocity { get { return physics.Rigidbody.velocity; } }
    public float Throttle { get { return input.throttle; } }


    private void Awake()
    {
        input = GetComponent<ShipInput>();
        physics = GetComponent<ShipPhysics>();
    }

    protected virtual void Update()
    {
        // Pass the input to the physics to move the ship.
        physics.SetPhysicsInput(new Vector3(input.strafe, 0.0f, input.throttle), new Vector3(input.pitch, input.yaw, input.roll));

        // If this is the player ship, then set the static reference. If more than one ship
        // is set to player, then whatever happens to be the last ship to be updated will be
        // considered the player. Don't let this happen.
        if (isPlayer)
            playerShip = this;
    }


    //
    void Start()
    {
        health = maxHealth;
    }

    protected void Shoot(Vector3 direction)
    {
        GameObject playerShot = Instantiate(shotPrefab, transform.position, Quaternion.LookRotation(direction, Vector3.up));
        Shot shot = playerShot.GetComponent<Shot>();
        if (shot != null)
        {
            shot.damage = shotDamage;
            shot.duration = shotDuration;
            shot.speed = shotSpeed;
            shot.faction = faction;
        }
        else
            Debug.LogWarning("Shot missing from shot prefab");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            Die();
            Debug.Log("Go to 'Dead#4'!");
        }
        /*else if ((other.tag == "Player1") || (other.tag == "Death"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Debug.Log("Dead!#2");
        }*/
    }

    protected void Die()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        Debug.Log("Dead#4!");
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            Die();
            Debug.Log("Dead!#5");
        }
    }
    ///
    
}
