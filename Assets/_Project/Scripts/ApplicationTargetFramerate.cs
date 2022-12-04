using UnityEngine;

public class ApplicationTargetFramerate : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
