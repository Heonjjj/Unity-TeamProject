using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mousecursor : MonoBehaviour
{
    private RectTransform rectTransform;
    public Vector2 offset = new Vector2(10f, -10f);  // 좌측 위로 살짝 이동

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Cursor.visible = false; // 시스템 커서 숨기기
    }

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            Input.mousePosition,
            null,
            out pos
        );
        rectTransform.localPosition = pos+offset;
    }
}
