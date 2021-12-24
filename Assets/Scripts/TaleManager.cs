using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public enum ActionType { Move, Rotate };

public class TaleManager : MonoBehaviour
{
    public GameObject TextSceneNumber;
    public GameObject CurrentScene;

    public ButtonScene SelectedBtnScene;

    public GameObject ImgTarget;
    public GameObject BtnScenes;

    public GameObject BtnBack;
    public GameObject BtnAdd;
    public GameObject BtnShow;
    public GameObject BtnRemove;

    public GameObject Lines;
    public GameObject BtnAddLink;
    public GameObject BtnRemoveLink;
    public bool IsModeLink = false;
    public int LinkFirstScene;
    public Dictionary<int, List<int>> Links;

    public GameObject PanelScenesManager;
    public GameObject PanelScenesGraph;
    public GameObject TmplBtnScene;

    public Color ColorActionSelected;
    public Color ColorActionUnselected;
    public GameObject BtnMove;
    public GameObject BtnRotate;
    public ActionType actionType;

    public int LastSceneNumber;

    void Start()
    {
        ClearTale();

        PanelScenesManager.SetActive(false);

        BtnScenes.GetComponent<Button>().onClick.AddListener(() => PanelScenesManager.SetActive(true));
        BtnBack.GetComponent<Button>().onClick.AddListener(BtnBackOnClick);
        BtnAdd.GetComponent<Button>().onClick.AddListener(BtnAddOnClick);
        BtnShow.GetComponent<Button>().onClick.AddListener(BtnShowOnClick);
        BtnRemove.GetComponent<Button>().onClick.AddListener(BtnRemoveOnClick);

        BtnAddLink.GetComponent<Button>().onClick.AddListener(BtnAddLinkOnClick);
        BtnRemoveLink.GetComponent<Button>().onClick.AddListener(BtnRemoveLinkClick);

        BtnMove.GetComponent<Button>().onClick.AddListener(() => SetActionType(ActionType.Move));
        BtnRotate.GetComponent<Button>().onClick.AddListener(() => SetActionType(ActionType.Rotate));

        BtnAddOnClick(); // first scene

        RenderScene(1);
    }

    internal void CreateLink(int secondScene)
    {
        IsModeLink = false;

        int a = Math.Min(LinkFirstScene, secondScene);
        int b = Math.Max(LinkFirstScene, secondScene);

        if (!Links.ContainsKey(a))
        {
            Links.Add(a, new List<int>());
        }
        if (Links[a].Contains(b))
        {
            return;
        }
        Links[a].Add(b);
        RenderLinks();
    }

