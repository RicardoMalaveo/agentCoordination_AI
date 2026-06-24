using System.Collections;
using System.Collections.Generic;
public class BehaviorAction
{
    public System.Action ejecucion;

    public BehaviorAction(System.Action ejecucion)
    {
        this.ejecucion = ejecucion;
    }
}

public abstract class Expert
{
    public abstract float GetInsistence(Blackboard pizarra);
    public abstract BehaviorAction[] Run(Blackboard pizarra);
}