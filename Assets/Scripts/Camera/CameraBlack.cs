using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlack : MonoBehaviour {

    public Material ma;
    //初始化  
    void Start()
    {
        ma = new Material(Shader.Find("Custom/Test3")); //创建一个材质  
    }

    //这允许你使用基于shader的过滤器来处理最后的图片，  
    //进入的图片是source渲染纹理，结果是destination渲染纹理。  
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //拷贝源纹理到目的渲染纹理。这主要是用于实现图像效果。  
        //Blit设置dest到激活的渲染纹理，在材质上设置source作为  
        //_MainTex属性，并且绘制一个全屏方块。  
        Graphics.Blit(source, destination, ma);
    }
}
