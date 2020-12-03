using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class GuildScrollContoller : MonoBehaviour
{


    ScrollRect scrollRect;//스크롤쪽에 필요한부분
    [SerializeField]
    GameObject jobSeekerPrefab; // 고용 가능한 영웅 프리펩
    [SerializeField]
    GameObject MersenaryPrefab;// 고용된 영웅 프리펩
    [SerializeField]
    GameObject content; // 콘텐트 

    [SerializeField]
    int StartSign;// 스타트함수 제어용

    GameObject temp;

    int JobSeekerCount;
    int MersenaryCount;

    // Start is called before the first frame update
    void Start()
    {
        JsonIO.JsonTake();
        GlobalVariable.MaxjobSeekerIndex = JsonIO.save.GuildLV * 5;
        if (StartSign == 100 && JsonIO.save.JobSeeker_reset == 0)//startsign 100은 고용가능한 영웅을 위한것 90은 고용된영웅
        {
            JsonIO.save.JobSeeker_reset = 100;//길드 새로 시작을 위한 변수
            JsonIO.JsonExport();

            JobSeekerCount = GlobalVariable.MaxjobSeekerIndex;
            float temp1 = JobSeekerCount;//고용가능한 영웅 수 나중에 변수로 바꿔줘야함  길드에 필요한부분

            float width = 0.0f;
            float height = 550 + (275 * (temp1 - 2));

            scrollRect = GetComponent<ScrollRect>();
            scrollRect.content.sizeDelta = new Vector2(width, height);


            for (int i = 0; i < JobSeekerCount; i++)
            {
                int randtmep;
                int LV = -1;
                int classnum = -1;
                int cost = -1;
                string name = "x";
                string classname = "x";
                JsonIO.JsonTake();

                randtmep = Random.Range(1, 11);
                switch (JsonIO.save.GuildLV)
                {
                    case 1:
                        if (randtmep > 8)
                            LV = 2;
                        else
                            LV = 1;
                        break;
                    case 2:

                        if (randtmep > 8)
                            LV = 3;
                        else if (randtmep > 4)
                            LV = 2;
                        else
                            LV = 1;
                        break;
                    case 3:
                        if (randtmep > 8)
                            LV = 4;
                        else if (randtmep > 4)
                            LV = 3;
                        else
                            LV = 2;
                        break;
                }
                cost = LV * 100;

                classnum = Random.Range(1, 3);

                switch (classnum)
                {
                    case 1:
                        classname = "Cru";
                        break;
                    case 2:
                        classname = "Pir";
                        break;
                    case 3:
                        classname = "Mag";
                        break;
                    case 4:
                        classname = "Sli";
                        break;

                }

                randtmep = Random.Range(1, 5);
                switch (randtmep)
                {
                    case 1:
                        name = "Dismass";
                        break;
                    case 2:
                        name = "Ronald";
                        break;
                    case 3:
                        name = "Edrick";
                        break;
                    case 4:
                        name = "Timier";
                        break;

                }
                Character character = new Character(classname);
                JsonIO.save.JobSeeker_savedata[i].Level = LV;
                JsonIO.save.JobSeeker_savedata[i].Name = name;
                JsonIO.save.JobSeeker_savedata[i].State = "stand";
                JsonIO.save.JobSeeker_savedata[i].Job = classname;
                JsonIO.save.JobSeeker_savedata[i].Number = classnum;
                JsonIO.save.JobSeeker_savedata[i].Health = character.BasicSets[LV].Health;
                JsonIO.save.JobSeeker_savedata[i].Morale = character.BasicSets[LV].Morale / 2;
                for (int j = 1; j < Variable.SkillMany; j++)
                {
                    JsonIO.save.JobSeeker_savedata[i].Skill_Levels[j] = 1;
                }
                JsonIO.save.JobSeeker_savedata[i].EquipLevel = 1;
                JsonIO.save.JobSeeker_savedata[i].AccessoryIndex = 0;
                JsonIO.JsonExport();

                GlobalVariable.jobSeeker[i] = (GameObject)Instantiate(
                jobSeekerPrefab,
                transform.position + new Vector3(10, 202 - (i * 275), 0),
                Quaternion.identity

                );

                GlobalVariable.jobSeeker[i].transform.parent = content.transform;

                JsonIO.JsonExport();

            }
            GameObject.Find("JobSeekerScroll").SendMessage("Resize_jobSeeker");//리사이즈
            JsonIO.JsonExport();
        }
        else if (StartSign == 100 && JsonIO.save.JobSeeker_reset == 100)//else
        {

            JsonIO.save.JobSeeker_reset = 0;//나중에 지워야함 꼭!!

            JobSeekerCount = GlobalVariable.MaxjobSeekerIndex;
            float temp1 = JobSeekerCount;//고용가능한 영웅 수 나중에 변수로 바꿔줘야함  길드에 필요한부분

            float width = 0.0f;
            float height = 550 + (275 * (temp1 - 2));

            scrollRect = GetComponent<ScrollRect>();
            scrollRect.content.sizeDelta = new Vector2(width, height);


            for (int i = 0; i < JobSeekerCount; i++)
            {
                if (JsonIO.save.JobSeeker_savedata[i].Number == 0)
                {
                    GlobalVariable.jobSeekerIndexCheck[i] = -1;
                    continue;
                }



                Debug.Log("Tlqkf" + GlobalVariable.jobSeekerIndex);
                GlobalVariable.jobSeeker[i] = (GameObject)Instantiate(
                jobSeekerPrefab,
                transform.position + new Vector3(10, 202 - (i * 275), 0),
                Quaternion.identity

                );

                GlobalVariable.jobSeeker[i].transform.parent = content.transform;

                JsonIO.JsonExport();

            }
            JsonIO.JsonExport();
            Debug.Log("리사이즈앞");
            GameObject.Find("JobSeekerScroll").SendMessage("Resize_jobSeeker");

        }


        /*
        if (StartSign == 90)//머시너리 생성용
        {



            int MerCount = 0;
            for (int i = 1; i < Variable.RosterSize; i++)//고용된 머시너리 수 확인
            {
                if (JsonIO.save.Roster[i].Number != 0)
                {
                    MerCount++;//머시너리 수
                }
            }
            float temp1 = MerCount;//고용가능한 영웅 수 나중에 변수로 바꿔줘야함  길드에 필요한부분

            float width = 0.0f;
            float height = 550 + (275 * (temp1 - 2));

            scrollRect = GetComponent<ScrollRect>();
            scrollRect.content.sizeDelta = new Vector2(width, height);

            for (int i = 1, j = 0; i < Variable.RosterSize; i++, j++)
            {
                if (JsonIO.save.Roster[i].Number == 0)
                {
                    j--;
                    continue;
                }

                GlobalVariable.Mersenary[i] = (GameObject)Instantiate(
                   MersenaryPrefab,
                   transform.position + new Vector3(20, 202 - (j * 275), 0),
                   Quaternion.identity
                   );

                GlobalVariable.Mersenary[i].transform.parent = content.transform;
                Debug.Log("생성되는 머시너리" + i);

                JsonIO.JsonExport();

            }
        }*/
        JsonIO.JsonExport();
    }
    /*
    public void MakeMersemary()
    {

        JsonIO.JsonTake();
        for (int i = 1, j = 0; i < Variable.RosterSize; i++, j++)
        {
            Debug.Log("체크" + GlobalVariable.MersenaryIndexCheck[i] + "생성되는 머시너리" + i + "넘버" + JsonIO.save.Roster[i].Number);
            if (GlobalVariable.MersenaryIndexCheck[i] == -1)//-1이면 채워진거
            {
                Debug.Log("생성된 머시너리" + i);
                j--;
                continue;
            }

            GlobalVariable.Mersenary[i] = (GameObject)Instantiate(
               MersenaryPrefab,
               transform.position + new Vector3(20, 202 - (j * 275), 0),
               Quaternion.identity
               );

            GlobalVariable.Mersenary[i].transform.parent = content.transform;

            Debug.Log("생성되는 머시너리" + i);


            JsonIO.JsonExport();
            break;

        }
    }

    */


    public void Resize_jobSeeker()
    {
        JsonIO.JsonTake();
        int JobSeekerCount2 = 0;


        for (int k = 0; k < GlobalVariable.MaxjobSeekerIndex; k++)
        {
            if (JsonIO.save.JobSeeker_savedata[k].Number == 0)
            {
                continue;
            }
            JobSeekerCount2++;
        }




        float temp1 = JobSeekerCount2;//고용가능한 영웅 수 나중에 변수로 바꿔줘야함
        int i = 0, j = 0;
        float width = 0.0f;
        float height = 550 + (275 * (temp1 - 2));
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.content.sizeDelta = new Vector2(width, height);
        scrollRect.content.position = new Vector3(1030, -5, 0);



        for (i = 0, j = 0; i < GlobalVariable.MaxjobSeekerIndex; i++, j++)
        {
            if (JsonIO.save.JobSeeker_savedata[i].Number == 0)
            {
                j--;
                continue;
            }

            GlobalVariable.jobSeeker[i].transform.position = new Vector3(1420, -142 - (j * 275), 0);
        }

    }
    /*
    public void Resize_mersenary()
    {
        JsonIO.JsonTake();
        int MersenaryCount2 = 0;


        for (int k = 0; k < Variable.RosterSize; k++)
        {
            if (JsonIO.save.Roster[k].Number == 0 )
            {
                continue;
            }
            MersenaryCount2++;
        }
        
        int i = 0, j = 0;
        float width = 0.0f;
        float height = 550 + (275 * (MersenaryCount2 - 2));
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.content.sizeDelta = new Vector2(width, height);
        scrollRect.content.position = new Vector3(150, -5, 0);


        for (i = 1, j = 0; i < Variable.RosterSize; i++, j++)
        {
            if (JsonIO.save.Roster[i].Number == 0 )
            {
                Debug.Log("스킾되는것" + i);
                j--;
                continue;
            }

            GlobalVariable.Mersenary[i].transform.position = new Vector3(546, -142 - (j * 275), 0);
        }

    }

   */

}
