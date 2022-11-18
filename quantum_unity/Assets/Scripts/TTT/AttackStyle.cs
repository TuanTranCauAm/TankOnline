using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Normal", menuName = "Attack/Normal")]
public class AttackStyle : ScriptableObject
{
    [SerializeField] protected GameObject shell;
    [SerializeField] protected float timeReload;
    [SerializeField] protected AttackActivity activy;
    public float interval = 0;
    public float bulletAmount =10;
    
    public void Getup(){
        activy = new AttackActivity();
        activy.shell = shell;
    }

    public virtual void Fire(Vector3 position, Vector3 forward, Quaternion rotation){
            GameObject bullet = Instantiate(shell, position, Quaternion.Euler(forward));
            var rig = bullet.GetComponent<Rigidbody>();
            rig.velocity = 20f * forward;
            bullet.transform.forward = forward;
    }
}
