using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1 : MonoBehaviour
{
    [SerializeField] private AttackStyle weapon;
    void OnCollisionEnter(Collision ob){
        Destroy(gameObject);
        ob.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Attack>().ChangeAttaclStyle(weapon);
    }
}
