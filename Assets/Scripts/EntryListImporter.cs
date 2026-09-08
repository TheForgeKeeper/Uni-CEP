using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class EntryListImporter : MonoBehaviour
{
    [SerializeField] private TextAsset EntriesCSV;
    [SerializeField] private GameObject EntryPrefab;
    [SerializeField] private Transform EntryListParent;
    [SerializeField] private RectTransform scrollArea;
    [SerializeField] private string[] filters;
    [SerializeField] private float distanceBetweenEntries = 10f;
    [SerializeField] private float entriesPadding;

    [SerializeField] private List<EntryData> entriesData;
    [SerializeField] private bool realizeEntries = true;
    [SerializeField] private bool processData;

    private void OnValidate()
    {
        if(processData)
        {
            processData = false;
            CSVToEntriesData();
        }
        if(realizeEntries)
        {
            realizeEntries = false;
            RealizeEntries();
        }
    }

    [ExecuteInEditMode]
    public void RealizeEntries()
    {
        int entries = 0;
        for(int i = 0; i < entriesData.Count; i++)
        {
            EntryData entry = entriesData[i];
            if(filters.Contains(entry.filter) == false) continue;

            GameObject entryGO = PrefabUtility.InstantiatePrefab(EntryPrefab) as GameObject;
            entryGO.transform.SetParent(EntryListParent, false);
            float entryRectHeight = entryGO.GetComponent<RectTransform>().rect.height;
            float entryY = (-entries * (entryRectHeight + distanceBetweenEntries)) - entriesPadding;
            entryGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,entryY);
            EntryController entryController = entryGO.GetComponent<EntryController>();
            entryController.SetTitle(entry.title);
            entryController.SetDescription(entry.description);
            for(int j = 0;j < 5; j++)
            {
                if (j < entry.steps.Length)
                {
                    entryController.updateStep(j, entry.steps[j]);
                }
                else
                {
                    entryController.updateStep(j, "");
                }
            }
            entries++;
        }

        float scrollParentHeight = scrollArea.parent.GetComponent<RectTransform>().rect.height;
        float entryHeight = EntryPrefab.GetComponent<RectTransform>().rect.height;
        scrollArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (entries * (entryHeight + distanceBetweenEntries)) + entriesPadding * 2);
    }

    public void CSVToEntriesData()
    {
        entriesData = new List<EntryData>();
        foreach(string line in EntriesCSV.text.Split('\n'))
        {
            string[] values = line.Split(',');
            if (values.Length < 3) continue; // Skip lines that don't have enough data
            EntryData entry = new EntryData
            {
                filter = values[0].Trim(),
                title = values[1].Trim(),
                description = values[2].Trim(),
                steps = values[3].Trim().Split("|")
            };
            entriesData.Add(entry);

        }
    }


    [System.Serializable]
    struct EntryData
    {
        public string filter;
        public string title;
        public string description;
        public string[] steps;
    }
}
