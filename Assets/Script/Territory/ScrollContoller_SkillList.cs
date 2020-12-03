using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class ScrollContoller_SkillList : MonoBehaviour
{
    [SerializeField]
    int SkillNum;

    public void Skill_Slect()
    {
        transform.parent.parent.parent.parent.GetComponent<ScrollContoller_TrainingGround>().Resize_Skill(SkillNum);
    }
    
}
