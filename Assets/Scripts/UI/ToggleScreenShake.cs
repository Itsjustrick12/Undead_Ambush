using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScreenShake : MonoBehaviour
{
    public void Toggle()
    {
        CinemachineShake.Instance.ToggleShake();
    }
}
