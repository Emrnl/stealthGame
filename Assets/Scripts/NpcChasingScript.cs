using UnityEngine;
using System.Collections;

public class NpcChasingScript : MonoBehaviour {

	public Transform player;
	public Transform head;
	static Animator anim;
	bool pursuing = false;
	public float distanceToPlayer = 3f;


	public enum AIState
	{
		OnPatrol,
		OnPursuing,
		OnAttack,
		OnIdle

	}

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit hit;
		Ray headRay = new Ray (transform.position, head.forward); // headten ray atıyorum

		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
		
		float angle = Vector3.Angle(direction, head.forward);


		if(Vector3.Distance(player.position, this.transform.position) < 10 && (angle < 30 || pursuing) )  // eğer görebiliyorsam
		{
			if (Physics.Raycast (headRay, out hit, distanceToPlayer)) {  // şurayı bi kontrol et tekrar düzgün çalışmıyo
				if (hit.collider.tag == "Player") {
                    pursuing = true;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                        Quaternion.LookRotation(direction), 0.1f);
                }
				
			
			}
			anim.SetBool("isIdle",false);
			if(direction.magnitude > 5)
			{
				this.transform.Translate(0,0,0.05f);
				anim.SetBool("isWalking",true);
				anim.SetBool("isAttacking",false);
			}
			else 
			{
				anim.SetBool("isAttacking",true);
				anim.SetBool("isWalking",false);
			}

		}
		else 
		{
			anim.SetBool("isIdle", true);
			anim.SetBool("isWalking", false);
			anim.SetBool("isAttacking", false);
			pursuing = false;
		}

	}
}
