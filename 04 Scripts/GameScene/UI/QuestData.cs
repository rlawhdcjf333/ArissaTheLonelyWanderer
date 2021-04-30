using System;

[Serializable]
public class QuestData 
{
    //퀘스트 아이디
    public int id;
    //퀘스트 이름
    public string name;
    //퀘스트 상세
    public string description;
    //퀘스트 목표
    public string objective;

    //완료 여부
    public bool isCompleted;

    //진행중 여부
    public bool isOnBoard;


}
