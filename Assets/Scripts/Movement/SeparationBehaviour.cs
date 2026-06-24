using UnityEngine;

public class SeparationBehaviour : SteeringBehaviour
{
    public float radioSeparacion;
    public float maxVelocidad;

    public override Vector3 GetSteeringForce(Vector3 posicionAgente, Vector3 velocidadAgente)
    {
        Vector3 fuerzaEmpuje = Vector3.zero;
        int conteoVecinos = 0;
        SeparationBehaviour[] todos = FindObjectsByType<SeparationBehaviour>(FindObjectsSortMode.None);

        foreach (var otro in todos)
        {
            if (otro == this) continue;

            float distancia = Vector3.Distance(posicionAgente, otro.transform.position);
            if (distancia > 0 && distancia < radioSeparacion)
            {
                Vector3 direccionHuida = posicionAgente - otro.transform.position;
                direccionHuida.y = 0;
                fuerzaEmpuje += direccionHuida.normalized / distancia;
                conteoVecinos++;
            }
        }

        if (conteoVecinos > 0)
        {
            fuerzaEmpuje /= conteoVecinos;
            fuerzaEmpuje = fuerzaEmpuje.normalized * maxVelocidad;
            return fuerzaEmpuje - velocidadAgente;
        }
        return Vector3.zero;
    }
}