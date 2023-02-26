using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }
}
