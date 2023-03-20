using System.Collections;
using UnityEngine;

public class EnemySinusMove : MonoBehaviour, IEnemyMove
{
    [SerializeField, Range(0.0001f, 0.5f)] private float _smooth = 1f;
    [SerializeField, Range(0f, 1f)] private float _time = 0;
    [SerializeField] private float _amplitude = 1000;
    [SerializeField] private float _frequency = 0.9f;
    [SerializeField] private float _speed = 0.4f;
    private float _moveDuration;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private float _timeDown;

    public void Init(Vector3 targetPosition) {
        _targetPosition = targetPosition;
        _startPosition = transform.position;
        _moveDuration = Vector2.Distance(_targetPosition, _startPosition) / _speed;
        int randomDirectionIndex = Random.Range(0, 2);
        if (randomDirectionIndex == 0) {
            _frequency = -_frequency;
        }
        StartCoroutine(Move());
    }

    public void StartMove() {
        StartCoroutine(Move());
    }

    public void StopMove() {
        StopAllCoroutines();
    }

    private IEnumerator Move() {
        _timeDown = _moveDuration;
        float timeUp = 0;
        while (_timeDown > 0) {
            Vector2 pos = WaveLerp(_startPosition, _targetPosition, timeUp, _amplitude, _frequency);
            transform.position = new Vector3(pos.x, pos.y);
            _timeDown -= Time.deltaTime;
            timeUp += Time.deltaTime / _moveDuration;
            yield return null;
        }
    }

    private Vector2 WaveLerp(Vector2 startPosition, Vector2 targetPosition, float time, float waveScale = 1f, float freq = 1f) {
        time = Mathf.Clamp01(time);
        _smooth = Mathf.Clamp(_smooth, 0.0001f, 0.5f);

        if (time <= _smooth) {
            waveScale *= time / _smooth;
        }
        else if (time > 1f - _smooth) {
            waveScale *= (1f - time) / _smooth;
        }
        Vector2 result = Vector2.Lerp(startPosition, targetPosition, time);

        Vector2 dir = (targetPosition - startPosition).normalized;
        Vector2 leftNormal = result + new Vector2(-dir.y, dir.x) * waveScale;

        result = Vector2.LerpUnclamped(result, leftNormal, Mathf.Sin(time * freq));
        if (float.IsNaN(result.x) && float.IsNaN(result.y)) {
            result = Vector3.zero;
        }
        return result;
    }
}
