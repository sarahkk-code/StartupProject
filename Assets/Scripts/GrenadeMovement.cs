
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeMovement : MonoBehaviour
{
    public float throwSpeed = 2f;
    public float upwardForce = 0.5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Throw(Vector3 direction)
    {
        if (rb == null)
            return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 throwDirection =
            (direction.normalized + Vector3.up * upwardForce).normalized;

        rb.AddForce(throwDirection * throwSpeed, ForceMode.VelocityChange);
    }
}
