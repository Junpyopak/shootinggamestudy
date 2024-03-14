using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] List<Button> ListBtns;

    [SerializeField] GameObject objMainView;
    [SerializeField] GameObject objRankView;

    [SerializeField] GameObject fabRankData;
    [SerializeField] Transform trsContents;

    string keyRankData = "rankData";
    List<cRank> listRank = new List<cRank>();//0~9

    void Awake()
    {
        //cam = Camera.main;
        //cam.WorldToScreenPoint();//월드포인트(주로 게임 오브젝트들의 위치,오브젝트들을 기준으로 어디에있는지),부포트포인트,스크린포인트(뷰포트(화면을 0~1로 표현하는것)
        //스크린(화면비로 위치를 표현하는것)은 화면 캠버스에서 어떻게 표기하냐에 따라 차이)
        initBtns();
        initRank();
        onRank(false);//시작할때 가장먼저 랭킹창이 나오는걸 방지하기위해 onrank함수의 값을 false로설정

        Tool.IsEnterFirstScene = true;
    }
    /// <summary>
    /// 랭크데이터를 입력합니다.
    /// </summary>
    private void initRank()
    {
        string rankValue = PlayerPrefs.GetString(keyRankData, string.Empty);//"";
        int count = 0;
        if (rankValue == string.Empty)
        {
            count = 10;
            for (int iNum = 0; iNum < count; iNum++)
            {
                listRank.Add(new cRank());
            }

            rankValue = JsonConvert.SerializeObject(listRank);
            PlayerPrefs.SetString(keyRankData, rankValue);
        }
        else//""가 아니었다면
        {
            listRank = JsonConvert.DeserializeObject<List<cRank>>(rankValue);
        }

        count = listRank.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            cRank rank = listRank[iNum];

            GameObject go = Instantiate(fabRankData, trsContents);
            RankData goSc = go.GetComponent<RankData>();
            goSc.SetData(iNum + 1, rank.name, rank.score);
        }
    }

    /// <summary>
    /// 버튼들을 초기화합니다.
    /// </summary>
    private void initBtns()
    {
        ListBtns[0].onClick.AddListener(onStart);//시작버튼
        ListBtns[1].onClick.AddListener(()=>onRank(true));//랭킹버튼
        ListBtns[2].onClick.AddListener(OnExit);//종료버튼
        ListBtns[3].onClick.AddListener(() =>onRank(false));//랭킹닫기 버튼,함수안에 메소드가 없으면 그냥 넣어도 되지만 있으면 람다식처럼 이렇게 쓰면 된다.
    }

    private void onStart()
    {
        SceneManager.LoadSceneAsync((int)SceneNums.PlayScene);//플레이씬 이동
    }

    private void onRank(bool _value)//true로 들어오면 랭크뷰를 켜줌
    {
        objMainView.SetActive(!_value);
        objRankView.SetActive(_value);
    }

    private void OnExit()
    {
#if UNITY_EDITOR//아예 코트다 없는것처럼 하고싶을땐 전처리
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    void Update()
    {

    }
}
