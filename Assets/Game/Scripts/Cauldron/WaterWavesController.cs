using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Cauldron
{
    public class WaterWavesController : MonoBehaviour
    {
        [SerializeField] private bool developmentMode = false;
        
        private Renderer _waterRend;
        private MaterialPropertyBlock _mpb;
        
        private static readonly int TimeID = Shader.PropertyToID("_CurrentTime");
        private static readonly int WavesID = Shader.PropertyToID("_Waves");

        private const int MaxWavesCount = 16;
        private readonly Vector4[] _wavesBuffer = new Vector4[MaxWavesCount];
        private int _currentWaveToUpdate;
        
        private bool _wavesUpdateRequested;
        private int _drawRequested;

        private void Awake()
        {
            _waterRend = GetComponent<Renderer>();

            for (int i = 0; i < MaxWavesCount; i++)
            {
                _wavesBuffer[i] = new Vector4
                {
                    x = transform.position.x,
                    y = transform.position.z,
                    z = float.MinValue / 2,
                    w = 0f
                };
            }
            
            _mpb = new MaterialPropertyBlock();
        }

        private void Update()
        {
            DebugInput();
        }

        private void LateUpdate()
        {
            UpdateWavesInfo();
            TimeSynchronizationWithShader();
            
            _waterRend.SetPropertyBlock(_mpb);
        }

        private void OnDrawGizmos()
        {
            if (developmentMode)
            {
                Gizmos.color = Color.magenta;
                foreach (var wave in _wavesBuffer)
                {
                    if (Time.time - wave.z < 1f)
                        Gizmos.DrawSphere(new Vector3(wave.x, transform.position.y, wave.y), 0.01f);
                }
            }
        }

        public void AddWave(Vector2 coords)
        {
            var newWave = new Vector4(coords.x, coords.y, Time.time, 1);
            
            _wavesBuffer[_currentWaveToUpdate++] = newWave;
            
            if (_currentWaveToUpdate >= MaxWavesCount)
                _currentWaveToUpdate = 0;
            
            _wavesUpdateRequested = true;
        }

        private void UpdateWavesInfo()
        {
            if (_wavesUpdateRequested)
            {
                _wavesUpdateRequested = false;
                _mpb.SetVectorArray(WavesID, _wavesBuffer);
            }
        }

        private void TimeSynchronizationWithShader()
        {
            _mpb.SetFloat(TimeID, Time.time);
        }

        private void DebugInput()
        {
            if (developmentMode)
            {
                //create a wave at random cauldron point
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    var wavePos = new Vector2(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
                    wavePos.x += transform.position.x;
                    wavePos.y += transform.position.z;

                    AddWave(wavePos);

                    Debug.Log($"Created wave at {wavePos} point");
                    float wavesCount = _wavesBuffer.Count(w => w.w != 0);
                    Debug.Log($"Waves count: {wavesCount}");
                }
            }
        }
    }
}
