using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Complex", menuName = "Attack/Complex")]
public class ComplexStyle : AttackStyle
{
    public override void Fire(Vector3 position, Vector3 forward, Quaternion rotation){
            for(int i = 0; i < 5; i++){
                GameObject fire = Instantiate(shell, position + forward, rotation);
                fire.GetComponent<Rigidbody>().velocity = (20f) * ((forward + new Vector3(0f, 0.3f, 0f)) + new Vector3(forward.z, 0.3f, -forward.x) * (2 - i )/10 );
            } 
    }
}
