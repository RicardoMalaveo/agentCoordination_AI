using UnityEngine;

public class CirclePattern : FormationPattern
{
    private float radioDeFormacion = 9.0f;

    public Vector3 GetDriftOffset(SlotAssignment[] slots)
    {
        if (slots == null || slots.Length == 0) return Vector3.zero;

        Vector3 sumaPosiciones = Vector3.zero;
        for (int i = 0; i < slots.Length; i++)
        {
            sumaPosiciones += GetSlotTransform(slots[i].indice);
        }
        return sumaPosiciones / slots.Length;
    }

    public Vector3 GetAnchorPoint()
    {
        return Vector3.zero;
    }

    public Vector3 GetSlotTransform(int indice)
    {
        FormationManager manager = Object.FindFirstObjectByType<FormationManager>();
        if (manager == null || manager.slots.Count <= 1)
        {
            return new Vector3(radioDeFormacion, 0f, 0f);
        }

        int totalAgentes = manager.slots.Count;
        float anguloPasoSimetrico = (2f * Mathf.PI) / totalAgentes;
        float anguloActual = indice * anguloPasoSimetrico;
        float xLocal = Mathf.Cos(anguloActual) * radioDeFormacion;
        float zLocal = Mathf.Sin(anguloActual) * radioDeFormacion;

        return new Vector3(xLocal, 0f, zLocal);
    }

    public bool SupportsSlots(int cantidadSlots)
    {
        return true;
    }
}