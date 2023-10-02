namespace MyEnum
{
    public enum SceneNameEnum
    {
        None, Persistence, MainScene, Level_01, Level_02, Level_03, Level_04,
        Level_05, Level_06, Level_07, Level_08, Level_09, Level_10, Level_11,
        Level_12, Level_13, Level_14, Level_15, Level_16, Level_17, Level_18,
        Level_19, Level_20, MaxCnt,Level_21, 
    } 
    public enum LevelNameEnum
    {
         Level_01, Level_02, Level_03,
    }
    public enum Player_Identity_Enum
    {
        Seeker,Hider
    }
    public enum HiderAndSeekerInputMode
    {
        PlayerInput,AIInput
    }
    public enum HiderStatusEnum
    {
        None, Idle,Run,BeenAttack,Swin,ThrowNone,TakeThrow,Throw
            ,Win,Fail, AIFail,
    }
    public enum SeekerStatusEnum
    {
        None, Idle, Run, Attack, Swin, HoldHead,
        Throw,
        TakeThrow,
        ThrowNone, Win, Fail, AIFail,
    }
    public enum UICloseType
    {
        None, Yes, No
    }
}