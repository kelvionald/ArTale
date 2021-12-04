using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScene : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Scene;
    public TaleManager taleManager;
    public bool IsCurrent = false;
    public int SceneId = 0;

    private Color ColorSelectedScene;
    private Color ColorUnselecredScene;
    private Color ColorCurrentScene;

    public int LastSceneNumber;

    public bool IsMoving = false;


    public GameObject canvas;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#92FF93", out ColorCurrentScene);
        ColorUtility.TryParseHtmlString("#8AB6FF", out ColorSelectedScene);
        ColorUtility.TryParseHtmlString("#FFFFFF", out ColorUnselecredScene);

        GetComponent<Button>().onClick.AddListener(OnClick);
        //GetComponent<Button>().OnMove

        toggleSelection(false);
    }

    private void OnClick()
    {
        taleManager.SelectSceneBtn(this, Scene);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
        IsMoving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
        IsMoving = false;
    }

    public void Update()
    {
        if (IsMoving)
        {
            Vector2 _pos = Vector2.one;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, canvas.GetComponent<Canvas>().worldCamera, out _pos);
            Debug.Log("pos:" + _pos);
            transform.localPosition = _pos;

        }
    }

    public void Init(GameObject Scene, TaleManager taleManager, bool IsCurrent, int SceneId)
    {
        this.Scene = Scene;
        this.taleManager = taleManager;
        this.IsCurrent = IsCurrent;
        this.SceneId = SceneId;

        toggleSelection(false);
    }

    internal void toggleSelection(bool toggle)
    {
        if (toggle)
        {
            GetComponent<Image>().color = ColorSelectedScene;
        }
        else
        {
            GetComponent<Image>().color = IsCurrent ? ColorCurrentScene : ColorUnselecredScene;
        }
    }
}
