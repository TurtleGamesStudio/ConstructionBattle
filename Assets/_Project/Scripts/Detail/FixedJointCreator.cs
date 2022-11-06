using UnityEngine;
using System;

public class FixedJointCreator : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    public void Connect()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            throw new InvalidOperationException($"{name} doesn't have {nameof(Rigidbody)} component");
        }

        Rigidbody connectedBody = _target.GetComponent<Rigidbody>();

        if (connectedBody == null)
        {
            throw new InvalidOperationException($"{connectedBody.name} doesn't have {nameof(Rigidbody)} component");
        }

        FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = connectedBody;
    }
}
