using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIconBtn : MonoBehaviour
{
    public Transform context;
    public Text skillNameText;
    public Text skillKindText;
    public Text skillExplanation;

    public void OnClickSkillIcon()
    {
        skillNameText.gameObject.SetActive(true);
        skillKindText.gameObject.SetActive(true);
        skillExplanation.gameObject.SetActive(true);

        for (int i = 0; i < context.childCount; i++)
        {
            context.GetChild(i).GetComponent<Image>().color = new Color(192 / 255f, 192 / 255f, 192 / 255f, 186 / 255f);
        }
        this.GetComponent<Image>().color = Color.white;

        int tempNum = 0;
        if (this.gameObject.name.Contains("Water"))
        {
            tempNum = 5;
        }
        if (this.gameObject.name.Contains("Light"))
        {
            tempNum = 10;
        }
        tempNum += int.Parse(this.gameObject.name.Split('0')[1]) - 1;

        skillNameText.text = GameManager.instance.database.skill_DB.GetRowData(tempNum)[0];

        switch (int.Parse(this.gameObject.name.Split('0')[1]) - 1)
        {
            case 0:
                skillKindText.text = "[단일]";
                break;
            case 1:
                skillKindText.text = "[단일]";
                break;
            case 2:
                skillKindText.text = "[범위]";
                break;
            case 3:
                skillKindText.text = "[범위]";
                break;
            case 4:
                skillKindText.text = "[전체]";
                break;
        }

        skillExplanation.text = GameManager.instance.database.skill_DB.GetRowData(tempNum)[5];
    }
}
