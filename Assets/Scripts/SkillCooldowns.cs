using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldowns : MonoBehaviour {

   
    public float Energy = 100.0f;
    public float[] skillManaCosts = new float[3];
    public GameObject StunObject;

    [Header("Run Skill Parameters")]
    public bool IsRunning = false;
    public float RunDuration;
    private float RunActivationTime;

    private bool takedownActive = false;

   public List<Skill> skills;

    void Start()
    {
        

        skillManaCosts[0] = 40;
        skillManaCosts[1] = 20;
        skillManaCosts[2] = 40;
        //skillManaCosts[3] = 50;
    }
  

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (skills[0].currentcooldown >= skills[0].cooldown && Energy > skillManaCosts[0] )
            {
                skills[0].currentcooldown = 0;
                Energy -= skillManaCosts[0];
                Vector3 v3_SpawnLoc = new Vector3(transform.position.x, 1, transform.position.z);
                Instantiate(StunObject, v3_SpawnLoc, transform.rotation);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (skills[1].currentcooldown >= skills[1].cooldown && Energy > skillManaCosts[1])
            {
                RunActivationTime = Time.time;
                Energy -= skillManaCosts[1];
                skills[1].currentcooldown = 0;
                GetComponent<Player8DMovement>().fMaxSpeed = 8f;
                IsRunning = true;
                
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && !takedownActive)
        {
            if (skills[2].currentcooldown >= skills[2].cooldown && Energy > skillManaCosts[2])
            {
                Debug.Log("ActivatedTakedown");
                takedownActive = true;


            }
        }
    }


    // Update is called once per frame
    void Update()
    {

        foreach (Skill s in skills)
        {
            if (s.currentcooldown < s.cooldown)
            {
                s.currentcooldown += Time.deltaTime;
                s.skillcon.fillAmount = s.currentcooldown / s.cooldown;

            }
        }

        if (Time.time > RunActivationTime + RunDuration && IsRunning)
        {
            IsRunning = false;
            GetComponent<Player8DMovement>().fMaxSpeed = 5f;
        }
        if (takedownActive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log("DeactivatedTakedown");
                takedownActive = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit TakedownHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out TakedownHit))
                {
                    if (TakedownHit.transform.CompareTag("Enemy"))
                    {
                        Debug.Log("Clicked On ENEMY");

                        RaycastHit PlayerRayHit;
                        Vector3 v3;
                        v3 = TakedownHit.transform.position - transform.position;
                        if (Physics.Raycast(transform.position, v3, out PlayerRayHit))
                        {
                            Debug.Log(PlayerRayHit.transform.tag);
                            if (PlayerRayHit.transform.CompareTag("Enemy"))
                            {
                                Debug.Log("Enemy in Line of Sight");
                                if (PlayerRayHit.distance <= 5)
                                {
                                    Debug.Log("Enemy in Distance");
                                    if (true)
                                    {
                                        Debug.Log("Takedown Successful");
                                        Destroy(PlayerRayHit.transform.gameObject, 3);
                                        Energy -= skillManaCosts[2];
                                        skills[2].currentcooldown = 0;
                                        takedownActive = false;
                                    }
                                    else
                                    {
                                        takedownActive = false; Debug.Log("DeactivatedTakedown");
                                    }
                                }
                                else
                                {
                                    takedownActive = false; Debug.Log("DeactivatedTakedown");
                                }
                            }
                            else
                            {
                                takedownActive = false; Debug.Log("DeactivatedTakedown");
                            }

                        }
                        else
                        {
                            takedownActive = false; Debug.Log("DeactivatedTakedown");
                        }
                    }
                    else
                    {
                        takedownActive = false; Debug.Log("DeactivatedTakedown");
                    }
                }
            }
        }

    }

    [System.Serializable]
    public class Skill
    {
        public float cooldown;
        public Image skillcon;
        [HideInInspector]
        public float currentcooldown;
    }
}
