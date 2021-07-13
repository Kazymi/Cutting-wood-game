using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    private Renderer _renderer;
    private Texture2D _texture2D;
    private Texture2D _cloneTexture;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _texture2D = _renderer.material.mainTexture as Texture2D;
        _cloneTexture = Instantiate(_texture2D);
    }

    public void Draw(RaycastHit hit,Color _color, int _pixelsSize)
    {
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= _texture2D.width;
        pixelUV.y *= _texture2D.height;

        var colors = new Color[_pixelsSize*_pixelsSize];
        for (var i = 0; i < _pixelsSize*_pixelsSize; i++)
        {
            colors[i] = _color;
        }

        for (int i = 0; i != _texture2D.height; i++)
        {
            _texture2D.SetPixels(i, (int)pixelUV.y, _pixelsSize, _pixelsSize, colors);
        }
        _texture2D.Apply();
    }

    private void OnApplicationQuit()
    {
        _texture2D = _cloneTexture;
        _texture2D.Apply();
    }
}
