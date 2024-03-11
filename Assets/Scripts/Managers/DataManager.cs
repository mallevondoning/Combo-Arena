public static class DataManager
{
    public static ElementType[] ComboList { get; set; } = new ElementType[3];

    //option
    public static float Sensitvity { get; set; }
    public static float SenX { get; set; }
    public static float SenY { get; set; }

    public static int SetCurrentElement { get; set; } = 0;
    public static bool HasResetElement { get; set; } = false;
    public static int AmountFilledAfterReset { get; set; } = 0;


    public static int GetTotalComboEmpty()
    {
        int emptyCount = 0;
        for (int i = 0; i < ComboList.Length; i++) if (ComboList[i] == ElementType.NoneID) emptyCount++;
        return emptyCount;
    }
    public static int GetTotalComboFilled()
    {
        return ComboList.Length - GetTotalComboEmpty();
    }
}
