using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public float damage = 1f;
    public bool isPenetrate = false;
    public bool isPlayer = false;
    public Vector2 velocity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.name.Contains("Enemy")){
            if(gameObject.name.Contains("Normal")){
                TweakNormalVelocity();
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPenetrate)
        {
            Destroy(gameObject);
        }

    }

    void TweakNormalVelocity(){
        Vector3 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        velocity.x += Random.Range(-0.2f, 0.2f);
        velocity.y += Random.Range(-0.1f, 0.1f);
        gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
