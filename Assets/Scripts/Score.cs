using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    int number;

    public Sprite[] numbers;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sprite=numbers[number];
    }

    public bool SetNumber(int n){
        if (number < 10)
        {
            number = n;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(1,1,1));
    }

}
