using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New High", menuName = "Attack/High")]
public class HighStyle : AttackStyle
{
    public override void Fire(Vector3 position, Vector3 forward, Quaternion rotation){
            GameObject fireeeee = Instantiate(shell, position + forward, rotation);
            fireeeee.GetComponent<Rigidbody>().velocity = (4f) * (forward + new Vector3(0f, 4f, 0f));
    }
}

