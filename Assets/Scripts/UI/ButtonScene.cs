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


    public Canvas canvas;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#92FF93", out ColorCurrentScene);
        ColorUtility.TryParseHtmlString("#8AB6FF", out ColorSelectedScene);
        ColorUtility.TryParseHtmlString("#FFFFFF", out ColorUnselecredScene);

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        toggleSelection(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (taleManager.IsModeLink)
        {
            Debug.Log("mode link " + taleManager.LinkFirstScene + " " + SceneId);

            if (taleManager.LinkFirstScene == SceneId)
            {
                return;
            }

            if (taleManager.LinkFirstScene == -1)
            {
                taleManager.LinkFirstScene = SceneId;
            }
            else
            {
                taleManager.CreateLink(SceneId);
            }
            return;
        }
        Debug.Log("down");
        IsMoving = true;
        taleManager.SelectSceneBtn(this, Scene);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
        IsMoving = false;
        taleManager.RenderLinks();
    }

    public void Update()
    {
        if (IsMoving)
        {
            /*
            Vector2 pos = Vector2.one;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
            pos.y += 40;
            Debug.Log("pos:" + pos);
            transform.localPosition = pos;
            */ // Screen Space - Camera

            transform.position = Input.mousePosition; // Screen Space - Overlay
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
