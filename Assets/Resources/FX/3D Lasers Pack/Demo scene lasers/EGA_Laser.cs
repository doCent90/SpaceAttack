using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EGA_Laser : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private float _hitOffset = 0;
    [SerializeField] private float _maxLength;
    [SerializeField] private float _mainTextureLength = 1f;
    [SerializeField] private float _noiseTextureLength = 1f;

    private Vector4 _length = new Vector4(1,1,1,1);
    private bool _isLaserSaver = false;
    private bool _hasUpdateSaver = false;
    private LineRenderer _laser;

    private ParticleSystem[] _effects;
    private ParticleSystem[] _hits;

    private const string MainTexture = "_MainTex";
    private const string Noise = "_Noise";

    private void Start ()
    {
        _laser = GetComponent<LineRenderer>();
        _effects = GetComponentsInChildren<ParticleSystem>();
        _hits = _hitEffect.GetComponentsInChildren<ParticleSystem>();
    }

    private void Update()
    {
        _laser.material.SetTextureScale(MainTexture, new Vector2(_length[0], _length[1]));                    
        _laser.material.SetTextureScale(Noise, new Vector2(_length[2], _length[3]));

        if (_laser != null && _hasUpdateSaver == false)
        {
            _laser.SetPosition(0, transform.position);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength))
            {
                _laser.SetPosition(1, hit.point);
                _hitEffect.transform.position = hit.point + hit.normal * _hitOffset;
                _hitEffect.transform.rotation = Quaternion.identity;

                foreach (var allPs in _effects)
                {
                    if (!allPs.isPlaying) allPs.Play();
                }

                _length[0] = _mainTextureLength * (Vector3.Distance(transform.position, hit.point));
                _length[2] = _noiseTextureLength * (Vector3.Distance(transform.position, hit.point));
            }
            else
            {
                var endPos = transform.position + transform.forward * _maxLength;
                _laser.SetPosition(1, endPos);
                _hitEffect.transform.position = endPos;

                foreach (var allPs in _hits)
                {
                    if (allPs.isPlaying) allPs.Stop();
                }

                _length[0] = _mainTextureLength * (Vector3.Distance(transform.position, endPos));
                _length[2] = _noiseTextureLength * (Vector3.Distance(transform.position, endPos));
            }

            if (_laser.enabled == false && _isLaserSaver == false)
            {
                _isLaserSaver = true;
                _laser.enabled = true;
            }
        }  
    }

    public void DisablePrepare()
    {
        if (_laser != null)
        {
            _laser.enabled = false;
        }
        _hasUpdateSaver = true;

        if (_effects != null)
        {
            foreach (var allPs in _effects)
            {
                if (allPs.isPlaying) allPs.Stop();
            }
        }
    }
}
