using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : AbstractAbility
{
    //[SerializeField]
    //Rigidbody characterRB;

    //[SerializeField]
    //Transform rotationAnchor;
    [Header("Assignments")]
    [SerializeField]
    Dash dash;

    [SerializeField]
    Transform directionAnchor;

    [SerializeField]
    public Collider hitbox;

    [Header("Primary Force Scalars")]
    [SerializeField]
    float forceScalar = 1;

    [SerializeField]
    float empoweredScalar = 1;

    [SerializeField]
    float hitboxDuration = 0.2f;

    [Header("Added Vector")]
    [SerializeField]
    Vector3 impulseVector;

    [Header("Explosion")]
    [SerializeField]
    float explosionScalar = 1;

    [SerializeField]
    float explosionRadius = 1;

    public GameObject kickVFX;
    public GameObject dropkickVFX;
    [SerializeField]
    private GameObject vfxSpawnPoint;

    

    readonly List<Rigidbody> enemies = new();

    // Applies a random impulse force to the character
    protected override void Execute()
    {
        ViewmodelScript.viewmodelAnim.SetTrigger("kick");
        // VVV Obsolete, kick hitbox is now handled thru viewmodel animation.
        //hitbox.enabled = true;
        //StartCoroutine(DisableHitbox());
    }

    protected override void ChildTick()
    {

    }

    /*private void Update()
    {
        transform.rotation = Quaternion.Euler(rotationAnchor.rotation.x * -1.0f, rotationAnchor.rotation.y, 0);
    }*/

    /*IEnumerator DisableHitbox()
    {
        yield return new WaitForSeconds(hitboxDuration);
        hitbox.enabled = false;
        ApplyForce();
    }
    */

    public void ApplyForce()
    {
        //Vector3 force = transform.TransformVector(impulseVector * forceScalar + VectorUtils.DirectionVector(transform, forceScalar));

        if (enemies.Count >= 1 && dash.IsKickEmpowered() == true)
        {
            Instantiate(dropkickVFX, vfxSpawnPoint.transform.position, Quaternion.identity);
        }
        if (enemies.Count >= 1 && dash.IsKickEmpowered() == false)
        {
            Instantiate(kickVFX, vfxSpawnPoint.transform.position, Quaternion.identity);
        }

        Vector3 force = directionAnchor.transform.forward * forceScalar + impulseVector;

        float empowerMult = dash.IsKickEmpowered() ? empoweredScalar : 1;
        //Debug.Log("Empower: " + empowerMult);
        dash.ResetEmpoweredKick();
        
        foreach (Rigidbody rb in enemies)
        {
            // The kick itself
            if (rb != null)
            {
                rb.AddForce(force * empowerMult, ForceMode.Impulse);

                // Small random force
                rb.AddExplosionForce(explosionScalar, transform.position, explosionRadius, 0, ForceMode.Impulse);
            }
        }
        
        //Debug.Log("KICKKKK!");
        enemies.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        //Debug.Log("Enemy added!");

        enemies.Add(other.GetComponent<Rigidbody>());
    }
}
