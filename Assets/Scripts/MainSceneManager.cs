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
        //cam.WorldToScreenPoint();//��������Ʈ(�ַ� ���� ������Ʈ���� ��ġ,������Ʈ���� �������� ����ִ���),����Ʈ����Ʈ,��ũ������Ʈ(����Ʈ(ȭ���� 0~1�� ǥ���ϴ°�)
        //��ũ��(ȭ���� ��ġ�� ǥ���ϴ°�)�� ȭ�� ķ�������� ��� ǥ���ϳĿ� ���� ����)
        initBtns();
        initRank();
        onRank(false);//�����Ҷ� ������� ��ŷâ�� �����°� �����ϱ����� onrank�Լ��� ���� false�μ���

        Tool.IsEnterFirstScene = true;
    }
    /// <summary>
    /// ��ũ�����͸� �Է��մϴ�.
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
        else//""�� �ƴϾ��ٸ�
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
    /// ��ư���� �ʱ�ȭ�մϴ�.
    /// </summary>
    private void initBtns()
    {
        ListBtns[0].onClick.AddListener(onStart);//���۹�ư
        ListBtns[1].onClick.AddListener(()=>onRank(true));//��ŷ��ư
        ListBtns[2].onClick.AddListener(OnExit);//�����ư
        ListBtns[3].onClick.AddListener(() =>onRank(false));//��ŷ�ݱ� ��ư,�Լ��ȿ� �޼ҵ尡 ������ �׳� �־ ������ ������ ���ٽ�ó�� �̷��� ���� �ȴ�.
    }

    private void onStart()
    {
        SceneManager.LoadSceneAsync((int)SceneNums.PlayScene);//�÷��̾� �̵�
    }

    private void onRank(bool _value)//true�� ������ ��ũ�並 ����
    {
        objMainView.SetActive(!_value);
        objRankView.SetActive(_value);
    }

    private void OnExit()
    {
#if UNITY_EDITOR//�ƿ� ��Ʈ�� ���°�ó�� �ϰ������� ��ó��
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    void Update()
    {

    }
}