using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTimeComp : MonoBehaviour
{
    public float TimeToDestroy = 3;
    private void Update()
    {
        if (TimeToDestroy > 0)
        {
            TimeToDestroy -= Time.deltaTime;
            if (TimeToDestroy <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
