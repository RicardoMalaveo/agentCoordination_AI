using UnityEngine;

public class LinePattern : FormationPattern
{
    private float separacionEntreSlots = 8.0F;

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
        float signo = (indice % 2 == 0) ? 1f : -1f;
        float multiplicador = Mathf.Ceil(indice / 2f);

        float xLocal = signo * multiplicador * separacionEntreSlots;
        return new Vector3(xLocal, 0f, 0f);
    }
    public bool SupportsSlots(int cantidadSlots)
    {
        return true;
    }
}