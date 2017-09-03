using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyNormalPrefab;
    public ApplicationManager applicationManager;
    public float cubeHeight=1;
    public float cubeWidth=1;

	float xmin, xmax, ymin, ymax;
	float padding = 0.5f;
    public float moveSpeed = 0.25f;
    bool movingRight;
    float spawnDelay = 2f;
    float interval;

	// Use this for initialization
	void Start () {float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftDownCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightUpCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        xmin = leftDownCorner.x + padding;
        xmax = rightUpCorner.x - padding;
        ymin = leftDownCorner.y + padding;
        ymax = rightUpCorner.y - padding;
		foreach (Transform t in transform)
		{
			GameObject enemy = Instantiate(enemyNormalPrefab,
											   new Vector3(t.transform.position.x,
														   t.transform.position.y,
														   t.transform.position.z + 10),
											   Quaternion.identity);
			enemy.transform.parent = t;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if(movingRight){
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }else{
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
		foreach (Transform t in transform)
		{
            if(t.transform.position.x<xmin){
                movingRight = true;
            }else if(t.transform.position.x > xmax){
                movingRight = false;
            }
		}
        interval += Time.deltaTime;
        if(interval>spawnDelay){
            Spawn();
            interval = 0;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(cubeWidth,cubeHeight));
    }

    Transform NextFreePosition(){
		foreach (Transform t in transform)
		{
			if (t.childCount == 0)
			{
				return t;

			}
		}
        return null;
    }

    bool AllMembersAreDead(){
        foreach (Transform t in transform)
        {
            if (t.childCount > 0)
            {
                return false;

            }
        }
        return true;
    }

    void Spawn(){
        Transform t = NextFreePosition();
        if (t)
        {
            GameObject enemy = Instantiate(enemyNormalPrefab,
                                               new Vector3(t.transform.position.x,
                                                           t.transform.position.y,
                                                           t.transform.position.z + 10),
                                               Quaternion.identity);
            enemy.transform.parent = t;
        }
        if(NextFreePosition()){
            Invoke("Spawn", spawnDelay);
        }
    }
}
