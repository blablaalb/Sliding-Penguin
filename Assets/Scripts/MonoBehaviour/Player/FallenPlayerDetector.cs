using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenPlayerDetector : MonoBehaviour
{
    internal void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // TODO: currently we just reloading a level after the pnguins fall down. Put osme logic here.
            LevelManager.Instance.RestartLevel();
        }
    }
}
