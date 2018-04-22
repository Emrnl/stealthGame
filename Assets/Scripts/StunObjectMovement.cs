using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunObjectMovement : MonoBehaviour {
    public float speed = 1;
	
  void Update ()
    {
        // transform.Translate(transform.forward * speed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY DOWN");
          // other.gameObject.GetComponent<EnemyAI>().Stunned();
        }
    }
}
