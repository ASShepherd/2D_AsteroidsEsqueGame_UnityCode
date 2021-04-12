using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    private void OnEnable()
    {
        Destroy(this.gameObject, 3.0f);
    }
}
