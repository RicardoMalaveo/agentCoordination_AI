using UnityEngine;

public interface FormationPattern
{
    Vector3 GetDriftOffset(SlotAssignment[] slots);
    Vector3 GetAnchorPoint();
    Vector3 GetSlotTransform(int indice);
    bool SupportsSlots(int cantidadSlots);
}