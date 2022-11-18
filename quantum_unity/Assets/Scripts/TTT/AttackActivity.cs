using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActivity : MonoBehaviour
{
    public GameObject shell;

    public void Fire(Vector3 position, Vector3 forward, Quaternion rotation){
            GameObject fire = Instantiate(shell, position + forward, rotation);
            fire.GetComponent<Rigidbody>().velocity = 20f * forward;
    }
}
