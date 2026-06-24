using UnityEngine;

public class FormationExpert : Expert
{
    public override float GetInsistence(Blackboard pizarra)
    {
        return 0.2f;
    }

    public override BehaviorAction[] Run(Blackboard pizarra)
    {
        return new BehaviorAction[0];
    }
}