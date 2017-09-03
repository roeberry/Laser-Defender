using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed = 10f;

    public GameObject laserNormal;

    public AudioSource laserSound;

    public AudioClip explosion;

    public float fireRate = 0.2f;

    public static bool isDestroyed = false;

    float xmin, xmax, ymin, ymax;
    float padding = 0.5f;
    float interval = 0;


    // Use this for initialization
    void Start()
    {
        isDestroyed = false;
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftDownCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightUpCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        xmin = leftDownCorner.x + padding;
        xmax = rightUpCorner.x - padding;
        ymin = leftDownCorner.y + padding;
        ymax = rightUpCorner.y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = newPos = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            newPos.y = Mathf.Clamp(newPos.y + speed * Time.deltaTime, ymin, ymax);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            newPos.y = Mathf.Clamp(newPos.y - speed * Time.deltaTime, ymin, ymax);
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPos.x = Mathf.Clamp(newPos.x - speed * Time.deltaTime, xmin, xmax);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            newPos.x = Mathf.Clamp(newPos.x + speed * Time.deltaTime, xmin, xmax);
        }
        transform.position = newPos;

        interval += Time.deltaTime;
        if (Input.GetKey(KeyCode.Return))
        {
            if (interval > fireRate)
            {
                Fire();
                interval = 0;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(explosion, transform.position);
        Die();
    }

    void Fire(){
		GameObject laser = Instantiate(laserNormal, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = laser.GetComponent<Laser>().velocity;
        laserSound.Play();
    }

    void Die(){
        SceneManager.LoadScene("Lose");
		gameObject.SetActive(false);
		isDestroyed = true;
    }
}
