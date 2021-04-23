using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAbleObj : MonoBehaviour
{
    public AudioSource breakingSound;
   public void ItemDestoyed()
  {
    breakingSound.Play();
    Destroy(gameObject);
  }
  public void PlayBreak()
  {
    breakingSound.Play();
  }
  
}
