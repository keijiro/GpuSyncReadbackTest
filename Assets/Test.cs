using UnityEngine;
using UnityEngine.UI;

sealed class Test : MonoBehaviour
{
    [SerializeField] RawImage _ui1 = null;
    [SerializeField] RawImage _ui2 = null;
    [SerializeField] Shader _shader1 = null;
    [SerializeField] Shader _shader2 = null;
    [SerializeField] ComputeShader _compute = null;

    Material _material1, _material2;
    RenderTexture _rt1, _rt2;
    GraphicsBuffer _buffer;
    float[] _tempArray = new float[4];

    void Start()
    {
        _material1 = new Material(_shader1);
        _material2 = new Material(_shader2);

        _rt1 = new RenderTexture(8, 8, 0);
        _rt2 = new RenderTexture(8, 8, 0);

        _ui1.texture = _rt1;
        _ui2.texture = _rt2;

        _buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, 4, sizeof(float));
    }

    void OnDestroy()
    {
        Destroy(_material1);
        Destroy(_material2);
        Destroy(_rt1);
        Destroy(_rt2);
        _buffer.Dispose();
    }

    void Update()
    {
        Graphics.Blit(null, _rt1, _material1);

        _compute.SetTexture(0, "Input", _rt1);
        _compute.SetBuffer(0, "Output", _buffer);
        _compute.Dispatch(0, 1, 1, 1);

        _buffer.GetData(_tempArray);

        _material2.SetFloat("_Input", _tempArray[0]);

        Graphics.Blit(null, _rt2, _material2);
    }
}
