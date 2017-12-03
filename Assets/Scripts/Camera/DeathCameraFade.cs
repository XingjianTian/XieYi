using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeathCameraFade : MonoBehaviour {
    public GameObject Player;
    public DeathControl dc;
    public MonitoredByMonster mbm;
    public Shader curShader;
    public float grayScaleAmount = 1.0f;
    private Material curMaterial;
    public Material material
    {
        get
        {
            if(curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
	// Use this for initialization
	void Start () {
        //if (Player.GetComponent<DeathControl>().ifdead == true)
        // {
        dc = Player.GetComponent<DeathControl>();
        mbm = Player.GetComponent<MonitoredByMonster>();
            if (SystemInfo.supportsImageEffects == false)
            {
                enabled = false;
                return;
            }
            if (curShader != null && curShader.isSupported == false)
            {
                enabled = false;
            }
       // }
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.GetComponent<DeathControl>().ifdead == true)
            grayScaleAmount = Mathf.Clamp(grayScaleAmount, 0.0f, 1.0f);
	}
    void OnDisable()
    {
            if (curMaterial != null)
                DestroyImmediate(curMaterial);
    }
    void OnRenderImage(RenderTexture sourceTexture,RenderTexture destTexture)
    {
            if (curShader != null&&dc.ifdead == true&&dc.isgrounded == true)
        {
            material.SetFloat("_LuminosityAmount", grayScaleAmount);
                Graphics.Blit(sourceTexture, destTexture, material);

            }
            else if(curShader!=null&&mbm.iffade==true)
            {
                material.SetFloat("_LuminosityAmount", grayScaleAmount);
                Graphics.Blit(sourceTexture, destTexture, material);
                
            }
            else
            {
                Graphics.Blit(sourceTexture, destTexture);
            }
    }
}
