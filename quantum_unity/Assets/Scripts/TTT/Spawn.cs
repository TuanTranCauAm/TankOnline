
using UnityEngine;
using System;
using System.Collections.Generic;

public class Spawn : MonoBehaviour
{
    [SerializeField] private AttackStyle[] weapon;
    private AttackStyle currentWeapon;

    [SerializeField] private float timeReload;
    private float time;

    [SerializeField] private GameObject ui;
    private GameObject curUi;
    
    void Update()
    {
        if(curUi == null)
            time -= Time.deltaTime;
        if(time <= 0 && curUi == null){
            time = timeReload;
            var random = new System.Random();
            int index = random.Next(weapon.Length);
            currentWeapon = weapon[index];
            GameObject x = Instantiate(ui, transform.position, transform.rotation);
            curUi = x;
            x.GetComponent<Item2>().weapon = currentWeapon;
        }
    }
}
