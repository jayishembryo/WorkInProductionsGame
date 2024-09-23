using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    public float zSpeed; // Used to move pillars across map
    public float ySpeed; // Used to move pillars under water in case of collision with PillarDestroyer
    [SerializeField] private ParticleSystem destroyParticle;

    private void Start()
    {
        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, 0, transform.parent.transform.position.z);
    }
    private void Update()
    {
        transform.parent.transform.Translate(0, ySpeed, zSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PillarDestroyer"))
        {
            Debug.Log(gameObject.transform.parent.name + " has collided with destroyer! They will now be destroyed.");
            StartCoroutine(DestroyPillar());
        }
    }

    private IEnumerator DestroyPillar()
    {
        ySpeed = -0.222f;
        zSpeed = -0.2f;
        destroyParticle.Play();
        yield return new WaitForSeconds(6f);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
