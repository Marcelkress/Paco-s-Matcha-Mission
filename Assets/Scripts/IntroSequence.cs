using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    [Title("Speed and wait parameters")]
    public float walkTime;
    public GameObject girlWalkTarget, endWalkTarget;
    public float waitTime = 1;
    public int spawnCatStep = 4, startPortalStep;
    
    [ReadOnly]public int girlDialogueIndex = 0, boyDialogueIndex;
    private Animator boyAnim, girlAnim;
    private bool skippable;

    [Title("UI elements")] 
    public TMP_Text[] boyTexts;
    public TMP_Text[] girlTexts;
    public SpriteRenderer textBoxBoy, textBoxGirl;
    private bool flipflop = true;
    public float fadeDuration;

    [Title("Player UI to enable")] 
    public GameObject healthBar;
    public GameObject matchaTracker;

    [Title("Other references")] public Animator whirlAnim;
    public bool disable = false;
    public GameObject boy, girl;
    [FormerlySerializedAs("camera")] public CinemachineCamera cam;
    public Image promptUI, buttonImg;
    public TMP_Text promptText;
    public PausePanel pausePanel;

    public PlayerInput inputActionMap;
    private InputAction skipAction;
    
    void Start()
    {
        if (disable)
        {
            Destroy(this);
            matchaTracker.SetActive(true);
            healthBar.SetActive(true);
            return; // We still return because the object is only destroyed after the first Update loop
        }
        
        SceneManager.instance.sceneLoadedEvent.AddListener(StartSeq);
    }

    private void StartSeq()
    {
        pausePanel.canPause = false;
        matchaTracker.SetActive(false);
        healthBar.SetActive(false);
        GameManager.instance.ChangeInputActionMap(true);
        flipflop = true;
        skippable = false;
        skipAction = inputActionMap.actions.FindAction("SkipDialogue");
        skipAction.performed += SkipDialogue;
        inputActionMap.GetComponent<SpriteRenderer>().enabled = false;

        cam.Target.TrackingTarget = boy.transform;
        CatInput.canReceiveInput = false;
        
        boyAnim = boy.GetComponent<Animator>();
        girlAnim = girl.GetComponent<Animator>();
        
        StartCoroutine(Begin());
    }
    
    private IEnumerator Begin()
    {
        textBoxBoy.DOFade(1, fadeDuration);
        NextDialogue();

        yield return new WaitForSeconds(waitTime);
        
        TriggerWalk();
    }

    public void TriggerWalk()
    {
        girlAnim.SetBool("Walk", true);
        girl.transform.DOMove(girlWalkTarget.transform.position, walkTime, false);
    }

    private bool triggered = false;
    public void Update()
    {
        if (girl.transform.position == girlWalkTarget.transform.position && !triggered)
        {
            girlAnim.SetBool("Walk", false);
            textBoxGirl.DOFade(1, fadeDuration);
            NextDialogue();
            triggered = true;
            skippable = true;
            promptUI.DOFade(1, fadeDuration);
            buttonImg.DOFade(1, fadeDuration);
            promptText.DOFade(1, fadeDuration);
        }
    }

    private void SkipDialogue(InputAction.CallbackContext context)
    {
        if (skippable)
        {
            NextDialogue();
        }
    }
    
    private void NextDialogue()
    {
        for (int i = 0; i < boyTexts.Length; i++)
        {
            boyTexts[i].enabled = false;
            textBoxBoy.DOFade(0, fadeDuration);
        }
        for (int i = 0; i < girlTexts.Length; i++)
        {
            girlTexts[i].enabled = false;
            textBoxGirl.DOFade(0, fadeDuration);
        }
        
        // Next
        if (flipflop)
        {
            boyTexts[girlDialogueIndex].enabled = true;
            textBoxBoy.DOFade(1, fadeDuration);
            boyDialogueIndex++;
        }
        else
        {
            girlTexts[girlDialogueIndex].enabled = true;
            textBoxGirl.DOFade(1, fadeDuration);
            girlDialogueIndex++;
        }
        
        // Reached end
        if (boyDialogueIndex == boyTexts.Length)
        {
            StartCoroutine(End());
            skipAction.performed -= SkipDialogue;
            EnableCat();
            return;
        }

        if (girlDialogueIndex == spawnCatStep)
        {
            RevealCat();
        }

        if (boyDialogueIndex == startPortalStep)
        {
            whirlAnim.SetTrigger("Begin");
        }
        
        flipflop = !flipflop;
    }
    
    private IEnumerator End()
    {
        promptUI.DOFade(0, fadeDuration);
        buttonImg.DOFade(0, fadeDuration);
        promptText.DOFade(0, fadeDuration);
        
        // walk back into house
        yield return new WaitForSeconds(waitTime);
        
        boyAnim.SetBool("Walk", true);
        girlAnim.SetBool("Walk", true);
        girl.transform.DOScale(new Vector3(girl.transform.localScale.x * -1, girl.transform.localScale.y), 0f);
        textBoxBoy.DOFade(0, fadeDuration);
        boyTexts[boyTexts.Length - 1].DOFade(0, fadeDuration);

        girl.transform.DOMove(endWalkTarget.transform.position, walkTime + 2, false);
        boy.transform.DOMove(endWalkTarget.transform.position, walkTime + 2, false);
        
        yield return new WaitForSeconds(walkTime + 2);
        Destroy(girl);
        Destroy(boy);
        Destroy(this);
    }

    private void RevealCat()
    {
        whirlAnim.SetTrigger("End");
        inputActionMap.GetComponent<SpriteRenderer>().enabled = true;
        cam.Target.TrackingTarget = inputActionMap.transform;
        CatInput.canReceiveInput = true;
        GameManager.instance.ChangeInputActionMap(false);
        GameManager.instance.playerInput.currentActionMap.Disable();
        GameManager.instance.ChangeInputActionMap(true);
    }

    private void EnableCat()
    {
        pausePanel.canPause = true;
        GameManager.instance.ChangeInputActionMap(false);
        GameManager.instance.playerInput.currentActionMap.Enable();
        matchaTracker.SetActive(true);
        healthBar.SetActive(true);
    }
}