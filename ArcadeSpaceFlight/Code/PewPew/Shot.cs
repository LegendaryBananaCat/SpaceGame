using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject explosionPrefab;


    [HideInInspector] public float          damage;
    [HideInInspector] public float          speed;
    [HideInInspector] public float          duration;
    [HideInInspector] public Ship.Faction   faction;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pewpew")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Debug.Log("Dead!");
        }
        else if ((other.tag == "Player1") || (other.tag == "PewPew"))
        {
            Ship ship = other.GetComponent<Ship>();
            if (ship != null && ship.faction != faction)
            {
                ship.DealDamage(damage);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Debug.Log("Dead!#2");
            }
        }

    }
}