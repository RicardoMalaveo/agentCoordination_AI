using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    public List<SlotAssignment> slots = new();
    public FormationPattern patron;
    public Transform anchorPoint;
    public Blackboard blackboard;
    private Vector3 ajuste_de_formacion = Vector3.zero;
    public void SetPattern(FormationPattern nuevoPatron)
    {
        patron = nuevoPatron;
        UpdateSlotAssignments();
    }

    public void UpdateSlotAssignments()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].indice = i;
        }

        if (patron != null)
        {
            ajuste_de_formacion = patron.GetDriftOffset(slots.ToArray());
        }
    }

    public bool AddAgent(GameObject agente)
    {
        if (patron == null || patron.SupportsSlots(slots.Count + 1))
        {
            SlotAssignment nuevoSlot = new SlotAssignment(agente, slots.Count);
            slots.Add(nuevoSlot);
            UpdateSlotAssignments();
            return true;
        }
        return false;
    }

    public bool RemoveAgent(GameObject agente)
    {
        SlotAssignment slotARemover = null;
        foreach (SlotAssignment slot in slots)
        {
            if (slot.agente == agente)
            {
                slotARemover = slot;
                break;
            }
        }

        if (slotARemover != null)
        {
            slots.Remove(slotARemover);
            UpdateSlotAssignments();
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (patron == null || slots.Count <= 1) 
        {
            return;
        }

        Vector3 baseAnchor = anchorPoint != null ? anchorPoint.position : transform.position;

        foreach (SlotAssignment slot in slots)
        {
            Vector3 transformacion_en_patron = patron.GetSlotTransform(slot.indice);
            Vector3 localizacionTarget = transformacion_en_patron + baseAnchor - ajuste_de_formacion;
            localizacionTarget.y = slot.agente.transform.position.y;
            AgentController controller = slot.agente.GetComponent<AgentController>();
            if (controller != null)
            {
                controller.SetTargetSlot(localizacionTarget);
            }
        }
    }
}