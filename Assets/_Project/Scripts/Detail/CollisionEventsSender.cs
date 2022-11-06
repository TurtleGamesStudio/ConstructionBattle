using System;
using UnityEngine;

public class CollisionEventsSender : MonoBehaviour
{
    public event Action<Collision> Staying;
    public event Action<Collision> Entered;
    public event Action<Collision> Exited;

    private void OnCollisionStay(Collision collision)
    {
        Staying?.Invoke(collision);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Entered?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        Exited?.Invoke(collision);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
