using System.Collections.Generic;
using System;

public interface IInterTurnsBehaviour
{
    public event Action Completed;
    public event Action Failed;

    public void Init()
    {
    }

    public void StartBehaviour(IEnumerable<CollisionEventsSender> collisionEventsSenders)
    {
    }
}
