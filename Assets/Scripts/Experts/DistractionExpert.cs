using UnityEngine;

public class DistractionExpert : Expert
{
    public override float GetInsistence(Blackboard pizarra)
    {
        BlackboardData dataDistraccion = pizarra.GetDataByKey("punto_distraccion");
        if (dataDistraccion != null && dataDistraccion.valor != null)
        {
            return 0.6f;
        }
        return 0f;
    }

    public override BehaviorAction[] Run(Blackboard pizarra)
    {
        return new BehaviorAction[0];
    }
}