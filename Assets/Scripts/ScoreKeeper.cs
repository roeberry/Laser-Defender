using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    public static int score;
    public GameObject ones;
    public GameObject tens;
    public GameObject hundreds;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(score);
        ones.GetComponent<Score>().SetNumber(score%10);
        tens.GetComponent<Score>().SetNumber(score/10%10);
        hundreds.GetComponent<Score>().SetNumber(score/100%10);
	}
}
