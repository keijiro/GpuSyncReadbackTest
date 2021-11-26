using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

sealed class VideoTest : MonoBehaviour
{
    [SerializeField] RawImage _ui1 = null;
    [SerializeField] RawImage _ui2 = null;
    [SerializeField] VideoPlayer _video = null;
    [SerializeField] ComputeShader _compute = null;
    [SerializeField] Shader _shader = null;

    GraphicsBuffer _buffer;
    float[] _tempArray = new float[4];

    Material _material;
    RenderTexture _rt1, _rt2;

    void Start()
    {
        _buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, 4, sizeof(float));
        _material = new Material(_shader);
        _rt1 = new RenderTexture(1920, 1080, 0);
        _rt2 = new RenderTexture(8, 8, 0);
    }

    void OnDestroy()
    {
        _buffer.Dispose();
        Destroy(_material);
        Destroy(_rt1);
        Destroy(_rt2);
    }

    void Update()
    {
        if (_video.texture == null) return;

        Graphics.Blit(_video.texture, _rt1);

        _compute.SetTexture(0, "Input", _rt1);
        _compute.SetBuffer(0, "Output", _buffer);
        _compute.Dispatch(0, 1, 1, 1);

        _buffer.GetData(_tempArray);

        _material.SetFloat("_Input", _tempArray[0]);

        Graphics.Blit(null, _rt2, _material);

        _ui1.texture = _rt1;
        _ui2.texture = _rt2;
    }
}
