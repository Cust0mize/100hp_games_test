using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public abstract class AbstractPanel : MonoBehaviour
{
    [SerializeField] protected RectTransform _animationTransform;
    private float _scale = 1f;

    public virtual void Show(System.Action onComplete) {
        gameObject.SetActive(true);

        if (_animationTransform == null) {
            onComplete?.Invoke();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(IE_Showing(onComplete));

    }

    public virtual void Hide(System.Action onComplete) {

        if (_animationTransform == null) {
            onComplete?.Invoke();
            gameObject.SetActive(false);
            return;
        }

        StopAllCoroutines();
        StartCoroutine(IE_Hiding(onComplete));
    }

    private IEnumerator IE_Showing(System.Action onComplete) {
        _scale = 0.5f;

        while (_scale < 1f) {
            _scale += 5 * Time.deltaTime;
            _scale = Mathf.Min(_scale, 1f);
            _animationTransform.localScale = new Vector3(_scale, _scale, 1f);
            yield return null;
        }

        _animationTransform.localScale = Vector3.one;
        onComplete?.Invoke();
    }

    private IEnumerator IE_Hiding(System.Action onComplete) {
        _scale = 1f;

        while (_scale > 0.5f) {
            _scale -= 10 * Time.deltaTime;
            _animationTransform.localScale = new Vector3(_scale, _scale, 1f);
            yield return null;
        }

        gameObject.SetActive(false);
        onComplete?.Invoke();
        _animationTransform.localScale = Vector3.one;
    }
}
