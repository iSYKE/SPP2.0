using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
   
    public int attackDamage = 10;

    GameObject player;
    //PlayerHealth playerHealth;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        //playerHealth = player.GetComponent <PlayerHealth> ();
    }

    void Update ()
    {       
            //Attack ();
    }


//    void Attack ()
//    {
//        if(playerHealth.hullCurrentHealth > 0)
//        {
//			if(Input.GetKeyDown(KeyCode.Space))  
//			{
//            	playerHealth.TakeDamage (attackDamage);
//			}
//        }
//    }
}
