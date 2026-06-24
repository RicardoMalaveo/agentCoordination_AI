using UnityEngine;

public class ArriveBehaviour : SteeringBehaviour
{
    public float maxVelocidad;
    public float radioLlegada;
    [HideInInspector] public Vector3 targetSlot;

    public override Vector3 GetSteeringForce(Vector3 posicionAgente, Vector3 velocidadAgente)
    {
        Vector3 deseada = targetSlot - posicionAgente;
        deseada.y = 0;
        float distancia = deseada.magnitude;

        if (distancia < 0.1f) return Vector3.zero;

        if (distancia < radioLlegada)
        {
            float velocidadMapeada = maxVelocidad * (distancia / radioLlegada);
            deseada = deseada.normalized * velocidadMapeada;
        }
        else
        {
            deseada = deseada.normalized * maxVelocidad;
        }

        return deseada - velocidadAgente;
    }
}
