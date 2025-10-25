using UnityEngine;
using TMPro;
using System.Collections;

public class FadeInAndOutText : MonoBehaviour
{
    private TMP_Text _text;
    
    [SerializeField] private float _fadeInDuration = 1.5f;
    [SerializeField] private float _fadeOutDuration = 1.5f;
    [SerializeField] private float _visibleDuration = 2f;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        StartFadeSequence();
    }

    void StartFadeSequence()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color color = _text.color;
        color.a = 0f;
        _text.color = color;

        float timer = 0f;
        while (timer < _fadeInDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, timer / _fadeInDuration);
            _text.color = color;
            timer += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        _text.color = color;

        yield return new WaitForSeconds(_visibleDuration);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = _text.color;
        float timer = 0f;
        while (timer < _fadeOutDuration)
        {
            color.a = Mathf.Lerp(1f, 0f, timer / _fadeOutDuration);
            _text.color = color;
            timer += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        _text.color = color;
    }
}
