using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : MonoBehaviour
{
    public AttackStyle weapon;
    void OnCollisionEnter(Collision ob){
        if(ob.gameObject.tag == "Player"){
            Destroy(gameObject);
            ob.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Attack>().ChangeAttaclStyle(weapon);
        }
    }
}
