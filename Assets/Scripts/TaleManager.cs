using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaleManager : MonoBehaviour
{
    public GameObject[] ImageTargets;
    public GameObject ObjectsForScene;
    public GameObject ContentScroll;
    public GameObject ContentScrollItem;

    void Start()
    {

        int i = 0;
        float imageHeight = 58;

        foreach (Transform child in ObjectsForScene.transform)
        {
            if (null == child)
                continue;

            Texture2D img = RuntimePreviewGenerator.GenerateModelPreview(child);
            GameObject obj = Instantiate(ContentScrollItem, ContentScroll.transform);
            obj.transform.localPosition = changeY(obj.transform.localPosition, obj.transform.localPosition.y - i * imageHeight);
            obj.GetComponent<RawImage>().texture = img;
            i++;
            child.gameObject.SetActive(false);
        }
    }

    Vector3 changeY(Vector3 position, float changeY)
    {
        return new Vector3(position.x, changeY, position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
