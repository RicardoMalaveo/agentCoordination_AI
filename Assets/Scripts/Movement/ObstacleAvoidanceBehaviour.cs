using UnityEngine;
using System.Collections.Generic;

public class ObstacleAvoidanceBehaviour : SteeringBehaviour
{
    public Blackboard blackboard;
    public float radioObstaculoEvitar;
    public float maxVelocidad;
    public float distanciaVistaSensor;
    public LayerMask Obstaculos;

    public override Vector3 GetSteeringForce(Vector3 posicionAgente, Vector3 velocidadAgente)
    {
        if (blackboard == null || string.IsNullOrEmpty("obstaculos_registrados"))
        {
            return Vector3.zero;
        }

        DetectarYPublicarObstaculos(posicionAgente, velocidadAgente);
        BlackboardData data = blackboard.GetDataByKey("obstaculos_registrados");

        if (data == null || data.valor == null || !(data.valor is List<Vector3>))
        {
            return Vector3.zero;
        }

        List<Vector3> obstaculos = (List<Vector3>)data.valor;
        Vector3 fuerzaEsquiveAcumulada = Vector3.zero;

        foreach (Vector3 posObstaculo in obstaculos)
        {
            float distancia = Vector3.Distance(posicionAgente, posObstaculo);
            if (distancia > 0 && distancia < radioObstaculoEvitar)
            {
                Vector3 direccionEmpuje = posicionAgente - posObstaculo;
                direccionEmpuje.y = 0;
                fuerzaEsquiveAcumulada += direccionEmpuje.normalized / distancia;
            }
        }

        if (fuerzaEsquiveAcumulada.sqrMagnitude > 0.01f)
        {
            fuerzaEsquiveAcumulada = fuerzaEsquiveAcumulada.normalized * maxVelocidad;
            return fuerzaEsquiveAcumulada - velocidadAgente;
        }
        return Vector3.zero;
    }

    private void DetectarYPublicarObstaculos(Vector3 posicion, Vector3 velocidad)
    {
        Vector3 direccionMirada = velocidad.sqrMagnitude > 0.01f ? velocidad.normalized : transform.forward;
        Vector3 origenSensor = posicion;
        origenSensor.y += 0.5f;
        float radioDelSensor = 0.5f;

        if (Physics.SphereCast(origenSensor, radioDelSensor, direccionMirada, out RaycastHit hit, distanciaVistaSensor, Obstaculos))
        {
            Vector3 posObstaculo = hit.collider.transform.position;
            posObstaculo.y = posicion.y;

            List<Vector3> listaTablero;
            BlackboardData dataExistente = blackboard.GetDataByKey("obstaculos_registrados");

            if (dataExistente != null && dataExistente.valor is List<Vector3>)
            {
                listaTablero = (List<Vector3>)dataExistente.valor;
            }
            else
            {
                listaTablero = new List<Vector3>();
                blackboard.SetData("obstaculos_registrados", typeof(List<Vector3>), listaTablero);
            }

            if (!listaTablero.Contains(posObstaculo))
            {
                listaTablero.Add(posObstaculo);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 direccionMirada = transform.forward;
        Gizmos.color = Color.orange;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.5f, (transform.position + Vector3.up * 0.5f) + (direccionMirada * distanciaVistaSensor));
    }
}