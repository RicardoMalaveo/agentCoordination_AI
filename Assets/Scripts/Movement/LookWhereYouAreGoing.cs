using UnityEngine;

public class LookWhereYouAreGoing : SteeringBehaviour
{
    public float velocidadRotacion;
    public override Vector3 GetSteeringForce(Vector3 posicionAgente, Vector3 velocidadAgente)
    {
        if (velocidadAgente.sqrMagnitude > 0.01f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(velocidadAgente.normalized);
            rb.rotation = Quaternion.Slerp(rb.rotation, rotacionObjetivo, Time.fixedDeltaTime * velocidadRotacion);
        }
        return Vector3.zero;
    }
}