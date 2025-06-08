using UnityEngine;

public class Debug_GUI : MonoBehaviour
{
    // OnGUI 에서 사용할 메시지 변수
    private string currentGUIMessage = "대기 중...";

    // OnGUI 텍스트 스타일 (선택 사항: Inspector에서 설정 가능하게)
    [Range(10, 150)]
    public int guiFontSize = 40;
    public Color guiTextColor = Color.red; // 기본값은 빨간색으로 설정

    // 메시지를 외부에서 업데이트할 수 있는 public 메서드
    public void SetGUIMessage(string message)
    {
        currentGUIMessage = message;
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = guiFontSize;
        style.normal.textColor = guiTextColor;
        style.alignment = TextAnchor.UpperCenter; // 화면 상단 중앙에 표시

        // 화면 중앙 상단에 메시지 표시 (원하는 위치로 Rect 값 조절 가능)
        GUI.Label(new Rect(Screen.width / 2 - 200, 10, 400, 100), currentGUIMessage, style);
    }
}