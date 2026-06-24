using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AgentController : MonoBehaviour
{
    public float maxFuerza;
    public float maxVelocidad;

    private ArriveBehaviour arrive;
    private SeparationBehaviour separation;
    private ObstacleAvoidanceBehaviour avoidance;
    private LookWhereYouAreGoing lookWhereGoing;
    private Rigidbody rb;
    private Vector3 velocidad;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        arrive = GetComponent<ArriveBehaviour>();
        separation = GetComponent<SeparationBehaviour>();
        avoidance = GetComponent<ObstacleAvoidanceBehaviour>();
        lookWhereGoing = GetComponent<LookWhereYouAreGoing>();

        rb.freezeRotation = true;
    }

    public void SetTargetSlot(Vector3 nuevoTarget)
    {
        if (arrive != null)
        {
            arrive.targetSlot = nuevoTarget;
        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 fArrive = arrive != null ? arrive.GetSteeringForce(pos, velocidad) : Vector3.zero;
        Vector3 fSeparation = separation != null ? separation.GetSteeringForce(pos, velocidad) : Vector3.zero;
        Vector3 fAvoidance = avoidance != null ? avoidance.GetSteeringForce(pos, velocidad) : Vector3.zero;

        Vector3 fuerzaTotal;
        if (fAvoidance.sqrMagnitude > 0.01f)
        {
            fuerzaTotal = (fArrive * 0.3f) + (fSeparation * 1.5f) + (fAvoidance * 3.5f);
        }
        else
        {
            fuerzaTotal = fArrive + (fSeparation * 1.5f);
        }

        Vector3 aceleracion = Vector3.ClampMagnitude(fuerzaTotal, maxFuerza);
        velocidad += aceleracion * Time.fixedDeltaTime;
        velocidad = Vector3.ClampMagnitude(velocidad, maxVelocidad);
        velocidad.y = 0;

        rb.linearVelocity = velocidad;
        lookWhereGoing.GetSteeringForce(pos, velocidad);
    }
}