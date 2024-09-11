using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;

    [Header("Knife Count Display")]
    
    [SerializeField]
    private GameObject panelKnives;
    
    [SerializeField]
    private GameObject iconKnife;

    [SerializeField]
    private Color usedKnifeColor;

    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void SetInitialDisplayedKnifeCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(iconKnife, panelKnives.transform);
        }
    }

    private int KnifeIconIndexToChange = 0;
    
    public void DecrementDisplayedKnifeCount()
    {
        panelKnives.transform.GetChild(KnifeIconIndexToChange).GetComponent<Image>().color = usedKnifeColor;
        KnifeIconIndexToChange++;
    }
}