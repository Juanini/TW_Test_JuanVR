using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsUI : MonoBehaviour
{
    public TextMeshPro textMesh;

    private WaitForSeconds jumpWait;
    private int points;

    void Start()
    {
        jumpWait = new WaitForSeconds(1.85f);
    }

    public void AddPoints(int _points, Vector2 _startPosition)
    {
        points = _points;
        textMesh.text = "+" + points.ToString("00");

        transform.position = _startPosition;
        transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.2f);

        StartCoroutine(JumpToScore());
    }

    private IEnumerator JumpToScore()
    {
        yield return new WaitForSeconds(1f);

        transform.DOJump(UIManager.Ins.scoreText.transform.position, Random.Range(1, 101) > 50 ? -0.85f : 0.85f , 1, 0.4f)
                 .OnComplete(OnJumpComplete)
                 .SetEase(Ease.InOutSine);
    }

    private void OnJumpComplete()
    {
        gameObject.SetActive(false);
        LevelManager.Ins.AddScore(points);
    }
}
