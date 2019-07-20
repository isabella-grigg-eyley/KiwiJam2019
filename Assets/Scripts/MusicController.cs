using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Allows music Control to be accessed everywhere using "MusicController.MC".
    public static MusicController MC;
    
    // The music event file can be changed easily within the editor.
    [FMODUnity.EventRef]
    public string music;

    [Header("FMOD Settings")]    
    [SerializeField] private string MusicParameterName;

    FMOD.Studio.EventInstance musicEV;

    void Awake()
    {  
        if(MC == null)
        {
            DontDestroyOnLoad(gameObject);
            MC = this;
        }
        else if(MC != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicEV = FMODUnity.RuntimeManager.CreateInstance(music);
        musicEV.start();    
    }

    // GameStartedMusic triggers the transition from menu to the game.
    public void GameStartedMusic()
    {
        musicEV.setParameterByName(MusicParameterName, 1f);  
        Debug.Log("Param1");  
    }

    // MusicLevel1 adds some bass
    public void MusicLevel1()
    {
        musicEV.setParameterByName(MusicParameterName, 2f);
    }
   
    // MusicLevel2 adds train drums and more melody
    public void MusicLevel2()
    {
        musicEV.setParameterByName(MusicParameterName, 3f);
    }

    // GameEndedMusic transitions back to menu music
    public void GameEndedMusic()
    {
        musicEV.setParameterByName(MusicParameterName, 0f);
    }
}
