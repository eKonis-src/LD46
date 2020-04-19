using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
	public events eventhandller;
	protected ContactFilter2D contactFilter;
	public bool canMove;
	public bool canBeHurt;
	public float speed;
	public float life;

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public List<Transform> visibleTargets = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {	
    	eventhandller = GameObject.FindWithTag("Events").GetComponent<events>();
    	canMove = true;
    	contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    	contactFilter.useLayerMask = true;
    	InvokeRepeating("canHurt", 0f, 2f);
     	life = 1f;   
		StartCoroutine("FindTargetWithDelay", .005f);
	}

	IEnumerator FindTargetWithDelay(float delay){
		while(true){
			yield return new WaitForSeconds (delay);
			findVisibleTargets();
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag != "PrefabWall"){
			Vector3 ndir = new Vector3(other.transform.position.x,other.transform.position.y, 0);
			moveTo(knokbackDir(transform.position, ndir), speed*2);
		}
	}
	IEnumerator OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag != "PrefabWall"){
			canMove = false;
			if (other.gameObject.tag == "Enemy"){
				if(canBeHurt){
					life -= 0.25f;
					canBeHurt = false;
					if (life == 0){
						z_spawn.znb -= 1;
						eventhandller.nbzbmort += 1;
						Destroy(this.gameObject);
					}
				}
			}
			Vector3 ndir = new Vector3(other.transform.position.x,other.transform.position.y, 0);
			if (other.gameObject.tag == "Bullet"){
				moveTo(knokbackDir(transform.position, ndir), speed*10);	
				yield return new WaitForSeconds(1f);
			}
			else{
				moveTo(knokbackDir(transform.position, ndir), speed*3);
				yield return new WaitForSeconds(0.5f);
			}
			canMove = true;
		}
	}

	void canHurt(){
		if (!canBeHurt){
			canBeHurt = true;
		}
	}
    // Update is called once per frame
    void Update()
    {

    }

 	void findVisibleTargets(){
		visibleTargets.Clear();
		Collider2D[] targetInViewRadius = Physics2D.OverlapCircleAll (transform.position, viewRadius, targetMask);

		for(int i =0; i < targetInViewRadius.Length; i++){
			Transform target = targetInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) <viewAngle/2){
				float dstToTarget = Vector3.Distance(transform.position, target.position);

				if(!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)){
					visibleTargets.Add(target);
					Vector2 direction = new Vector2(target.position.x - transform.position.x,
    					target.position.y - transform.position.y);
    				transform.up = direction;  
					if (canMove){
						moveTo(target.position, speed);
					}
				}
			}
		}
	}
	Vector3 knokbackDir(Vector3 obj, Vector3 obstacle){
		Vector3 newDir;
		float x;
		float y;
		x = obj.x + ( obj.x - obstacle.x)*3;
		y = obj.y + (obj.y - obstacle.y)*3;
		newDir = new Vector3(x, y, 0);
		return newDir;
	}
	void moveTo(Vector3 enemy, float speed){
		float x;
		float y;
		Vector3 dir;
		Vector3 pos = transform.position;
		if ((enemy.x - pos.x) >= 0){
			x = speed;
		}

		else {
			x = -speed;
		}

		if ((enemy.y - pos.y) >= 0){
			y = speed;
		}

		else{
			y = -speed;
		}
  
		dir = new Vector3(enemy.x - pos.x ,enemy.y - pos.y ,0);
		dir = dir.normalized;
		transform.position += dir *speed * Time.deltaTime;
	}
}
