using UnityEngine;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;

    public _Pokemon Pokemon { get; set; }

    public void Setup()
    {
        Pokemon = new _Pokemon(_base, level);
    }

    public void PlayFaintAnimation()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
    }

    public void PlayAttackAnimation(Vector3 targetPosition)
    {
        var originalPosition = transform.position;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetPosition, 0.2f));
        seq.Append(transform.DOMove(originalPosition, 0.2f));
    }

    public void PlayDefendAnimation()
    {
        var sprite = GetComponent<SpriteRenderer>();
        Sequence seq = DOTween.Sequence();
        seq.Append(sprite.DOColor(Color.white, 0.1f));
        seq.Append(sprite.DOColor(Color.gray, 0.1f));
        seq.Append(sprite.DOColor(Color.white, 0.1f));
    }
}
