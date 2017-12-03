using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Volume : MonoBehaviour {

    public GameObject came;
    public AudioListener cameAL;
    public bool ifvolumeon;
    public Sprite VolumeOn;
    public Sprite VolumeOff;
    public Image img;
    void Start ()
    {
        img = GetComponent<Image>();
        ifvolumeon = true;
	}
    public void OnChangeVolumeSet()
    {
        
        if(ifvolumeon==true)
        {
            ifvolumeon = false;
            img.sprite = VolumeOff;
            AudioListener.pause = true;

        }
        else if(ifvolumeon == false)
        {
            ifvolumeon = true;
            img.sprite = VolumeOn;
            AudioListener.pause = false;
        }
        
    }
        

}
