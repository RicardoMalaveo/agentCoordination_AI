using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public List<BlackboardData> entradas = new();
    public List<Expert> expertos = new();

    public void UpdateBlackboard()
    {
        if (expertos.Count == 0) return;

        Expert mejorExperto = null;
        float mayorInsistencia = -1f;
        foreach (Expert experto in expertos)
        {
            float insistencia = experto.GetInsistence(this);
            if (insistencia > mayorInsistencia)
            {
                mayorInsistencia = insistencia;
                mejorExperto = experto;
            }
        }

        if (mejorExperto != null)
        {
            BehaviorAction[] accionesAEjecutar = mejorExperto.Run(this);

            if (accionesAEjecutar != null)
            {
                for (int i = 0; i < accionesAEjecutar.Length; i++)
                {
                    if (accionesAEjecutar[i] != null && accionesAEjecutar[i].ejecucion != null)
                    {
                        accionesAEjecutar[i].ejecucion.Invoke();
                    }
                }
            }
        }
    }

    public BlackboardData GetDataByKey(string clave)
    {
        foreach (BlackboardData entrada in entradas)
        {
            if (entrada.clave == clave)
            {
                return entrada;
            }
        }
        return null;
    }
    public void SetData(string clave, System.Type tipo, object valor)
    {
        BlackboardData data = GetDataByKey(clave);
        if (data != null)
        {
            data.valor = valor;
        }
        else
        {
            entradas.Add(new BlackboardData(clave, tipo, valor));
        }
    }
}