using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class z_spawn : MonoBehaviour
{
	public static int znb;
	public GameObject toSpawn;
	public float timeto;
	public Vector3[] posToSpawn = new Vector3[6]; 
    // Start is called before the first frame update
    void Start()
    {
    	znb = 0;
    	timeto = 9f;
    	StartCoroutine(Spawn());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn(){
    	int i;
    	while(true){
    		if (znb < 30){
    		   	i = Random.Range(0,3);
    		    Instantiate(toSpawn, posToSpawn[i], Quaternion.identity);
    	   	    znb += 1;
    	   	}
		    yield return new WaitForSecondsRealtime(timeto);
   		   	if(timeto > 1f){
   		   	    timeto = timeto * 0.9f;
    		}
    	}
    }
} 
