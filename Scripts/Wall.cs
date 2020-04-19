using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

	protected ContactFilter2D contactFilter;
	public float life;
    // Start is called before the first frame update
    void Start()
    {
    	contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    	contactFilter.useLayerMask = true;
        life = 1f;
    }

    void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Zombies"){
				life -= 0.2f;
				if (life <= 0){
					Destroy(this.gameObject);
				}
		}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
