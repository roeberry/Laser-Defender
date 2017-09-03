using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 1f;
    public float fireFrequency = 0.2f;
    public GameObject enemyLaserNormalPrefab;
    public AudioClip explosion;

    float interval;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        interval += Time.deltaTime;
        if (interval*fireFrequency >= Random.value){
            Fire();
            interval = 0;
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Hit(collision.gameObject);
	}

    void Hit(GameObject laser){
        health -= laser.GetComponent<Laser>().damage;
        if(health<=0){
            AudioSource.PlayClipAtPoint(explosion,transform.position);
            ScoreKeeper.score++;
            Destroy(gameObject);
        }
    }

    void Fire(){
        GameObject laser = Instantiate(enemyLaserNormalPrefab, transform.position, Quaternion.identity);
		laser.GetComponent<Rigidbody2D>().velocity = laser.GetComponent<Laser>().velocity;
        Debug.Log(laser.GetComponent<Laser>().velocity);
    }
}
