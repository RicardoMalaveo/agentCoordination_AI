using UnityEngine;

[System.Serializable]
public class SlotAssignment
{
    public GameObject agente;
    public int indice;

    public SlotAssignment(GameObject agente, int indice)
    {
        this.agente = agente;
        this.indice = indice;
    }
}