    public void RenderLinks()
    {
        foreach (Transform t in Lines.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (var kv in Links)
        {
            int a = kv.Key;
            foreach (int b in kv.Value)
            {
                GameObject obj = new GameObject();
                obj.transform.SetParent(Lines.transform);
                var lineImage = obj.AddComponent(typeof(Image)) as Image;
                lineImage.color = ColorActionUnselected;
                ButtonScene objA = FindButtonById(a);
                ButtonScene objB = FindButtonById(b);
                Vector3 delta = objA.transform.position - objB.transform.position;
                float width = delta.magnitude;
                lineImage.rectTransform.sizeDelta = new Vector2(width, 2);
                lineImage.rectTransform.pivot = new Vector2(0, 0);
                lineImage.rectTransform.localPosition = objA.transform.position;
                var rad = (float) Math.Asin(delta.normalized.y);
                lineImage.rectTransform.Rotate(new Vector3(0, 0, -rad * 180 / 3.14f));

                /*var lineRenderer = obj.AddComponent(typeof(LineRenderer)) as LineRenderer;
                lineRenderer.SetPosition(0, FindButtonById(a).transform.position);
                lineRenderer.SetPosition(1, FindButtonById(b).transform.position);
                lineRenderer.SetWidth(0.6f, 0.6f);
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.SetColors(ColorActionUnselected, ColorActionUnselected);*/
            }
        }
    }

    ButtonScene FindButtonById(int id)
    {
        foreach (Transform t in PanelScenesGraph.transform)
        {
            var bs = t.GetComponent<ButtonScene>();
            if (bs && bs.SceneId == id)
            {
                return bs;
            }
        }
        return null;
    }

    private void BtnAddLinkOnClick()
    {
        IsModeLink = true;
    }

    private void BtnRemoveLinkClick()
    {

    }

    private void BtnBackOnClick()
    {
        PanelScenesManager.SetActive(false);
    }

    private void SetActionType(ActionType actionType)
    {
        this.actionType = actionType;
        BtnMove.GetComponent<Image>().color = ColorActionUnselected;
        BtnRotate.GetComponent<Image>().color = ColorActionUnselected;
        if (actionType == ActionType.Move)
        {
            BtnMove.GetComponent<Image>().color = ColorActionSelected;
        }
        else if (actionType == ActionType.Rotate)
        {
            BtnRotate.GetComponent<Image>().color = ColorActionSelected;
        }
    }

    public void ClearTale()
    {
        Links = new Dictionary<int, List<int>>();
        LinkFirstScene = -1;
        IsModeLink = false;
        LastSceneNumber = 1;
        CurrentScene = null;
        foreach (Transform sc in ImgTarget.transform)
        {
            Destroy(sc.gameObject);
        }

        foreach (Transform sc in PanelScenesGraph.transform)
        {
            Destroy(sc.gameObject);
        }
        Lines = new GameObject();
        Lines.transform.SetParent(PanelScenesGraph.transform);

        foreach (Transform sc in GetComponent<MenuManager>().ObjectsForScene.transform)
        {
            Destroy(sc.gameObject);
        }
        foreach (Transform sc in GetComponent<DrawPreviewSceneObjects>().ContentScroll.transform)
        {
            Debug.Log("destroy content");
            Debug.Log(sc.gameObject);
            Destroy(sc.gameObject);
        }
    }

    private void BtnAddOnClick()
    {
        CreateScene("Scene " + LastSceneNumber);
    }

    public ButtonScene CreateScene(string btnName)
    {
        GameObject btnScene = Instantiate(TmplBtnScene, PanelScenesGraph.transform);
        btnScene.GetComponentInChildren<Text>().text = btnName;

        GameObject scene = Instantiate(new GameObject(), ImgTarget.transform);

        bool currentSceneIsNull = CurrentScene == null;
        if (currentSceneIsNull)
        {
            CurrentScene = scene;
        }

        ButtonScene bs = btnScene.GetComponent<ButtonScene>();
        bs.Init(scene, this, currentSceneIsNull, LastSceneNumber);

        LastSceneNumber++;

        return bs;
    }

    private void BtnShowOnClick()
    {
        if (SelectedBtnScene == null)
        {
            BtnBackOnClick();
            return;
        }
        Debug.Log("SelectedId " + SelectedBtnScene.SceneId);
        CurrentScene = SelectedBtnScene.Scene;
        RenderScene(SelectedBtnScene.SceneId);
        foreach (Transform scene in ImgTarget.transform)
        {
            scene.gameObject.SetActive(scene.gameObject == SelectedBtnScene.Scene);
        }
        foreach (Transform sceneBtn in PanelScenesGraph.transform)
        {
            var tmpBtnScene = sceneBtn.GetComponent<ButtonScene>();
            tmpBtnScene.IsCurrent = tmpBtnScene == SelectedBtnScene;
            tmpBtnScene.toggleSelection(false);
        }
        BtnBackOnClick();
    }

    private void BtnRemoveOnClick()
    {
        if (SelectedBtnScene == null)
        {
            return;
        }
        Destroy(SelectedBtnScene.Scene);
        Destroy(SelectedBtnScene.gameObject);
    }

    private void RenderScene(int id)
    {
        TextSceneNumber.GetComponent<InputField>().text = "Scene " + id;
    }

    internal void SelectSceneBtn(ButtonScene buttonScene, GameObject sceneParent)
    {
        SelectedBtnScene = buttonScene;
        foreach (Transform btn in PanelScenesGraph.transform)
        {
            var btnScene = btn.gameObject.GetComponent<ButtonScene>();
            if (btnScene != null)
            {
                btnScene.toggleSelection(btnScene == buttonScene);
            }
        }
    }
}
