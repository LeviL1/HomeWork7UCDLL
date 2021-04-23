using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateNewMenu : EditorWindow
{


  
  public AudioSource JumpSound;
  public AudioSource ShootSound;
  public AudioSource DestSound;
  
  bool muteShootAudio = false;
  bool muteJumpAudio = false;
  bool muteDestAudio = false;
  bool muteBackgroundAudio = false;
  
  [MenuItem("MyWindow/AudioManager")]
  public static void ShowWindow()
  {
    GetWindow<CreateNewMenu>();
  }

  void OnGUI()
  {
    DestroyAbleObj objBreak = FindObjectOfType<DestroyAbleObj>();
    GunScript objShot = FindObjectOfType<GunScript>();
    FirstPersonController objJump = FindObjectOfType<FirstPersonController>();
    AccessAudioSource objBgMusic = FindObjectOfType<AccessAudioSource>();
    muteJumpAudio = EditorGUILayout.Toggle("Mute Jump Sound", muteJumpAudio);
    if(muteJumpAudio)
    {
      objJump.jumpSound.mute = true;
    }
    else
    {
      objJump.jumpSound.mute = false;
    }
    muteShootAudio = EditorGUILayout.Toggle("Mute shooting sound", muteShootAudio);
    if (muteShootAudio)
    {
      objShot.gunSound.mute = true;
    }
    else
    {
      objShot.gunSound.mute = false;
    }
    muteDestAudio = EditorGUILayout.Toggle("Mute Destruction Audio", muteDestAudio);
    if (muteDestAudio)
    {
      objBreak.breakingSound.mute = true;
    }
    else
    {
      objBreak.breakingSound.mute = false;
    }
    muteBackgroundAudio = EditorGUILayout.Toggle("Mute Background Audio", muteBackgroundAudio);
    if (muteBackgroundAudio)
    {
      objBgMusic.bgMusic.mute = true;
    }
    else
    {
      objBgMusic.bgMusic.mute = false;
    }
    objBreak.breakingSound.volume = EditorGUILayout.Slider("Destruction Volume", objBreak.breakingSound.volume, 0f, 1f);
    objShot.gunSound.volume = EditorGUILayout.Slider("Shooting Volume", objShot.gunSound.volume, 0f, 1f);
    objJump.jumpSound.volume = EditorGUILayout.Slider("Jump Volume", objJump.jumpSound.volume, 0f, 1f);
    objBgMusic.bgMusic.volume = EditorGUILayout.Slider("Background Volume", objBgMusic.bgMusic.volume, 0f, 1f);
    if (GUILayout.Button("Play Jump Sound"))
    {
      
      objJump.jumpSound.Play();

    }
    if (GUILayout.Button("Play Shooting Sound"))
    {
      
      objShot.gunSound.Play();
    }
    if (GUILayout.Button("Play Destruction Sound"))
    {
      
      objBreak.breakingSound.Play();
    }
    if(GUILayout.Button("Play Background Music"))
    {
      objBgMusic.bgMusic.Play();
    }
    if(GUILayout.Button("Stop Background Audio"))
    {
      objBgMusic.bgMusic.Stop();
    }
  }
}
