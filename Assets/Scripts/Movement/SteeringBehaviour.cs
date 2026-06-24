using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public abstract Vector3 GetSteeringForce(Vector3 posicionAgente, Vector3 velocidadAgente);
}
