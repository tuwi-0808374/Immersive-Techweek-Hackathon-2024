using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GrowPlant : MonoBehaviour
{

    public Animator animator;

    bool isGrowing = false;
    bool hasSeed = false;

    public UnityEvent seedInSoul;

    public void GrowSeed()
    {
        if (!isGrowing)
        {
            isGrowing = true;
            StartCoroutine(Growing());
            
        }
    }

    IEnumerator Growing()
    {
        animator.Play("grow");
        yield return new WaitForSeconds(.05f);
        animator.StopPlayback();
        isGrowing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.tag + " collision");    
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " trigger");
        if (other.tag == "Seed" && !isGrowing)
        {
            //GrowSeed();
            hasSeed = true;
            seedInSoul.Invoke();
            Destroy(other.gameObject, .1f);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.tag + " trigger");
        GrowSeed();
    }

}
