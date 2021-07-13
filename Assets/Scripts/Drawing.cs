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
        _renderer.material.mainTexture = _cloneTexture;
    }
    
    public void Draw(RaycastHit hit,Color _color, int _pixelsSize)
    {
        ToOpaqueMode(_renderer.material);
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= _cloneTexture.width;
        pixelUV.y *= _cloneTexture.height;
        
        var colors = new Color[_pixelsSize*_pixelsSize];
        
        for (var i = 0; i < _pixelsSize*_pixelsSize; i++)
        {
            colors[i] = _color;
        }

        for (int i = 0; i != _cloneTexture.height; i++)
        {
            _cloneTexture.SetPixels(i, (int)pixelUV.y, _pixelsSize, 1, colors);
        }
        _cloneTexture.Apply();
    }
    
    public void ToOpaqueMode(Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }
}
