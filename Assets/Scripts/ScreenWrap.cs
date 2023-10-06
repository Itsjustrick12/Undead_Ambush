using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private void Update()
    {
        if (this.gameObject.transform.position.x <= -9.5)
        {
            gameObject.transform.position = new Vector3(10+Random.Range(0,10), gameObject.transform.position.y, 1);
        }
    }
}
