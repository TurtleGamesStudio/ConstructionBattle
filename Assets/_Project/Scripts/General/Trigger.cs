using UnityEngine;
using System;

public class Trigger : MonoBehaviour
{
    public event Action<Collider> Staying;
    public event Action<Collider> Entered;
    public event Action<Collider> Exited;

    private void OnTriggerStay(Collider other)
    {
        Staying?.Invoke(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        Entered?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        Exited?.Invoke(other);
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
