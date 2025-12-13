using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    public GameObject mug;
    public GameObject girlMugPoint;

    public Vector3 endRotValue;

    public float startWaitTime = 2, mugMoveTime, mugRotTime;

    [Header("UI")] public Image whiteimg;
    public Image textBox, buttonBox;
    public TMP_Text text, buttonText;
    public float fadeTime;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Mug());
    }

    private IEnumerator Mug()
    {
        yield return new WaitForSeconds(startWaitTime);

        mug.transform.DOMove(girlMugPoint.transform.position, mugMoveTime);

        yield return new WaitForSeconds(mugMoveTime);

        mug.transform.DORotate(endRotValue, mugRotTime);

        whiteimg.DOFade(1, fadeTime);

        yield return new WaitForSeconds(fadeTime);

        text.DOFade(1, fadeTime);
        textBox.DOFade(1, fadeTime);
        buttonBox.DOFade(1, fadeTime);
        buttonText.DOFade(1, fadeTime);
    }

    public void MainMenu()
    {
        SceneManager.instance.ChangeScene(0);
    }
    
    
    
}
