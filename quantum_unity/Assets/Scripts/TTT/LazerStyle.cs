using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lazer", menuName = "Attack/Lazer")]
public class LazerStyle : AttackStyle
{
    [SerializeField]
    private Lazer lazer;
    public override void Fire(Vector3 position, Vector3 forward, Quaternion rotation){
            if(lazer){
                GameObject x = GameObject.Instantiate(lazer.gameObject);
                x.transform.position = position + forward;
                x.transform.forward = forward;
            }
    }
}
