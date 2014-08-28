using System.ComponentModel;

namespace ClubArcada.BusinessObjects
{
    public enum eInfoCtrlType
    {
        [Description("NOT SET")]
        NotSet = 0,

        [Description("Level")]
        Level,
        
        [Description("Počet hráčov")]
        PlayerCountTotal,

        [Description("V hre")]
        ActivePlayerCount,

        [Description("Počet Buy In")]
        BuyInCount,

        [Description("Počet Re Buy")]
        ReBuyCount,

        [Description("Počet Add-on")]
        AddOnCount,

        [Description("First Chance")]
        FirstChangeCount,

        [Description("Počet Re-entry")]
        ReEntryCount,

        [Description("AVG Stack")]
        AvgStack,

        [Description("Chips Total")]
        ChipsTotal,

        [Description("Moneypool")]
        MoneyPool,

        [Description("Prizepool")]
        PrizePool,

        [Description("Liga")]
        League,

        [Description("Rake")]
        Rake,
    }

    public enum eEnvironment
    {
        NotSet = 0,
        Development,
        QA,
        Live
    }

    public enum eGameType
    {
        [Description("Vyberte si")]
        NotSet = 0,

        [Description("Freeze Out")]
        FreezeOut = 201,

        [Description("Limited Rebuy")]
        RebuyLimited = 202,

        [Description("Unlimited ReBuy")]
        RebuyUnlimited = 203,

        [Description("Double Chance")]
        DoubleChance = 204,

        [Description("Triple Chance")]
        TripleChance = 205,

        [Description("Freroll")]
        FreeRoll = 206,

        [Description("Cash Game")]
        CashGame = 207,

        [Description("Double Trouble")]
        DoubleTrouble = 208
    }

    public enum ePositions
    {
        [Description("50_30_20_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0")]
        NotSet = 0,

        [Description("50_30_20_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0")]
        p3,

        [Description("45_25_18_12_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0")]
        p4,

        [Description("40_23_16_12_9_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0")]
        p5,

        [Description("38_22_15_11_8_6_0_0_0_0_0_0_0_0_0_0_0_0_0_0")]
        p6,

        [Description("35_21_15_11_8_6_4_0_0_0_0_0_0_0_0_0_0_0_0_0")]
        p7,

        [Description("34_20_14_11_8_6_4_3_0_0_0_0_0_0_0_0_0_0_0_0")]
        p8,

        [Description("33_19_14_11_8_6_4_3_2_0_0_0_0_0_0_0_0_0_0_0")]
        p9,

        [Description("32,00_19,50_13,70_10,00_7,70_5,80_4,50_3,20_2,30_1,30_0,00_0,00_0,00_0,00_0,00_0,00_0,00_0,00_0,00_0,00")]
        p10,

        [Description("31,90_19,50_12,85_9,35_7,25_5,55_4,40_3,30_2,50_1,70_1,70_0,00_0,00_0,00_0,00_0,00_0,00_0,00_0,00_0,00")]
        p11,

        [Description("31,80_19,40_12,75_9,25_6,95_5,45_4,20_3,20_2,40_1,60_1,60_1,40_0,00_0,00_0,00_0,00_0,00_0,00_0,00_0,00")]
        p12,

        [Description("31,80_19,40_12,75_9,25_6,75_5,25_4,00_3,00_2,20_1,50_1,50_1,30_1,30_0,00_0,00_0,00_0,00_0,00_0,00_0,00")]
        p13,

        [Description("31,50_18,90_12,00_9,50_6,75_5,25_4,00_3,00_2,20_1,50_1,50_1,30_1,30_1,30_0,00_0,00_0,00_0,00_0,00_0,00")]
        p14,

        [Description("31,50_18,90_11,50_9,00_6,75_5,25_4,00_3,00_2,20_1,50_1,50_1,30_1,30_1,30_1,00_0,00_0,00_0,00_0,00_0,00")]
        p15,

        [Description("31,00_18,40_11,50_9,00_6,75_5,25_4,00_3,00_2,20_1,50_1,50_1,30_1,30_1,30_1,00_1,00_0,00_0,00_0,00_0,00")]
        p16,

        [Description("31,00_18,40_11,50_8,50_6,50_5,00_4,00_3,00_2,20_1,50_1,50_1,30_1,30_1,30_1,00_1,00_1,00_0,00_0,00_0,00")]
        p17,

        [Description("31,00_18,40_10,50_8,50_6,50_5,00_4,00_3,00_2,20_1,50_1,50_1,30_1,30_1,30_1,00_1,00_1,00_1,00_0,00_0,00")]
        p18,

        [Description("30,00_18,40_10,50_8,50_6,50_5,00_4,00_3,00_2,20_1,50_1,50_1,30_1,30_1,30_1,00_1,00_1,00_1,00_1,00_0,00")]
        p19,

        [Description("30,00_17,40_10,50_8,50_6,50_5,00_4,00_3,00_2,20_1,50_1,50_1,30_1,30_1,30_1,00_1,00_1,00_1,00_1,00_1,00")]
        p20,

    }

    public enum ePositionSeqs
    {
        NotSet = 0,
        p3 = 0,
        p4 = 28,
        p5 = 37,
        p6 = 46,
        p7 = 55,
        p8 = 64,
        p9 = 73,
        p10 = 100,
        p11 = 110,
        p12 = 120,
        p13 = 130,
        p14 = 140,
        p15 = 150,
        p16 = 160,
        p17 = 170,
        p18 = 180,
        p19 = 190,
        p20 = 200
    }
}
