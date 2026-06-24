using UnityEngine;

public class TrianglePattern : FormationPattern
{
    private float separacionFrenteYFlancos = 6.0f;
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
        if (indice == 0)
        {
            return Vector3.zero;
        }

        int fila = Mathf.FloorToInt((Mathf.Sqrt(8 * indice + 1) - 1) / 2);
        int primerIndiceDeFila = (fila * (fila + 1)) / 2;
        int posicionEnFila = indice - primerIndiceDeFila;
        float despX = (posicionEnFila - (fila / 2.0f)) * separacionFrenteYFlancos;
        float despZ = -fila * separacionFrenteYFlancos;

        return new Vector3(despX, 0f, despZ);
    }

    public bool SupportsSlots(int cantidadSlots)
    {
        return true;
    }
}