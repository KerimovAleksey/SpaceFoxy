using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFoxController : MonoBehaviour
{
    [SerializeField] private AudioSource _foxClickSound;

    public void PlayFoxSound()
    {
        _foxClickSound.Play();
    }
}
