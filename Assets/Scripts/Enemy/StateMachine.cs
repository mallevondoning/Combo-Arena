using System;
using UnityEngine;

public class StateMachine
{
    public virtual void Enter()
    {
        throw new NotImplementedException();
    }

    public virtual void Action()
    {
        throw new NotImplementedException();
    }

    public virtual StateMachine Exit()
    {
        return this;
    }
}
