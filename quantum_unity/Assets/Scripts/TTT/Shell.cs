using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private ParticleSystem explored;
    void OnCollisionEnter(Collision ob){
        if(ob.gameObject.CompareTag("Player"))
        return;
        ParticleSystem x = Instantiate(explored, gameObject.transform.position, gameObject.transform.rotation);
        x.Play();
        StartCoroutine(End());
    }
    IEnumerator End(){
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}
