using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntryController : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text[] steps = new TMP_Text[5];

    public void updateStep(int step, string text)
    {
        if (step < 0 || step >= steps.Length) return;
        steps[step].text = text;
    }

    public bool isEntryTrue => toggle.isOn;

    public void SetTitle(string text)
    {
        title.text = text;
    }
        
    public void SetDescription(string text)
    {
                description.text = text;
    }
}