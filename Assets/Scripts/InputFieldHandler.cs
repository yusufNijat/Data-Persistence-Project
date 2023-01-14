using TMPro;
using UnityEngine;

public class InputFieldHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI nameField;

    void Start()
    {
        TMP_InputField input = gameObject.GetComponent<TMP_InputField>();
        input.onEndEdit.AddListener(SubmitName);
    }

    private void SubmitName(string arg0)
    {
        nameField.gameObject.SetActive(true);
        nameField.SetText(arg0);
        GameManager.SavePlayer(arg0);
    }
}
