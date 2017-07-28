using System;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.Verify
{
    public class WaterLevelSearchData : Data
    {
        private string _ID ;
        //private new string _OBSDT ;
        public string OBSCD { get; set; }
        public string OBSNM { get; set; }
        public string DAMNM { get; set; }
        public string DAMCD { get; set; }
        //private new string _TRMDV ;
        private string _WL_0; public string WL_0 { get { return _WL_0; } set { DoubleValidationCheck(value); _WL_0 = value; IsDirty_0 = true; OnPropertyChanged("WL_0"); } }
        private string _WL_1; public string WL_1 { get { return _WL_1; } set { DoubleValidationCheck(value); _WL_1 = value; IsDirty_1 = true; OnPropertyChanged("WL_1"); } }
        private string _WL_2; public string WL_2 { get { return _WL_2; } set { DoubleValidationCheck(value); _WL_2 = value; IsDirty_2 = true; OnPropertyChanged("WL_2"); } }
        private string _WL_3; public string WL_3 { get { return _WL_3; } set { DoubleValidationCheck(value); _WL_3 = value; IsDirty_3 = true; OnPropertyChanged("WL_3"); } }
        private string _WL_4; public string WL_4 { get { return _WL_4; } set { DoubleValidationCheck(value); _WL_4 = value; IsDirty_4 = true; OnPropertyChanged("WL_4"); } }
        private string _WL_5; public string WL_5 { get { return _WL_5; } set { DoubleValidationCheck(value); _WL_5 = value; IsDirty_5 = true; OnPropertyChanged("WL_5"); } }
        private string _WL_6; public string WL_6 { get { return _WL_6; } set { DoubleValidationCheck(value); _WL_6 = value; IsDirty_6 = true; OnPropertyChanged("WL_6"); } }
        private string _WL_7; public string WL_7 { get { return _WL_7; } set { DoubleValidationCheck(value); _WL_7 = value; IsDirty_7 = true; OnPropertyChanged("WL_7"); } }
        private string _WL_8; public string WL_8 { get { return _WL_8; } set { DoubleValidationCheck(value); _WL_8 = value; IsDirty_8 = true; OnPropertyChanged("WL_8"); } }
        private string _WL_9; public string WL_9 { get { return _WL_9; } set { DoubleValidationCheck(value); _WL_9 = value; IsDirty_9 = true; OnPropertyChanged("WL_9"); } }
        private string _WL_10; public string WL_10 { get { return _WL_10; } set { DoubleValidationCheck(value); _WL_10 = value; IsDirty_10 = true; OnPropertyChanged("WL_10"); } }
        private string _WL_11; public string WL_11 { get { return _WL_11; } set { DoubleValidationCheck(value); _WL_11 = value; IsDirty_11 = true; OnPropertyChanged("WL_11"); } }
        private string _WL_12; public string WL_12 { get { return _WL_12; } set { DoubleValidationCheck(value); _WL_12 = value; IsDirty_12 = true; OnPropertyChanged("WL_12"); } }
        private string _WL_13; public string WL_13 { get { return _WL_13; } set { DoubleValidationCheck(value); _WL_13 = value; IsDirty_13 = true; OnPropertyChanged("WL_13"); } }
        private string _WL_14; public string WL_14 { get { return _WL_14; } set { DoubleValidationCheck(value); _WL_14 = value; IsDirty_14 = true; OnPropertyChanged("WL_14"); } }
        private string _WL_15; public string WL_15 { get { return _WL_15; } set { DoubleValidationCheck(value); _WL_15 = value; IsDirty_15 = true; OnPropertyChanged("WL_15"); } }
        private string _WL_16; public string WL_16 { get { return _WL_16; } set { DoubleValidationCheck(value); _WL_16 = value; IsDirty_16 = true; OnPropertyChanged("WL_16"); } }
        private string _WL_17; public string WL_17 { get { return _WL_17; } set { DoubleValidationCheck(value); _WL_17 = value; IsDirty_17 = true; OnPropertyChanged("WL_17"); } }
        private string _WL_18; public string WL_18 { get { return _WL_18; } set { DoubleValidationCheck(value); _WL_18 = value; IsDirty_18 = true; OnPropertyChanged("WL_18"); } }
        private string _WL_19; public string WL_19 { get { return _WL_19; } set { DoubleValidationCheck(value); _WL_19 = value; IsDirty_19 = true; OnPropertyChanged("WL_19"); } }
        private string _WL_20; public string WL_20 { get { return _WL_20; } set { DoubleValidationCheck(value); _WL_20 = value; IsDirty_20 = true; OnPropertyChanged("WL_20"); } }
        private string _WL_21; public string WL_21 { get { return _WL_21; } set { DoubleValidationCheck(value); _WL_21 = value; IsDirty_21 = true; OnPropertyChanged("WL_21"); } }
        private string _WL_22; public string WL_22 { get { return _WL_22; } set { DoubleValidationCheck(value); _WL_22 = value; IsDirty_22 = true; OnPropertyChanged("WL_22"); } }
        private string _WL_23; public string WL_23 { get { return _WL_23; } set { DoubleValidationCheck(value); _WL_23 = value; IsDirty_23 = true; OnPropertyChanged("WL_23"); } }
        private string _WL_24; public string WL_24 { get { return _WL_24; } set { DoubleValidationCheck(value); _WL_24 = value; IsDirty_24 = true; OnPropertyChanged("WL_24"); } }
        private string _WL_25; public string WL_25 { get { return _WL_25; } set { DoubleValidationCheck(value); _WL_25 = value; IsDirty_25 = true; OnPropertyChanged("WL_25"); } }
        private string _WL_26; public string WL_26 { get { return _WL_26; } set { DoubleValidationCheck(value); _WL_26 = value; IsDirty_26 = true; OnPropertyChanged("WL_26"); } }
        private string _WL_27; public string WL_27 { get { return _WL_27; } set { DoubleValidationCheck(value); _WL_27 = value; IsDirty_27 = true; OnPropertyChanged("WL_27"); } }
        private string _WL_28; public string WL_28 { get { return _WL_28; } set { DoubleValidationCheck(value); _WL_28 = value; IsDirty_28 = true; OnPropertyChanged("WL_28"); } }
        private string _WL_29; public string WL_29 { get { return _WL_29; } set { DoubleValidationCheck(value); _WL_29 = value; IsDirty_29 = true; OnPropertyChanged("WL_29"); } }
        private string _WL_30; public string WL_30 { get { return _WL_30; } set { DoubleValidationCheck(value); _WL_30 = value; IsDirty_30 = true; OnPropertyChanged("WL_30"); } }
        private string _WL_31; public string WL_31 { get { return _WL_31; } set { DoubleValidationCheck(value); _WL_31 = value; IsDirty_31 = true; OnPropertyChanged("WL_31"); } }
        private string _WL_32; public string WL_32 { get { return _WL_32; } set { DoubleValidationCheck(value); _WL_32 = value; IsDirty_32 = true; OnPropertyChanged("WL_32"); } }
        private string _WL_33; public string WL_33 { get { return _WL_33; } set { DoubleValidationCheck(value); _WL_33 = value; IsDirty_33 = true; OnPropertyChanged("WL_33"); } }
        private string _WL_34; public string WL_34 { get { return _WL_34; } set { DoubleValidationCheck(value); _WL_34 = value; IsDirty_34 = true; OnPropertyChanged("WL_34"); } }
        private string _WL_35; public string WL_35 { get { return _WL_35; } set { DoubleValidationCheck(value); _WL_35 = value; IsDirty_35 = true; OnPropertyChanged("WL_35"); } }
        private string _WL_36; public string WL_36 { get { return _WL_36; } set { DoubleValidationCheck(value); _WL_36 = value; IsDirty_36 = true; OnPropertyChanged("WL_36"); } }
        private string _WL_37; public string WL_37 { get { return _WL_37; } set { DoubleValidationCheck(value); _WL_37 = value; IsDirty_37 = true; OnPropertyChanged("WL_37"); } }
        private string _WL_38; public string WL_38 { get { return _WL_38; } set { DoubleValidationCheck(value); _WL_38 = value; IsDirty_38 = true; OnPropertyChanged("WL_38"); } }
        private string _WL_39; public string WL_39 { get { return _WL_39; } set { DoubleValidationCheck(value); _WL_39 = value; IsDirty_39 = true; OnPropertyChanged("WL_39"); } }
        private string _WL_40; public string WL_40 { get { return _WL_40; } set { DoubleValidationCheck(value); _WL_40 = value; IsDirty_40 = true; OnPropertyChanged("WL_40"); } }
        private string _WL_41; public string WL_41 { get { return _WL_41; } set { DoubleValidationCheck(value); _WL_41 = value; IsDirty_41 = true; OnPropertyChanged("WL_41"); } }
        private string _WL_42; public string WL_42 { get { return _WL_42; } set { DoubleValidationCheck(value); _WL_42 = value; IsDirty_42 = true; OnPropertyChanged("WL_42"); } }
        private string _WL_43; public string WL_43 { get { return _WL_43; } set { DoubleValidationCheck(value); _WL_43 = value; IsDirty_43 = true; OnPropertyChanged("WL_43"); } }
        private string _WL_44; public string WL_44 { get { return _WL_44; } set { DoubleValidationCheck(value); _WL_44 = value; IsDirty_44 = true; OnPropertyChanged("WL_44"); } }
        private string _WL_45; public string WL_45 { get { return _WL_45; } set { DoubleValidationCheck(value); _WL_45 = value; IsDirty_45 = true; OnPropertyChanged("WL_45"); } }
        private string _WL_46; public string WL_46 { get { return _WL_46; } set { DoubleValidationCheck(value); _WL_46 = value; IsDirty_46 = true; OnPropertyChanged("WL_46"); } }
        private string _WL_47; public string WL_47 { get { return _WL_47; } set { DoubleValidationCheck(value); _WL_47 = value; IsDirty_47 = true; OnPropertyChanged("WL_47"); } }
        private string _WL_48; public string WL_48 { get { return _WL_48; } set { DoubleValidationCheck(value); _WL_48 = value; IsDirty_48 = true; OnPropertyChanged("WL_48"); } }
        private string _WL_49; public string WL_49 { get { return _WL_49; } set { DoubleValidationCheck(value); _WL_49 = value; IsDirty_49 = true; OnPropertyChanged("WL_49"); } }
        private string _WL_50; public string WL_50 { get { return _WL_50; } set { DoubleValidationCheck(value); _WL_50 = value; IsDirty_50 = true; OnPropertyChanged("WL_50"); } }
        private string _WL_51; public string WL_51 { get { return _WL_51; } set { DoubleValidationCheck(value); _WL_51 = value; IsDirty_51 = true; OnPropertyChanged("WL_51"); } }
        private string _WL_52; public string WL_52 { get { return _WL_52; } set { DoubleValidationCheck(value); _WL_52 = value; IsDirty_52 = true; OnPropertyChanged("WL_52"); } }
        private string _WL_53; public string WL_53 { get { return _WL_53; } set { DoubleValidationCheck(value); _WL_53 = value; IsDirty_53 = true; OnPropertyChanged("WL_53"); } }
        private string _WL_54; public string WL_54 { get { return _WL_54; } set { DoubleValidationCheck(value); _WL_54 = value; IsDirty_54 = true; OnPropertyChanged("WL_54"); } }
        private string _WL_55; public string WL_55 { get { return _WL_55; } set { DoubleValidationCheck(value); _WL_55 = value; IsDirty_55 = true; OnPropertyChanged("WL_55"); } }
        private string _WL_56; public string WL_56 { get { return _WL_56; } set { DoubleValidationCheck(value); _WL_56 = value; IsDirty_56 = true; OnPropertyChanged("WL_56"); } }
        private string _WL_57; public string WL_57 { get { return _WL_57; } set { DoubleValidationCheck(value); _WL_57 = value; IsDirty_57 = true; OnPropertyChanged("WL_57"); } }
        private string _WL_58; public string WL_58 { get { return _WL_58; } set { DoubleValidationCheck(value); _WL_58 = value; IsDirty_58 = true; OnPropertyChanged("WL_58"); } }
        private string _WL_59; public string WL_59 { get { return _WL_59; } set { DoubleValidationCheck(value); _WL_59 = value; IsDirty_59 = true; OnPropertyChanged("WL_59"); } }
        private string _WL_60; public string WL_60 { get { return _WL_60; } set { DoubleValidationCheck(value); _WL_60 = value; IsDirty_60 = true; OnPropertyChanged("WL_60"); } }
        private string _WL_61; public string WL_61 { get { return _WL_61; } set { DoubleValidationCheck(value); _WL_61 = value; IsDirty_61 = true; OnPropertyChanged("WL_61"); } }
        private string _WL_62; public string WL_62 { get { return _WL_62; } set { DoubleValidationCheck(value); _WL_62 = value; IsDirty_62 = true; OnPropertyChanged("WL_62"); } }
        private string _WL_63; public string WL_63 { get { return _WL_63; } set { DoubleValidationCheck(value); _WL_63 = value; IsDirty_63 = true; OnPropertyChanged("WL_63"); } }
        private string _WL_64; public string WL_64 { get { return _WL_64; } set { DoubleValidationCheck(value); _WL_64 = value; IsDirty_64 = true; OnPropertyChanged("WL_64"); } }
        private string _WL_65; public string WL_65 { get { return _WL_65; } set { DoubleValidationCheck(value); _WL_65 = value; IsDirty_65 = true; OnPropertyChanged("WL_65"); } }
        private string _WL_66; public string WL_66 { get { return _WL_66; } set { DoubleValidationCheck(value); _WL_66 = value; IsDirty_66 = true; OnPropertyChanged("WL_66"); } }
        private string _WL_67; public string WL_67 { get { return _WL_67; } set { DoubleValidationCheck(value); _WL_67 = value; IsDirty_67 = true; OnPropertyChanged("WL_67"); } }
        private string _WL_68; public string WL_68 { get { return _WL_68; } set { DoubleValidationCheck(value); _WL_68 = value; IsDirty_68 = true; OnPropertyChanged("WL_68"); } }
        private string _WL_69; public string WL_69 { get { return _WL_69; } set { DoubleValidationCheck(value); _WL_69 = value; IsDirty_69 = true; OnPropertyChanged("WL_69"); } }
        private string _WL_70; public string WL_70 { get { return _WL_70; } set { DoubleValidationCheck(value); _WL_70 = value; IsDirty_70 = true; OnPropertyChanged("WL_70"); } }
        private string _WL_71; public string WL_71 { get { return _WL_71; } set { DoubleValidationCheck(value); _WL_71 = value; IsDirty_71 = true; OnPropertyChanged("WL_71"); } }
        private string _WL_72; public string WL_72 { get { return _WL_72; } set { DoubleValidationCheck(value); _WL_72 = value; IsDirty_72 = true; OnPropertyChanged("WL_72"); } }
        private string _WL_73; public string WL_73 { get { return _WL_73; } set { DoubleValidationCheck(value); _WL_73 = value; IsDirty_73 = true; OnPropertyChanged("WL_73"); } }
        private string _WL_74; public string WL_74 { get { return _WL_74; } set { DoubleValidationCheck(value); _WL_74 = value; IsDirty_74 = true; OnPropertyChanged("WL_74"); } }
        private string _WL_75; public string WL_75 { get { return _WL_75; } set { DoubleValidationCheck(value); _WL_75 = value; IsDirty_75 = true; OnPropertyChanged("WL_75"); } }
        private string _WL_76; public string WL_76 { get { return _WL_76; } set { DoubleValidationCheck(value); _WL_76 = value; IsDirty_76 = true; OnPropertyChanged("WL_76"); } }
        private string _WL_77; public string WL_77 { get { return _WL_77; } set { DoubleValidationCheck(value); _WL_77 = value; IsDirty_77 = true; OnPropertyChanged("WL_77"); } }
        private string _WL_78; public string WL_78 { get { return _WL_78; } set { DoubleValidationCheck(value); _WL_78 = value; IsDirty_78 = true; OnPropertyChanged("WL_78"); } }
        private string _WL_79; public string WL_79 { get { return _WL_79; } set { DoubleValidationCheck(value); _WL_79 = value; IsDirty_79 = true; OnPropertyChanged("WL_79"); } }
        private string _WL_80; public string WL_80 { get { return _WL_80; } set { DoubleValidationCheck(value); _WL_80 = value; IsDirty_80 = true; OnPropertyChanged("WL_80"); } }
        private string _WL_81; public string WL_81 { get { return _WL_81; } set { DoubleValidationCheck(value); _WL_81 = value; IsDirty_81 = true; OnPropertyChanged("WL_81"); } }
        private string _WL_82; public string WL_82 { get { return _WL_82; } set { DoubleValidationCheck(value); _WL_82 = value; IsDirty_82 = true; OnPropertyChanged("WL_82"); } }
        private string _WL_83; public string WL_83 { get { return _WL_83; } set { DoubleValidationCheck(value); _WL_83 = value; IsDirty_83 = true; OnPropertyChanged("WL_83"); } }
        private string _WL_84; public string WL_84 { get { return _WL_84; } set { DoubleValidationCheck(value); _WL_84 = value; IsDirty_84 = true; OnPropertyChanged("WL_84"); } }
        private string _WL_85; public string WL_85 { get { return _WL_85; } set { DoubleValidationCheck(value); _WL_85 = value; IsDirty_85 = true; OnPropertyChanged("WL_85"); } }
        private string _WL_86; public string WL_86 { get { return _WL_86; } set { DoubleValidationCheck(value); _WL_86 = value; IsDirty_86 = true; OnPropertyChanged("WL_86"); } }
        private string _WL_87; public string WL_87 { get { return _WL_87; } set { DoubleValidationCheck(value); _WL_87 = value; IsDirty_87 = true; OnPropertyChanged("WL_87"); } }
        private string _WL_88; public string WL_88 { get { return _WL_88; } set { DoubleValidationCheck(value); _WL_88 = value; IsDirty_88 = true; OnPropertyChanged("WL_88"); } }
        private string _WL_89; public string WL_89 { get { return _WL_89; } set { DoubleValidationCheck(value); _WL_89 = value; IsDirty_89 = true; OnPropertyChanged("WL_89"); } }
        private string _WL_90; public string WL_90 { get { return _WL_90; } set { DoubleValidationCheck(value); _WL_90 = value; IsDirty_90 = true; OnPropertyChanged("WL_90"); } }
        private string _WL_91; public string WL_91 { get { return _WL_91; } set { DoubleValidationCheck(value); _WL_91 = value; IsDirty_91 = true; OnPropertyChanged("WL_91"); } }
        private string _WL_92; public string WL_92 { get { return _WL_92; } set { DoubleValidationCheck(value); _WL_92 = value; IsDirty_92 = true; OnPropertyChanged("WL_92"); } }
        private string _WL_93; public string WL_93 { get { return _WL_93; } set { DoubleValidationCheck(value); _WL_93 = value; IsDirty_93 = true; OnPropertyChanged("WL_93"); } }
        private string _WL_94; public string WL_94 { get { return _WL_94; } set { DoubleValidationCheck(value); _WL_94 = value; IsDirty_94 = true; OnPropertyChanged("WL_94"); } }
        private string _WL_95; public string WL_95 { get { return _WL_95; } set { DoubleValidationCheck(value); _WL_95 = value; IsDirty_95 = true; OnPropertyChanged("WL_95"); } }
        private string _WL_96; public string WL_96 { get { return _WL_96; } set { DoubleValidationCheck(value); _WL_96 = value; IsDirty_96 = true; OnPropertyChanged("WL_96"); } }
        private string _WL_97; public string WL_97 { get { return _WL_97; } set { DoubleValidationCheck(value); _WL_97 = value; IsDirty_97 = true; OnPropertyChanged("WL_97"); } }
        private string _WL_98; public string WL_98 { get { return _WL_98; } set { DoubleValidationCheck(value); _WL_98 = value; IsDirty_98 = true; OnPropertyChanged("WL_98"); } }
        private string _WL_99; public string WL_99 { get { return _WL_99; } set { DoubleValidationCheck(value); _WL_99 = value; IsDirty_99 = true; OnPropertyChanged("WL_99"); } }
        private string _WL_100; public string WL_100 { get { return _WL_100; } set { DoubleValidationCheck(value); _WL_100 = value; IsDirty_100 = true; OnPropertyChanged("WL_100"); } }
        private string _WL_101; public string WL_101 { get { return _WL_101; } set { DoubleValidationCheck(value); _WL_101 = value; IsDirty_101 = true; OnPropertyChanged("WL_101"); } }
        private string _WL_102; public string WL_102 { get { return _WL_102; } set { DoubleValidationCheck(value); _WL_102 = value; IsDirty_102 = true; OnPropertyChanged("WL_102"); } }
        private string _WL_103; public string WL_103 { get { return _WL_103; } set { DoubleValidationCheck(value); _WL_103 = value; IsDirty_103 = true; OnPropertyChanged("WL_103"); } }
        private string _WL_104; public string WL_104 { get { return _WL_104; } set { DoubleValidationCheck(value); _WL_104 = value; IsDirty_104 = true; OnPropertyChanged("WL_104"); } }
        private string _WL_105; public string WL_105 { get { return _WL_105; } set { DoubleValidationCheck(value); _WL_105 = value; IsDirty_105 = true; OnPropertyChanged("WL_105"); } }
        private string _WL_106; public string WL_106 { get { return _WL_106; } set { DoubleValidationCheck(value); _WL_106 = value; IsDirty_106 = true; OnPropertyChanged("WL_106"); } }
        private string _WL_107; public string WL_107 { get { return _WL_107; } set { DoubleValidationCheck(value); _WL_107 = value; IsDirty_107 = true; OnPropertyChanged("WL_107"); } }
        private string _WL_108; public string WL_108 { get { return _WL_108; } set { DoubleValidationCheck(value); _WL_108 = value; IsDirty_108 = true; OnPropertyChanged("WL_108"); } }
        private string _WL_109; public string WL_109 { get { return _WL_109; } set { DoubleValidationCheck(value); _WL_109 = value; IsDirty_109 = true; OnPropertyChanged("WL_109"); } }
        private string _WL_110; public string WL_110 { get { return _WL_110; } set { DoubleValidationCheck(value); _WL_110 = value; IsDirty_110 = true; OnPropertyChanged("WL_110"); } }
        private string _WL_111; public string WL_111 { get { return _WL_111; } set { DoubleValidationCheck(value); _WL_111 = value; IsDirty_111 = true; OnPropertyChanged("WL_111"); } }
        private string _WL_112; public string WL_112 { get { return _WL_112; } set { DoubleValidationCheck(value); _WL_112 = value; IsDirty_112 = true; OnPropertyChanged("WL_112"); } }
        private string _WL_113; public string WL_113 { get { return _WL_113; } set { DoubleValidationCheck(value); _WL_113 = value; IsDirty_113 = true; OnPropertyChanged("WL_113"); } }
        private string _WL_114; public string WL_114 { get { return _WL_114; } set { DoubleValidationCheck(value); _WL_114 = value; IsDirty_114 = true; OnPropertyChanged("WL_114"); } }
        private string _WL_115; public string WL_115 { get { return _WL_115; } set { DoubleValidationCheck(value); _WL_115 = value; IsDirty_115 = true; OnPropertyChanged("WL_115"); } }
        private string _WL_116; public string WL_116 { get { return _WL_116; } set { DoubleValidationCheck(value); _WL_116 = value; IsDirty_116 = true; OnPropertyChanged("WL_116"); } }
        private string _WL_117; public string WL_117 { get { return _WL_117; } set { DoubleValidationCheck(value); _WL_117 = value; IsDirty_117 = true; OnPropertyChanged("WL_117"); } }
        private string _WL_118; public string WL_118 { get { return _WL_118; } set { DoubleValidationCheck(value); _WL_118 = value; IsDirty_118 = true; OnPropertyChanged("WL_118"); } }
        private string _WL_119; public string WL_119 { get { return _WL_119; } set { DoubleValidationCheck(value); _WL_119 = value; IsDirty_119 = true; OnPropertyChanged("WL_119"); } }
        private string _WL_120; public string WL_120 { get { return _WL_120; } set { DoubleValidationCheck(value); _WL_120 = value; IsDirty_120 = true; OnPropertyChanged("WL_120"); } }
        private string _WL_121; public string WL_121 { get { return _WL_121; } set { DoubleValidationCheck(value); _WL_121 = value; IsDirty_121 = true; OnPropertyChanged("WL_121"); } }
        private string _WL_122; public string WL_122 { get { return _WL_122; } set { DoubleValidationCheck(value); _WL_122 = value; IsDirty_122 = true; OnPropertyChanged("WL_122"); } }
        private string _WL_123; public string WL_123 { get { return _WL_123; } set { DoubleValidationCheck(value); _WL_123 = value; IsDirty_123 = true; OnPropertyChanged("WL_123"); } }
        private string _WL_124; public string WL_124 { get { return _WL_124; } set { DoubleValidationCheck(value); _WL_124 = value; IsDirty_124 = true; OnPropertyChanged("WL_124"); } }
        private string _WL_125; public string WL_125 { get { return _WL_125; } set { DoubleValidationCheck(value); _WL_125 = value; IsDirty_125 = true; OnPropertyChanged("WL_125"); } }
        private string _WL_126; public string WL_126 { get { return _WL_126; } set { DoubleValidationCheck(value); _WL_126 = value; IsDirty_126 = true; OnPropertyChanged("WL_126"); } }
        private string _WL_127; public string WL_127 { get { return _WL_127; } set { DoubleValidationCheck(value); _WL_127 = value; IsDirty_127 = true; OnPropertyChanged("WL_127"); } }
        private string _WL_128; public string WL_128 { get { return _WL_128; } set { DoubleValidationCheck(value); _WL_128 = value; IsDirty_128 = true; OnPropertyChanged("WL_128"); } }
        private string _WL_129; public string WL_129 { get { return _WL_129; } set { DoubleValidationCheck(value); _WL_129 = value; IsDirty_129 = true; OnPropertyChanged("WL_129"); } }
        private string _WL_130; public string WL_130 { get { return _WL_130; } set { DoubleValidationCheck(value); _WL_130 = value; IsDirty_130 = true; OnPropertyChanged("WL_130"); } }
        private string _WL_131; public string WL_131 { get { return _WL_131; } set { DoubleValidationCheck(value); _WL_131 = value; IsDirty_131 = true; OnPropertyChanged("WL_131"); } }
        private string _WL_132; public string WL_132 { get { return _WL_132; } set { DoubleValidationCheck(value); _WL_132 = value; IsDirty_132 = true; OnPropertyChanged("WL_132"); } }
        private string _WL_133; public string WL_133 { get { return _WL_133; } set { DoubleValidationCheck(value); _WL_133 = value; IsDirty_133 = true; OnPropertyChanged("WL_133"); } }
        private string _WL_134; public string WL_134 { get { return _WL_134; } set { DoubleValidationCheck(value); _WL_134 = value; IsDirty_134 = true; OnPropertyChanged("WL_134"); } }
        private string _WL_135; public string WL_135 { get { return _WL_135; } set { DoubleValidationCheck(value); _WL_135 = value; IsDirty_135 = true; OnPropertyChanged("WL_135"); } }
        private string _WL_136; public string WL_136 { get { return _WL_136; } set { DoubleValidationCheck(value); _WL_136 = value; IsDirty_136 = true; OnPropertyChanged("WL_136"); } }
        private string _WL_137; public string WL_137 { get { return _WL_137; } set { DoubleValidationCheck(value); _WL_137 = value; IsDirty_137 = true; OnPropertyChanged("WL_137"); } }
        private string _WL_138; public string WL_138 { get { return _WL_138; } set { DoubleValidationCheck(value); _WL_138 = value; IsDirty_138 = true; OnPropertyChanged("WL_138"); } }
        private string _WL_139; public string WL_139 { get { return _WL_139; } set { DoubleValidationCheck(value); _WL_139 = value; IsDirty_139 = true; OnPropertyChanged("WL_139"); } }
        private string _WL_140; public string WL_140 { get { return _WL_140; } set { DoubleValidationCheck(value); _WL_140 = value; IsDirty_140 = true; OnPropertyChanged("WL_140"); } }
        private string _WL_141; public string WL_141 { get { return _WL_141; } set { DoubleValidationCheck(value); _WL_141 = value; IsDirty_141 = true; OnPropertyChanged("WL_141"); } }
        private string _WL_142; public string WL_142 { get { return _WL_142; } set { DoubleValidationCheck(value); _WL_142 = value; IsDirty_142 = true; OnPropertyChanged("WL_142"); } }
        private string _WL_143; public string WL_143 { get { return _WL_143; } set { DoubleValidationCheck(value); _WL_143 = value; IsDirty_143 = true; OnPropertyChanged("WL_143"); } }



        private string _EDEXLVL_0; public string EDEXLVL_0 { get { return _EDEXLVL_0; } set { _EDEXLVL_0 = value; OnPropertyChanged("EDEXLVL_0"); } }
        private string _EDEXLVL_1; public string EDEXLVL_1 { get { return _EDEXLVL_1; } set { _EDEXLVL_1 = value; OnPropertyChanged("EDEXLVL_1"); } }
        private string _EDEXLVL_2; public string EDEXLVL_2 { get { return _EDEXLVL_2; } set { _EDEXLVL_2 = value; OnPropertyChanged("EDEXLVL_2"); } }
        private string _EDEXLVL_3; public string EDEXLVL_3 { get { return _EDEXLVL_3; } set { _EDEXLVL_3 = value; OnPropertyChanged("EDEXLVL_3"); } }
        private string _EDEXLVL_4; public string EDEXLVL_4 { get { return _EDEXLVL_4; } set { _EDEXLVL_4 = value; OnPropertyChanged("EDEXLVL_4"); } }
        private string _EDEXLVL_5; public string EDEXLVL_5 { get { return _EDEXLVL_5; } set { _EDEXLVL_5 = value; OnPropertyChanged("EDEXLVL_5"); } }
        private string _EDEXLVL_6; public string EDEXLVL_6 { get { return _EDEXLVL_6; } set { _EDEXLVL_6 = value; OnPropertyChanged("EDEXLVL_6"); } }
        private string _EDEXLVL_7; public string EDEXLVL_7 { get { return _EDEXLVL_7; } set { _EDEXLVL_7 = value; OnPropertyChanged("EDEXLVL_7"); } }
        private string _EDEXLVL_8; public string EDEXLVL_8 { get { return _EDEXLVL_8; } set { _EDEXLVL_8 = value; OnPropertyChanged("EDEXLVL_8"); } }
        private string _EDEXLVL_9; public string EDEXLVL_9 { get { return _EDEXLVL_9; } set { _EDEXLVL_9 = value; OnPropertyChanged("EDEXLVL_9"); } }
        private string _EDEXLVL_10; public string EDEXLVL_10 { get { return _EDEXLVL_10; } set { _EDEXLVL_10 = value; OnPropertyChanged("EDEXLVL_10"); } }
        private string _EDEXLVL_11; public string EDEXLVL_11 { get { return _EDEXLVL_11; } set { _EDEXLVL_11 = value; OnPropertyChanged("EDEXLVL_11"); } }
        private string _EDEXLVL_12; public string EDEXLVL_12 { get { return _EDEXLVL_12; } set { _EDEXLVL_12 = value; OnPropertyChanged("EDEXLVL_12"); } }
        private string _EDEXLVL_13; public string EDEXLVL_13 { get { return _EDEXLVL_13; } set { _EDEXLVL_13 = value; OnPropertyChanged("EDEXLVL_13"); } }
        private string _EDEXLVL_14; public string EDEXLVL_14 { get { return _EDEXLVL_14; } set { _EDEXLVL_14 = value; OnPropertyChanged("EDEXLVL_14"); } }
        private string _EDEXLVL_15; public string EDEXLVL_15 { get { return _EDEXLVL_15; } set { _EDEXLVL_15 = value; OnPropertyChanged("EDEXLVL_15"); } }
        private string _EDEXLVL_16; public string EDEXLVL_16 { get { return _EDEXLVL_16; } set { _EDEXLVL_16 = value; OnPropertyChanged("EDEXLVL_16"); } }
        private string _EDEXLVL_17; public string EDEXLVL_17 { get { return _EDEXLVL_17; } set { _EDEXLVL_17 = value; OnPropertyChanged("EDEXLVL_17"); } }
        private string _EDEXLVL_18; public string EDEXLVL_18 { get { return _EDEXLVL_18; } set { _EDEXLVL_18 = value; OnPropertyChanged("EDEXLVL_18"); } }
        private string _EDEXLVL_19; public string EDEXLVL_19 { get { return _EDEXLVL_19; } set { _EDEXLVL_19 = value; OnPropertyChanged("EDEXLVL_19"); } }
        private string _EDEXLVL_20; public string EDEXLVL_20 { get { return _EDEXLVL_20; } set { _EDEXLVL_20 = value; OnPropertyChanged("EDEXLVL_20"); } }
        private string _EDEXLVL_21; public string EDEXLVL_21 { get { return _EDEXLVL_21; } set { _EDEXLVL_21 = value; OnPropertyChanged("EDEXLVL_21"); } }
        private string _EDEXLVL_22; public string EDEXLVL_22 { get { return _EDEXLVL_22; } set { _EDEXLVL_22 = value; OnPropertyChanged("EDEXLVL_22"); } }
        private string _EDEXLVL_23; public string EDEXLVL_23 { get { return _EDEXLVL_23; } set { _EDEXLVL_23 = value; OnPropertyChanged("EDEXLVL_23"); } }
        private string _EDEXLVL_24; public string EDEXLVL_24 { get { return _EDEXLVL_24; } set { _EDEXLVL_24 = value; OnPropertyChanged("EDEXLVL_24"); } }
        private string _EDEXLVL_25; public string EDEXLVL_25 { get { return _EDEXLVL_25; } set { _EDEXLVL_25 = value; OnPropertyChanged("EDEXLVL_25"); } }
        private string _EDEXLVL_26; public string EDEXLVL_26 { get { return _EDEXLVL_26; } set { _EDEXLVL_26 = value; OnPropertyChanged("EDEXLVL_26"); } }
        private string _EDEXLVL_27; public string EDEXLVL_27 { get { return _EDEXLVL_27; } set { _EDEXLVL_27 = value; OnPropertyChanged("EDEXLVL_27"); } }
        private string _EDEXLVL_28; public string EDEXLVL_28 { get { return _EDEXLVL_28; } set { _EDEXLVL_28 = value; OnPropertyChanged("EDEXLVL_28"); } }
        private string _EDEXLVL_29; public string EDEXLVL_29 { get { return _EDEXLVL_29; } set { _EDEXLVL_29 = value; OnPropertyChanged("EDEXLVL_29"); } }
        private string _EDEXLVL_30; public string EDEXLVL_30 { get { return _EDEXLVL_30; } set { _EDEXLVL_30 = value; OnPropertyChanged("EDEXLVL_30"); } }
        private string _EDEXLVL_31; public string EDEXLVL_31 { get { return _EDEXLVL_31; } set { _EDEXLVL_31 = value; OnPropertyChanged("EDEXLVL_31"); } }
        private string _EDEXLVL_32; public string EDEXLVL_32 { get { return _EDEXLVL_32; } set { _EDEXLVL_32 = value; OnPropertyChanged("EDEXLVL_32"); } }
        private string _EDEXLVL_33; public string EDEXLVL_33 { get { return _EDEXLVL_33; } set { _EDEXLVL_33 = value; OnPropertyChanged("EDEXLVL_33"); } }
        private string _EDEXLVL_34; public string EDEXLVL_34 { get { return _EDEXLVL_34; } set { _EDEXLVL_34 = value; OnPropertyChanged("EDEXLVL_34"); } }
        private string _EDEXLVL_35; public string EDEXLVL_35 { get { return _EDEXLVL_35; } set { _EDEXLVL_35 = value; OnPropertyChanged("EDEXLVL_35"); } }
        private string _EDEXLVL_36; public string EDEXLVL_36 { get { return _EDEXLVL_36; } set { _EDEXLVL_36 = value; OnPropertyChanged("EDEXLVL_36"); } }
        private string _EDEXLVL_37; public string EDEXLVL_37 { get { return _EDEXLVL_37; } set { _EDEXLVL_37 = value; OnPropertyChanged("EDEXLVL_37"); } }
        private string _EDEXLVL_38; public string EDEXLVL_38 { get { return _EDEXLVL_38; } set { _EDEXLVL_38 = value; OnPropertyChanged("EDEXLVL_38"); } }
        private string _EDEXLVL_39; public string EDEXLVL_39 { get { return _EDEXLVL_39; } set { _EDEXLVL_39 = value; OnPropertyChanged("EDEXLVL_39"); } }
        private string _EDEXLVL_40; public string EDEXLVL_40 { get { return _EDEXLVL_40; } set { _EDEXLVL_40 = value; OnPropertyChanged("EDEXLVL_40"); } }
        private string _EDEXLVL_41; public string EDEXLVL_41 { get { return _EDEXLVL_41; } set { _EDEXLVL_41 = value; OnPropertyChanged("EDEXLVL_41"); } }
        private string _EDEXLVL_42; public string EDEXLVL_42 { get { return _EDEXLVL_42; } set { _EDEXLVL_42 = value; OnPropertyChanged("EDEXLVL_42"); } }
        private string _EDEXLVL_43; public string EDEXLVL_43 { get { return _EDEXLVL_43; } set { _EDEXLVL_43 = value; OnPropertyChanged("EDEXLVL_43"); } }
        private string _EDEXLVL_44; public string EDEXLVL_44 { get { return _EDEXLVL_44; } set { _EDEXLVL_44 = value; OnPropertyChanged("EDEXLVL_44"); } }
        private string _EDEXLVL_45; public string EDEXLVL_45 { get { return _EDEXLVL_45; } set { _EDEXLVL_45 = value; OnPropertyChanged("EDEXLVL_45"); } }
        private string _EDEXLVL_46; public string EDEXLVL_46 { get { return _EDEXLVL_46; } set { _EDEXLVL_46 = value; OnPropertyChanged("EDEXLVL_46"); } }
        private string _EDEXLVL_47; public string EDEXLVL_47 { get { return _EDEXLVL_47; } set { _EDEXLVL_47 = value; OnPropertyChanged("EDEXLVL_47"); } }
        private string _EDEXLVL_48; public string EDEXLVL_48 { get { return _EDEXLVL_48; } set { _EDEXLVL_48 = value; OnPropertyChanged("EDEXLVL_48"); } }
        private string _EDEXLVL_49; public string EDEXLVL_49 { get { return _EDEXLVL_49; } set { _EDEXLVL_49 = value; OnPropertyChanged("EDEXLVL_49"); } }
        private string _EDEXLVL_50; public string EDEXLVL_50 { get { return _EDEXLVL_50; } set { _EDEXLVL_50 = value; OnPropertyChanged("EDEXLVL_50"); } }
        private string _EDEXLVL_51; public string EDEXLVL_51 { get { return _EDEXLVL_51; } set { _EDEXLVL_51 = value; OnPropertyChanged("EDEXLVL_51"); } }
        private string _EDEXLVL_52; public string EDEXLVL_52 { get { return _EDEXLVL_52; } set { _EDEXLVL_52 = value; OnPropertyChanged("EDEXLVL_52"); } }
        private string _EDEXLVL_53; public string EDEXLVL_53 { get { return _EDEXLVL_53; } set { _EDEXLVL_53 = value; OnPropertyChanged("EDEXLVL_53"); } }
        private string _EDEXLVL_54; public string EDEXLVL_54 { get { return _EDEXLVL_54; } set { _EDEXLVL_54 = value; OnPropertyChanged("EDEXLVL_54"); } }
        private string _EDEXLVL_55; public string EDEXLVL_55 { get { return _EDEXLVL_55; } set { _EDEXLVL_55 = value; OnPropertyChanged("EDEXLVL_55"); } }
        private string _EDEXLVL_56; public string EDEXLVL_56 { get { return _EDEXLVL_56; } set { _EDEXLVL_56 = value; OnPropertyChanged("EDEXLVL_56"); } }
        private string _EDEXLVL_57; public string EDEXLVL_57 { get { return _EDEXLVL_57; } set { _EDEXLVL_57 = value; OnPropertyChanged("EDEXLVL_57"); } }
        private string _EDEXLVL_58; public string EDEXLVL_58 { get { return _EDEXLVL_58; } set { _EDEXLVL_58 = value; OnPropertyChanged("EDEXLVL_58"); } }
        private string _EDEXLVL_59; public string EDEXLVL_59 { get { return _EDEXLVL_59; } set { _EDEXLVL_59 = value; OnPropertyChanged("EDEXLVL_59"); } }
        private string _EDEXLVL_60; public string EDEXLVL_60 { get { return _EDEXLVL_60; } set { _EDEXLVL_60 = value; OnPropertyChanged("EDEXLVL_60"); } }
        private string _EDEXLVL_61; public string EDEXLVL_61 { get { return _EDEXLVL_61; } set { _EDEXLVL_61 = value; OnPropertyChanged("EDEXLVL_61"); } }
        private string _EDEXLVL_62; public string EDEXLVL_62 { get { return _EDEXLVL_62; } set { _EDEXLVL_62 = value; OnPropertyChanged("EDEXLVL_62"); } }
        private string _EDEXLVL_63; public string EDEXLVL_63 { get { return _EDEXLVL_63; } set { _EDEXLVL_63 = value; OnPropertyChanged("EDEXLVL_63"); } }
        private string _EDEXLVL_64; public string EDEXLVL_64 { get { return _EDEXLVL_64; } set { _EDEXLVL_64 = value; OnPropertyChanged("EDEXLVL_64"); } }
        private string _EDEXLVL_65; public string EDEXLVL_65 { get { return _EDEXLVL_65; } set { _EDEXLVL_65 = value; OnPropertyChanged("EDEXLVL_65"); } }
        private string _EDEXLVL_66; public string EDEXLVL_66 { get { return _EDEXLVL_66; } set { _EDEXLVL_66 = value; OnPropertyChanged("EDEXLVL_66"); } }
        private string _EDEXLVL_67; public string EDEXLVL_67 { get { return _EDEXLVL_67; } set { _EDEXLVL_67 = value; OnPropertyChanged("EDEXLVL_67"); } }
        private string _EDEXLVL_68; public string EDEXLVL_68 { get { return _EDEXLVL_68; } set { _EDEXLVL_68 = value; OnPropertyChanged("EDEXLVL_68"); } }
        private string _EDEXLVL_69; public string EDEXLVL_69 { get { return _EDEXLVL_69; } set { _EDEXLVL_69 = value; OnPropertyChanged("EDEXLVL_69"); } }
        private string _EDEXLVL_70; public string EDEXLVL_70 { get { return _EDEXLVL_70; } set { _EDEXLVL_70 = value; OnPropertyChanged("EDEXLVL_70"); } }
        private string _EDEXLVL_71; public string EDEXLVL_71 { get { return _EDEXLVL_71; } set { _EDEXLVL_71 = value; OnPropertyChanged("EDEXLVL_71"); } }
        private string _EDEXLVL_72; public string EDEXLVL_72 { get { return _EDEXLVL_72; } set { _EDEXLVL_72 = value; OnPropertyChanged("EDEXLVL_72"); } }
        private string _EDEXLVL_73; public string EDEXLVL_73 { get { return _EDEXLVL_73; } set { _EDEXLVL_73 = value; OnPropertyChanged("EDEXLVL_73"); } }
        private string _EDEXLVL_74; public string EDEXLVL_74 { get { return _EDEXLVL_74; } set { _EDEXLVL_74 = value; OnPropertyChanged("EDEXLVL_74"); } }
        private string _EDEXLVL_75; public string EDEXLVL_75 { get { return _EDEXLVL_75; } set { _EDEXLVL_75 = value; OnPropertyChanged("EDEXLVL_75"); } }
        private string _EDEXLVL_76; public string EDEXLVL_76 { get { return _EDEXLVL_76; } set { _EDEXLVL_76 = value; OnPropertyChanged("EDEXLVL_76"); } }
        private string _EDEXLVL_77; public string EDEXLVL_77 { get { return _EDEXLVL_77; } set { _EDEXLVL_77 = value; OnPropertyChanged("EDEXLVL_77"); } }
        private string _EDEXLVL_78; public string EDEXLVL_78 { get { return _EDEXLVL_78; } set { _EDEXLVL_78 = value; OnPropertyChanged("EDEXLVL_78"); } }
        private string _EDEXLVL_79; public string EDEXLVL_79 { get { return _EDEXLVL_79; } set { _EDEXLVL_79 = value; OnPropertyChanged("EDEXLVL_79"); } }
        private string _EDEXLVL_80; public string EDEXLVL_80 { get { return _EDEXLVL_80; } set { _EDEXLVL_80 = value; OnPropertyChanged("EDEXLVL_80"); } }
        private string _EDEXLVL_81; public string EDEXLVL_81 { get { return _EDEXLVL_81; } set { _EDEXLVL_81 = value; OnPropertyChanged("EDEXLVL_81"); } }
        private string _EDEXLVL_82; public string EDEXLVL_82 { get { return _EDEXLVL_82; } set { _EDEXLVL_82 = value; OnPropertyChanged("EDEXLVL_82"); } }
        private string _EDEXLVL_83; public string EDEXLVL_83 { get { return _EDEXLVL_83; } set { _EDEXLVL_83 = value; OnPropertyChanged("EDEXLVL_83"); } }
        private string _EDEXLVL_84; public string EDEXLVL_84 { get { return _EDEXLVL_84; } set { _EDEXLVL_84 = value; OnPropertyChanged("EDEXLVL_84"); } }
        private string _EDEXLVL_85; public string EDEXLVL_85 { get { return _EDEXLVL_85; } set { _EDEXLVL_85 = value; OnPropertyChanged("EDEXLVL_85"); } }
        private string _EDEXLVL_86; public string EDEXLVL_86 { get { return _EDEXLVL_86; } set { _EDEXLVL_86 = value; OnPropertyChanged("EDEXLVL_86"); } }
        private string _EDEXLVL_87; public string EDEXLVL_87 { get { return _EDEXLVL_87; } set { _EDEXLVL_87 = value; OnPropertyChanged("EDEXLVL_87"); } }
        private string _EDEXLVL_88; public string EDEXLVL_88 { get { return _EDEXLVL_88; } set { _EDEXLVL_88 = value; OnPropertyChanged("EDEXLVL_88"); } }
        private string _EDEXLVL_89; public string EDEXLVL_89 { get { return _EDEXLVL_89; } set { _EDEXLVL_89 = value; OnPropertyChanged("EDEXLVL_89"); } }
        private string _EDEXLVL_90; public string EDEXLVL_90 { get { return _EDEXLVL_90; } set { _EDEXLVL_90 = value; OnPropertyChanged("EDEXLVL_90"); } }
        private string _EDEXLVL_91; public string EDEXLVL_91 { get { return _EDEXLVL_91; } set { _EDEXLVL_91 = value; OnPropertyChanged("EDEXLVL_91"); } }
        private string _EDEXLVL_92; public string EDEXLVL_92 { get { return _EDEXLVL_92; } set { _EDEXLVL_92 = value; OnPropertyChanged("EDEXLVL_92"); } }
        private string _EDEXLVL_93; public string EDEXLVL_93 { get { return _EDEXLVL_93; } set { _EDEXLVL_93 = value; OnPropertyChanged("EDEXLVL_93"); } }
        private string _EDEXLVL_94; public string EDEXLVL_94 { get { return _EDEXLVL_94; } set { _EDEXLVL_94 = value; OnPropertyChanged("EDEXLVL_94"); } }
        private string _EDEXLVL_95; public string EDEXLVL_95 { get { return _EDEXLVL_95; } set { _EDEXLVL_95 = value; OnPropertyChanged("EDEXLVL_95"); } }
        private string _EDEXLVL_96; public string EDEXLVL_96 { get { return _EDEXLVL_96; } set { _EDEXLVL_96 = value; OnPropertyChanged("EDEXLVL_96"); } }
        private string _EDEXLVL_97; public string EDEXLVL_97 { get { return _EDEXLVL_97; } set { _EDEXLVL_97 = value; OnPropertyChanged("EDEXLVL_97"); } }
        private string _EDEXLVL_98; public string EDEXLVL_98 { get { return _EDEXLVL_98; } set { _EDEXLVL_98 = value; OnPropertyChanged("EDEXLVL_98"); } }
        private string _EDEXLVL_99; public string EDEXLVL_99 { get { return _EDEXLVL_99; } set { _EDEXLVL_99 = value; OnPropertyChanged("EDEXLVL_99"); } }
        private string _EDEXLVL_100; public string EDEXLVL_100 { get { return _EDEXLVL_100; } set { _EDEXLVL_100 = value; OnPropertyChanged("EDEXLVL_100"); } }
        private string _EDEXLVL_101; public string EDEXLVL_101 { get { return _EDEXLVL_101; } set { _EDEXLVL_101 = value; OnPropertyChanged("EDEXLVL_101"); } }
        private string _EDEXLVL_102; public string EDEXLVL_102 { get { return _EDEXLVL_102; } set { _EDEXLVL_102 = value; OnPropertyChanged("EDEXLVL_102"); } }
        private string _EDEXLVL_103; public string EDEXLVL_103 { get { return _EDEXLVL_103; } set { _EDEXLVL_103 = value; OnPropertyChanged("EDEXLVL_103"); } }
        private string _EDEXLVL_104; public string EDEXLVL_104 { get { return _EDEXLVL_104; } set { _EDEXLVL_104 = value; OnPropertyChanged("EDEXLVL_104"); } }
        private string _EDEXLVL_105; public string EDEXLVL_105 { get { return _EDEXLVL_105; } set { _EDEXLVL_105 = value; OnPropertyChanged("EDEXLVL_105"); } }
        private string _EDEXLVL_106; public string EDEXLVL_106 { get { return _EDEXLVL_106; } set { _EDEXLVL_106 = value; OnPropertyChanged("EDEXLVL_106"); } }
        private string _EDEXLVL_107; public string EDEXLVL_107 { get { return _EDEXLVL_107; } set { _EDEXLVL_107 = value; OnPropertyChanged("EDEXLVL_107"); } }
        private string _EDEXLVL_108; public string EDEXLVL_108 { get { return _EDEXLVL_108; } set { _EDEXLVL_108 = value; OnPropertyChanged("EDEXLVL_108"); } }
        private string _EDEXLVL_109; public string EDEXLVL_109 { get { return _EDEXLVL_109; } set { _EDEXLVL_109 = value; OnPropertyChanged("EDEXLVL_109"); } }
        private string _EDEXLVL_110; public string EDEXLVL_110 { get { return _EDEXLVL_110; } set { _EDEXLVL_110 = value; OnPropertyChanged("EDEXLVL_110"); } }
        private string _EDEXLVL_111; public string EDEXLVL_111 { get { return _EDEXLVL_111; } set { _EDEXLVL_111 = value; OnPropertyChanged("EDEXLVL_111"); } }
        private string _EDEXLVL_112; public string EDEXLVL_112 { get { return _EDEXLVL_112; } set { _EDEXLVL_112 = value; OnPropertyChanged("EDEXLVL_112"); } }
        private string _EDEXLVL_113; public string EDEXLVL_113 { get { return _EDEXLVL_113; } set { _EDEXLVL_113 = value; OnPropertyChanged("EDEXLVL_113"); } }
        private string _EDEXLVL_114; public string EDEXLVL_114 { get { return _EDEXLVL_114; } set { _EDEXLVL_114 = value; OnPropertyChanged("EDEXLVL_114"); } }
        private string _EDEXLVL_115; public string EDEXLVL_115 { get { return _EDEXLVL_115; } set { _EDEXLVL_115 = value; OnPropertyChanged("EDEXLVL_115"); } }
        private string _EDEXLVL_116; public string EDEXLVL_116 { get { return _EDEXLVL_116; } set { _EDEXLVL_116 = value; OnPropertyChanged("EDEXLVL_116"); } }
        private string _EDEXLVL_117; public string EDEXLVL_117 { get { return _EDEXLVL_117; } set { _EDEXLVL_117 = value; OnPropertyChanged("EDEXLVL_117"); } }
        private string _EDEXLVL_118; public string EDEXLVL_118 { get { return _EDEXLVL_118; } set { _EDEXLVL_118 = value; OnPropertyChanged("EDEXLVL_118"); } }
        private string _EDEXLVL_119; public string EDEXLVL_119 { get { return _EDEXLVL_119; } set { _EDEXLVL_119 = value; OnPropertyChanged("EDEXLVL_119"); } }
        private string _EDEXLVL_120; public string EDEXLVL_120 { get { return _EDEXLVL_120; } set { _EDEXLVL_120 = value; OnPropertyChanged("EDEXLVL_120"); } }
        private string _EDEXLVL_121; public string EDEXLVL_121 { get { return _EDEXLVL_121; } set { _EDEXLVL_121 = value; OnPropertyChanged("EDEXLVL_121"); } }
        private string _EDEXLVL_122; public string EDEXLVL_122 { get { return _EDEXLVL_122; } set { _EDEXLVL_122 = value; OnPropertyChanged("EDEXLVL_122"); } }
        private string _EDEXLVL_123; public string EDEXLVL_123 { get { return _EDEXLVL_123; } set { _EDEXLVL_123 = value; OnPropertyChanged("EDEXLVL_123"); } }
        private string _EDEXLVL_124; public string EDEXLVL_124 { get { return _EDEXLVL_124; } set { _EDEXLVL_124 = value; OnPropertyChanged("EDEXLVL_124"); } }
        private string _EDEXLVL_125; public string EDEXLVL_125 { get { return _EDEXLVL_125; } set { _EDEXLVL_125 = value; OnPropertyChanged("EDEXLVL_125"); } }
        private string _EDEXLVL_126; public string EDEXLVL_126 { get { return _EDEXLVL_126; } set { _EDEXLVL_126 = value; OnPropertyChanged("EDEXLVL_126"); } }
        private string _EDEXLVL_127; public string EDEXLVL_127 { get { return _EDEXLVL_127; } set { _EDEXLVL_127 = value; OnPropertyChanged("EDEXLVL_127"); } }
        private string _EDEXLVL_128; public string EDEXLVL_128 { get { return _EDEXLVL_128; } set { _EDEXLVL_128 = value; OnPropertyChanged("EDEXLVL_128"); } }
        private string _EDEXLVL_129; public string EDEXLVL_129 { get { return _EDEXLVL_129; } set { _EDEXLVL_129 = value; OnPropertyChanged("EDEXLVL_129"); } }
        private string _EDEXLVL_130; public string EDEXLVL_130 { get { return _EDEXLVL_130; } set { _EDEXLVL_130 = value; OnPropertyChanged("EDEXLVL_130"); } }
        private string _EDEXLVL_131; public string EDEXLVL_131 { get { return _EDEXLVL_131; } set { _EDEXLVL_131 = value; OnPropertyChanged("EDEXLVL_131"); } }
        private string _EDEXLVL_132; public string EDEXLVL_132 { get { return _EDEXLVL_132; } set { _EDEXLVL_132 = value; OnPropertyChanged("EDEXLVL_132"); } }
        private string _EDEXLVL_133; public string EDEXLVL_133 { get { return _EDEXLVL_133; } set { _EDEXLVL_133 = value; OnPropertyChanged("EDEXLVL_133"); } }
        private string _EDEXLVL_134; public string EDEXLVL_134 { get { return _EDEXLVL_134; } set { _EDEXLVL_134 = value; OnPropertyChanged("EDEXLVL_134"); } }
        private string _EDEXLVL_135; public string EDEXLVL_135 { get { return _EDEXLVL_135; } set { _EDEXLVL_135 = value; OnPropertyChanged("EDEXLVL_135"); } }
        private string _EDEXLVL_136; public string EDEXLVL_136 { get { return _EDEXLVL_136; } set { _EDEXLVL_136 = value; OnPropertyChanged("EDEXLVL_136"); } }
        private string _EDEXLVL_137; public string EDEXLVL_137 { get { return _EDEXLVL_137; } set { _EDEXLVL_137 = value; OnPropertyChanged("EDEXLVL_137"); } }
        private string _EDEXLVL_138; public string EDEXLVL_138 { get { return _EDEXLVL_138; } set { _EDEXLVL_138 = value; OnPropertyChanged("EDEXLVL_138"); } }
        private string _EDEXLVL_139; public string EDEXLVL_139 { get { return _EDEXLVL_139; } set { _EDEXLVL_139 = value; OnPropertyChanged("EDEXLVL_139"); } }
        private string _EDEXLVL_140; public string EDEXLVL_140 { get { return _EDEXLVL_140; } set { _EDEXLVL_140 = value; OnPropertyChanged("EDEXLVL_140"); } }
        private string _EDEXLVL_141; public string EDEXLVL_141 { get { return _EDEXLVL_141; } set { _EDEXLVL_141 = value; OnPropertyChanged("EDEXLVL_141"); } }
        private string _EDEXLVL_142; public string EDEXLVL_142 { get { return _EDEXLVL_142; } set { _EDEXLVL_142 = value; OnPropertyChanged("EDEXLVL_142"); } }
        private string _EDEXLVL_143; public string EDEXLVL_143 { get { return _EDEXLVL_143; } set { _EDEXLVL_143 = value; OnPropertyChanged("EDEXLVL_143"); } }

        private string _EXCOLOR_0; public string EXCOLOR_0 { get { return _EXCOLOR_0; } set { _EXCOLOR_0 = value; OnPropertyChanged("EXCOLOR_0"); } }
        private string _EXCOLOR_1; public string EXCOLOR_1 { get { return _EXCOLOR_1; } set { _EXCOLOR_1 = value; OnPropertyChanged("EXCOLOR_1"); } }
        private string _EXCOLOR_2; public string EXCOLOR_2 { get { return _EXCOLOR_2; } set { _EXCOLOR_2 = value; OnPropertyChanged("EXCOLOR_2"); } }
        private string _EXCOLOR_3; public string EXCOLOR_3 { get { return _EXCOLOR_3; } set { _EXCOLOR_3 = value; OnPropertyChanged("EXCOLOR_3"); } }
        private string _EXCOLOR_4; public string EXCOLOR_4 { get { return _EXCOLOR_4; } set { _EXCOLOR_4 = value; OnPropertyChanged("EXCOLOR_4"); } }
        private string _EXCOLOR_5; public string EXCOLOR_5 { get { return _EXCOLOR_5; } set { _EXCOLOR_5 = value; OnPropertyChanged("EXCOLOR_5"); } }
        private string _EXCOLOR_6; public string EXCOLOR_6 { get { return _EXCOLOR_6; } set { _EXCOLOR_6 = value; OnPropertyChanged("EXCOLOR_6"); } }
        private string _EXCOLOR_7; public string EXCOLOR_7 { get { return _EXCOLOR_7; } set { _EXCOLOR_7 = value; OnPropertyChanged("EXCOLOR_7"); } }
        private string _EXCOLOR_8; public string EXCOLOR_8 { get { return _EXCOLOR_8; } set { _EXCOLOR_8 = value; OnPropertyChanged("EXCOLOR_8"); } }
        private string _EXCOLOR_9; public string EXCOLOR_9 { get { return _EXCOLOR_9; } set { _EXCOLOR_9 = value; OnPropertyChanged("EXCOLOR_9"); } }
        private string _EXCOLOR_10; public string EXCOLOR_10 { get { return _EXCOLOR_10; } set { _EXCOLOR_10 = value; OnPropertyChanged("EXCOLOR_10"); } }
        private string _EXCOLOR_11; public string EXCOLOR_11 { get { return _EXCOLOR_11; } set { _EXCOLOR_11 = value; OnPropertyChanged("EXCOLOR_11"); } }
        private string _EXCOLOR_12; public string EXCOLOR_12 { get { return _EXCOLOR_12; } set { _EXCOLOR_12 = value; OnPropertyChanged("EXCOLOR_12"); } }
        private string _EXCOLOR_13; public string EXCOLOR_13 { get { return _EXCOLOR_13; } set { _EXCOLOR_13 = value; OnPropertyChanged("EXCOLOR_13"); } }
        private string _EXCOLOR_14; public string EXCOLOR_14 { get { return _EXCOLOR_14; } set { _EXCOLOR_14 = value; OnPropertyChanged("EXCOLOR_14"); } }
        private string _EXCOLOR_15; public string EXCOLOR_15 { get { return _EXCOLOR_15; } set { _EXCOLOR_15 = value; OnPropertyChanged("EXCOLOR_15"); } }
        private string _EXCOLOR_16; public string EXCOLOR_16 { get { return _EXCOLOR_16; } set { _EXCOLOR_16 = value; OnPropertyChanged("EXCOLOR_16"); } }
        private string _EXCOLOR_17; public string EXCOLOR_17 { get { return _EXCOLOR_17; } set { _EXCOLOR_17 = value; OnPropertyChanged("EXCOLOR_17"); } }
        private string _EXCOLOR_18; public string EXCOLOR_18 { get { return _EXCOLOR_18; } set { _EXCOLOR_18 = value; OnPropertyChanged("EXCOLOR_18"); } }
        private string _EXCOLOR_19; public string EXCOLOR_19 { get { return _EXCOLOR_19; } set { _EXCOLOR_19 = value; OnPropertyChanged("EXCOLOR_19"); } }
        private string _EXCOLOR_20; public string EXCOLOR_20 { get { return _EXCOLOR_20; } set { _EXCOLOR_20 = value; OnPropertyChanged("EXCOLOR_20"); } }
        private string _EXCOLOR_21; public string EXCOLOR_21 { get { return _EXCOLOR_21; } set { _EXCOLOR_21 = value; OnPropertyChanged("EXCOLOR_21"); } }
        private string _EXCOLOR_22; public string EXCOLOR_22 { get { return _EXCOLOR_22; } set { _EXCOLOR_22 = value; OnPropertyChanged("EXCOLOR_22"); } }
        private string _EXCOLOR_23; public string EXCOLOR_23 { get { return _EXCOLOR_23; } set { _EXCOLOR_23 = value; OnPropertyChanged("EXCOLOR_23"); } }
        private string _EXCOLOR_24; public string EXCOLOR_24 { get { return _EXCOLOR_24; } set { _EXCOLOR_24 = value; OnPropertyChanged("EXCOLOR_24"); } }
        private string _EXCOLOR_25; public string EXCOLOR_25 { get { return _EXCOLOR_25; } set { _EXCOLOR_25 = value; OnPropertyChanged("EXCOLOR_25"); } }
        private string _EXCOLOR_26; public string EXCOLOR_26 { get { return _EXCOLOR_26; } set { _EXCOLOR_26 = value; OnPropertyChanged("EXCOLOR_26"); } }
        private string _EXCOLOR_27; public string EXCOLOR_27 { get { return _EXCOLOR_27; } set { _EXCOLOR_27 = value; OnPropertyChanged("EXCOLOR_27"); } }
        private string _EXCOLOR_28; public string EXCOLOR_28 { get { return _EXCOLOR_28; } set { _EXCOLOR_28 = value; OnPropertyChanged("EXCOLOR_28"); } }
        private string _EXCOLOR_29; public string EXCOLOR_29 { get { return _EXCOLOR_29; } set { _EXCOLOR_29 = value; OnPropertyChanged("EXCOLOR_29"); } }
        private string _EXCOLOR_30; public string EXCOLOR_30 { get { return _EXCOLOR_30; } set { _EXCOLOR_30 = value; OnPropertyChanged("EXCOLOR_30"); } }
        private string _EXCOLOR_31; public string EXCOLOR_31 { get { return _EXCOLOR_31; } set { _EXCOLOR_31 = value; OnPropertyChanged("EXCOLOR_31"); } }
        private string _EXCOLOR_32; public string EXCOLOR_32 { get { return _EXCOLOR_32; } set { _EXCOLOR_32 = value; OnPropertyChanged("EXCOLOR_32"); } }
        private string _EXCOLOR_33; public string EXCOLOR_33 { get { return _EXCOLOR_33; } set { _EXCOLOR_33 = value; OnPropertyChanged("EXCOLOR_33"); } }
        private string _EXCOLOR_34; public string EXCOLOR_34 { get { return _EXCOLOR_34; } set { _EXCOLOR_34 = value; OnPropertyChanged("EXCOLOR_34"); } }
        private string _EXCOLOR_35; public string EXCOLOR_35 { get { return _EXCOLOR_35; } set { _EXCOLOR_35 = value; OnPropertyChanged("EXCOLOR_35"); } }
        private string _EXCOLOR_36; public string EXCOLOR_36 { get { return _EXCOLOR_36; } set { _EXCOLOR_36 = value; OnPropertyChanged("EXCOLOR_36"); } }
        private string _EXCOLOR_37; public string EXCOLOR_37 { get { return _EXCOLOR_37; } set { _EXCOLOR_37 = value; OnPropertyChanged("EXCOLOR_37"); } }
        private string _EXCOLOR_38; public string EXCOLOR_38 { get { return _EXCOLOR_38; } set { _EXCOLOR_38 = value; OnPropertyChanged("EXCOLOR_38"); } }
        private string _EXCOLOR_39; public string EXCOLOR_39 { get { return _EXCOLOR_39; } set { _EXCOLOR_39 = value; OnPropertyChanged("EXCOLOR_39"); } }
        private string _EXCOLOR_40; public string EXCOLOR_40 { get { return _EXCOLOR_40; } set { _EXCOLOR_40 = value; OnPropertyChanged("EXCOLOR_40"); } }
        private string _EXCOLOR_41; public string EXCOLOR_41 { get { return _EXCOLOR_41; } set { _EXCOLOR_41 = value; OnPropertyChanged("EXCOLOR_41"); } }
        private string _EXCOLOR_42; public string EXCOLOR_42 { get { return _EXCOLOR_42; } set { _EXCOLOR_42 = value; OnPropertyChanged("EXCOLOR_42"); } }
        private string _EXCOLOR_43; public string EXCOLOR_43 { get { return _EXCOLOR_43; } set { _EXCOLOR_43 = value; OnPropertyChanged("EXCOLOR_43"); } }
        private string _EXCOLOR_44; public string EXCOLOR_44 { get { return _EXCOLOR_44; } set { _EXCOLOR_44 = value; OnPropertyChanged("EXCOLOR_44"); } }
        private string _EXCOLOR_45; public string EXCOLOR_45 { get { return _EXCOLOR_45; } set { _EXCOLOR_45 = value; OnPropertyChanged("EXCOLOR_45"); } }
        private string _EXCOLOR_46; public string EXCOLOR_46 { get { return _EXCOLOR_46; } set { _EXCOLOR_46 = value; OnPropertyChanged("EXCOLOR_46"); } }
        private string _EXCOLOR_47; public string EXCOLOR_47 { get { return _EXCOLOR_47; } set { _EXCOLOR_47 = value; OnPropertyChanged("EXCOLOR_47"); } }
        private string _EXCOLOR_48; public string EXCOLOR_48 { get { return _EXCOLOR_48; } set { _EXCOLOR_48 = value; OnPropertyChanged("EXCOLOR_48"); } }
        private string _EXCOLOR_49; public string EXCOLOR_49 { get { return _EXCOLOR_49; } set { _EXCOLOR_49 = value; OnPropertyChanged("EXCOLOR_49"); } }
        private string _EXCOLOR_50; public string EXCOLOR_50 { get { return _EXCOLOR_50; } set { _EXCOLOR_50 = value; OnPropertyChanged("EXCOLOR_50"); } }
        private string _EXCOLOR_51; public string EXCOLOR_51 { get { return _EXCOLOR_51; } set { _EXCOLOR_51 = value; OnPropertyChanged("EXCOLOR_51"); } }
        private string _EXCOLOR_52; public string EXCOLOR_52 { get { return _EXCOLOR_52; } set { _EXCOLOR_52 = value; OnPropertyChanged("EXCOLOR_52"); } }
        private string _EXCOLOR_53; public string EXCOLOR_53 { get { return _EXCOLOR_53; } set { _EXCOLOR_53 = value; OnPropertyChanged("EXCOLOR_53"); } }
        private string _EXCOLOR_54; public string EXCOLOR_54 { get { return _EXCOLOR_54; } set { _EXCOLOR_54 = value; OnPropertyChanged("EXCOLOR_54"); } }
        private string _EXCOLOR_55; public string EXCOLOR_55 { get { return _EXCOLOR_55; } set { _EXCOLOR_55 = value; OnPropertyChanged("EXCOLOR_55"); } }
        private string _EXCOLOR_56; public string EXCOLOR_56 { get { return _EXCOLOR_56; } set { _EXCOLOR_56 = value; OnPropertyChanged("EXCOLOR_56"); } }
        private string _EXCOLOR_57; public string EXCOLOR_57 { get { return _EXCOLOR_57; } set { _EXCOLOR_57 = value; OnPropertyChanged("EXCOLOR_57"); } }
        private string _EXCOLOR_58; public string EXCOLOR_58 { get { return _EXCOLOR_58; } set { _EXCOLOR_58 = value; OnPropertyChanged("EXCOLOR_58"); } }
        private string _EXCOLOR_59; public string EXCOLOR_59 { get { return _EXCOLOR_59; } set { _EXCOLOR_59 = value; OnPropertyChanged("EXCOLOR_59"); } }
        private string _EXCOLOR_60; public string EXCOLOR_60 { get { return _EXCOLOR_60; } set { _EXCOLOR_60 = value; OnPropertyChanged("EXCOLOR_60"); } }
        private string _EXCOLOR_61; public string EXCOLOR_61 { get { return _EXCOLOR_61; } set { _EXCOLOR_61 = value; OnPropertyChanged("EXCOLOR_61"); } }
        private string _EXCOLOR_62; public string EXCOLOR_62 { get { return _EXCOLOR_62; } set { _EXCOLOR_62 = value; OnPropertyChanged("EXCOLOR_62"); } }
        private string _EXCOLOR_63; public string EXCOLOR_63 { get { return _EXCOLOR_63; } set { _EXCOLOR_63 = value; OnPropertyChanged("EXCOLOR_63"); } }
        private string _EXCOLOR_64; public string EXCOLOR_64 { get { return _EXCOLOR_64; } set { _EXCOLOR_64 = value; OnPropertyChanged("EXCOLOR_64"); } }
        private string _EXCOLOR_65; public string EXCOLOR_65 { get { return _EXCOLOR_65; } set { _EXCOLOR_65 = value; OnPropertyChanged("EXCOLOR_65"); } }
        private string _EXCOLOR_66; public string EXCOLOR_66 { get { return _EXCOLOR_66; } set { _EXCOLOR_66 = value; OnPropertyChanged("EXCOLOR_66"); } }
        private string _EXCOLOR_67; public string EXCOLOR_67 { get { return _EXCOLOR_67; } set { _EXCOLOR_67 = value; OnPropertyChanged("EXCOLOR_67"); } }
        private string _EXCOLOR_68; public string EXCOLOR_68 { get { return _EXCOLOR_68; } set { _EXCOLOR_68 = value; OnPropertyChanged("EXCOLOR_68"); } }
        private string _EXCOLOR_69; public string EXCOLOR_69 { get { return _EXCOLOR_69; } set { _EXCOLOR_69 = value; OnPropertyChanged("EXCOLOR_69"); } }
        private string _EXCOLOR_70; public string EXCOLOR_70 { get { return _EXCOLOR_70; } set { _EXCOLOR_70 = value; OnPropertyChanged("EXCOLOR_70"); } }
        private string _EXCOLOR_71; public string EXCOLOR_71 { get { return _EXCOLOR_71; } set { _EXCOLOR_71 = value; OnPropertyChanged("EXCOLOR_71"); } }
        private string _EXCOLOR_72; public string EXCOLOR_72 { get { return _EXCOLOR_72; } set { _EXCOLOR_72 = value; OnPropertyChanged("EXCOLOR_72"); } }
        private string _EXCOLOR_73; public string EXCOLOR_73 { get { return _EXCOLOR_73; } set { _EXCOLOR_73 = value; OnPropertyChanged("EXCOLOR_73"); } }
        private string _EXCOLOR_74; public string EXCOLOR_74 { get { return _EXCOLOR_74; } set { _EXCOLOR_74 = value; OnPropertyChanged("EXCOLOR_74"); } }
        private string _EXCOLOR_75; public string EXCOLOR_75 { get { return _EXCOLOR_75; } set { _EXCOLOR_75 = value; OnPropertyChanged("EXCOLOR_75"); } }
        private string _EXCOLOR_76; public string EXCOLOR_76 { get { return _EXCOLOR_76; } set { _EXCOLOR_76 = value; OnPropertyChanged("EXCOLOR_76"); } }
        private string _EXCOLOR_77; public string EXCOLOR_77 { get { return _EXCOLOR_77; } set { _EXCOLOR_77 = value; OnPropertyChanged("EXCOLOR_77"); } }
        private string _EXCOLOR_78; public string EXCOLOR_78 { get { return _EXCOLOR_78; } set { _EXCOLOR_78 = value; OnPropertyChanged("EXCOLOR_78"); } }
        private string _EXCOLOR_79; public string EXCOLOR_79 { get { return _EXCOLOR_79; } set { _EXCOLOR_79 = value; OnPropertyChanged("EXCOLOR_79"); } }
        private string _EXCOLOR_80; public string EXCOLOR_80 { get { return _EXCOLOR_80; } set { _EXCOLOR_80 = value; OnPropertyChanged("EXCOLOR_80"); } }
        private string _EXCOLOR_81; public string EXCOLOR_81 { get { return _EXCOLOR_81; } set { _EXCOLOR_81 = value; OnPropertyChanged("EXCOLOR_81"); } }
        private string _EXCOLOR_82; public string EXCOLOR_82 { get { return _EXCOLOR_82; } set { _EXCOLOR_82 = value; OnPropertyChanged("EXCOLOR_82"); } }
        private string _EXCOLOR_83; public string EXCOLOR_83 { get { return _EXCOLOR_83; } set { _EXCOLOR_83 = value; OnPropertyChanged("EXCOLOR_83"); } }
        private string _EXCOLOR_84; public string EXCOLOR_84 { get { return _EXCOLOR_84; } set { _EXCOLOR_84 = value; OnPropertyChanged("EXCOLOR_84"); } }
        private string _EXCOLOR_85; public string EXCOLOR_85 { get { return _EXCOLOR_85; } set { _EXCOLOR_85 = value; OnPropertyChanged("EXCOLOR_85"); } }
        private string _EXCOLOR_86; public string EXCOLOR_86 { get { return _EXCOLOR_86; } set { _EXCOLOR_86 = value; OnPropertyChanged("EXCOLOR_86"); } }
        private string _EXCOLOR_87; public string EXCOLOR_87 { get { return _EXCOLOR_87; } set { _EXCOLOR_87 = value; OnPropertyChanged("EXCOLOR_87"); } }
        private string _EXCOLOR_88; public string EXCOLOR_88 { get { return _EXCOLOR_88; } set { _EXCOLOR_88 = value; OnPropertyChanged("EXCOLOR_88"); } }
        private string _EXCOLOR_89; public string EXCOLOR_89 { get { return _EXCOLOR_89; } set { _EXCOLOR_89 = value; OnPropertyChanged("EXCOLOR_89"); } }
        private string _EXCOLOR_90; public string EXCOLOR_90 { get { return _EXCOLOR_90; } set { _EXCOLOR_90 = value; OnPropertyChanged("EXCOLOR_90"); } }
        private string _EXCOLOR_91; public string EXCOLOR_91 { get { return _EXCOLOR_91; } set { _EXCOLOR_91 = value; OnPropertyChanged("EXCOLOR_91"); } }
        private string _EXCOLOR_92; public string EXCOLOR_92 { get { return _EXCOLOR_92; } set { _EXCOLOR_92 = value; OnPropertyChanged("EXCOLOR_92"); } }
        private string _EXCOLOR_93; public string EXCOLOR_93 { get { return _EXCOLOR_93; } set { _EXCOLOR_93 = value; OnPropertyChanged("EXCOLOR_93"); } }
        private string _EXCOLOR_94; public string EXCOLOR_94 { get { return _EXCOLOR_94; } set { _EXCOLOR_94 = value; OnPropertyChanged("EXCOLOR_94"); } }
        private string _EXCOLOR_95; public string EXCOLOR_95 { get { return _EXCOLOR_95; } set { _EXCOLOR_95 = value; OnPropertyChanged("EXCOLOR_95"); } }
        private string _EXCOLOR_96; public string EXCOLOR_96 { get { return _EXCOLOR_96; } set { _EXCOLOR_96 = value; OnPropertyChanged("EXCOLOR_96"); } }
        private string _EXCOLOR_97; public string EXCOLOR_97 { get { return _EXCOLOR_97; } set { _EXCOLOR_97 = value; OnPropertyChanged("EXCOLOR_97"); } }
        private string _EXCOLOR_98; public string EXCOLOR_98 { get { return _EXCOLOR_98; } set { _EXCOLOR_98 = value; OnPropertyChanged("EXCOLOR_98"); } }
        private string _EXCOLOR_99; public string EXCOLOR_99 { get { return _EXCOLOR_99; } set { _EXCOLOR_99 = value; OnPropertyChanged("EXCOLOR_99"); } }
        private string _EXCOLOR_100; public string EXCOLOR_100 { get { return _EXCOLOR_100; } set { _EXCOLOR_100 = value; OnPropertyChanged("EXCOLOR_100"); } }
        private string _EXCOLOR_101; public string EXCOLOR_101 { get { return _EXCOLOR_101; } set { _EXCOLOR_101 = value; OnPropertyChanged("EXCOLOR_101"); } }
        private string _EXCOLOR_102; public string EXCOLOR_102 { get { return _EXCOLOR_102; } set { _EXCOLOR_102 = value; OnPropertyChanged("EXCOLOR_102"); } }
        private string _EXCOLOR_103; public string EXCOLOR_103 { get { return _EXCOLOR_103; } set { _EXCOLOR_103 = value; OnPropertyChanged("EXCOLOR_103"); } }
        private string _EXCOLOR_104; public string EXCOLOR_104 { get { return _EXCOLOR_104; } set { _EXCOLOR_104 = value; OnPropertyChanged("EXCOLOR_104"); } }
        private string _EXCOLOR_105; public string EXCOLOR_105 { get { return _EXCOLOR_105; } set { _EXCOLOR_105 = value; OnPropertyChanged("EXCOLOR_105"); } }
        private string _EXCOLOR_106; public string EXCOLOR_106 { get { return _EXCOLOR_106; } set { _EXCOLOR_106 = value; OnPropertyChanged("EXCOLOR_106"); } }
        private string _EXCOLOR_107; public string EXCOLOR_107 { get { return _EXCOLOR_107; } set { _EXCOLOR_107 = value; OnPropertyChanged("EXCOLOR_107"); } }
        private string _EXCOLOR_108; public string EXCOLOR_108 { get { return _EXCOLOR_108; } set { _EXCOLOR_108 = value; OnPropertyChanged("EXCOLOR_108"); } }
        private string _EXCOLOR_109; public string EXCOLOR_109 { get { return _EXCOLOR_109; } set { _EXCOLOR_109 = value; OnPropertyChanged("EXCOLOR_109"); } }
        private string _EXCOLOR_110; public string EXCOLOR_110 { get { return _EXCOLOR_110; } set { _EXCOLOR_110 = value; OnPropertyChanged("EXCOLOR_110"); } }
        private string _EXCOLOR_111; public string EXCOLOR_111 { get { return _EXCOLOR_111; } set { _EXCOLOR_111 = value; OnPropertyChanged("EXCOLOR_111"); } }
        private string _EXCOLOR_112; public string EXCOLOR_112 { get { return _EXCOLOR_112; } set { _EXCOLOR_112 = value; OnPropertyChanged("EXCOLOR_112"); } }
        private string _EXCOLOR_113; public string EXCOLOR_113 { get { return _EXCOLOR_113; } set { _EXCOLOR_113 = value; OnPropertyChanged("EXCOLOR_113"); } }
        private string _EXCOLOR_114; public string EXCOLOR_114 { get { return _EXCOLOR_114; } set { _EXCOLOR_114 = value; OnPropertyChanged("EXCOLOR_114"); } }
        private string _EXCOLOR_115; public string EXCOLOR_115 { get { return _EXCOLOR_115; } set { _EXCOLOR_115 = value; OnPropertyChanged("EXCOLOR_115"); } }
        private string _EXCOLOR_116; public string EXCOLOR_116 { get { return _EXCOLOR_116; } set { _EXCOLOR_116 = value; OnPropertyChanged("EXCOLOR_116"); } }
        private string _EXCOLOR_117; public string EXCOLOR_117 { get { return _EXCOLOR_117; } set { _EXCOLOR_117 = value; OnPropertyChanged("EXCOLOR_117"); } }
        private string _EXCOLOR_118; public string EXCOLOR_118 { get { return _EXCOLOR_118; } set { _EXCOLOR_118 = value; OnPropertyChanged("EXCOLOR_118"); } }
        private string _EXCOLOR_119; public string EXCOLOR_119 { get { return _EXCOLOR_119; } set { _EXCOLOR_119 = value; OnPropertyChanged("EXCOLOR_119"); } }
        private string _EXCOLOR_120; public string EXCOLOR_120 { get { return _EXCOLOR_120; } set { _EXCOLOR_120 = value; OnPropertyChanged("EXCOLOR_120"); } }
        private string _EXCOLOR_121; public string EXCOLOR_121 { get { return _EXCOLOR_121; } set { _EXCOLOR_121 = value; OnPropertyChanged("EXCOLOR_121"); } }
        private string _EXCOLOR_122; public string EXCOLOR_122 { get { return _EXCOLOR_122; } set { _EXCOLOR_122 = value; OnPropertyChanged("EXCOLOR_122"); } }
        private string _EXCOLOR_123; public string EXCOLOR_123 { get { return _EXCOLOR_123; } set { _EXCOLOR_123 = value; OnPropertyChanged("EXCOLOR_123"); } }
        private string _EXCOLOR_124; public string EXCOLOR_124 { get { return _EXCOLOR_124; } set { _EXCOLOR_124 = value; OnPropertyChanged("EXCOLOR_124"); } }
        private string _EXCOLOR_125; public string EXCOLOR_125 { get { return _EXCOLOR_125; } set { _EXCOLOR_125 = value; OnPropertyChanged("EXCOLOR_125"); } }
        private string _EXCOLOR_126; public string EXCOLOR_126 { get { return _EXCOLOR_126; } set { _EXCOLOR_126 = value; OnPropertyChanged("EXCOLOR_126"); } }
        private string _EXCOLOR_127; public string EXCOLOR_127 { get { return _EXCOLOR_127; } set { _EXCOLOR_127 = value; OnPropertyChanged("EXCOLOR_127"); } }
        private string _EXCOLOR_128; public string EXCOLOR_128 { get { return _EXCOLOR_128; } set { _EXCOLOR_128 = value; OnPropertyChanged("EXCOLOR_128"); } }
        private string _EXCOLOR_129; public string EXCOLOR_129 { get { return _EXCOLOR_129; } set { _EXCOLOR_129 = value; OnPropertyChanged("EXCOLOR_129"); } }
        private string _EXCOLOR_130; public string EXCOLOR_130 { get { return _EXCOLOR_130; } set { _EXCOLOR_130 = value; OnPropertyChanged("EXCOLOR_130"); } }
        private string _EXCOLOR_131; public string EXCOLOR_131 { get { return _EXCOLOR_131; } set { _EXCOLOR_131 = value; OnPropertyChanged("EXCOLOR_131"); } }
        private string _EXCOLOR_132; public string EXCOLOR_132 { get { return _EXCOLOR_132; } set { _EXCOLOR_132 = value; OnPropertyChanged("EXCOLOR_132"); } }
        private string _EXCOLOR_133; public string EXCOLOR_133 { get { return _EXCOLOR_133; } set { _EXCOLOR_133 = value; OnPropertyChanged("EXCOLOR_133"); } }
        private string _EXCOLOR_134; public string EXCOLOR_134 { get { return _EXCOLOR_134; } set { _EXCOLOR_134 = value; OnPropertyChanged("EXCOLOR_134"); } }
        private string _EXCOLOR_135; public string EXCOLOR_135 { get { return _EXCOLOR_135; } set { _EXCOLOR_135 = value; OnPropertyChanged("EXCOLOR_135"); } }
        private string _EXCOLOR_136; public string EXCOLOR_136 { get { return _EXCOLOR_136; } set { _EXCOLOR_136 = value; OnPropertyChanged("EXCOLOR_136"); } }
        private string _EXCOLOR_137; public string EXCOLOR_137 { get { return _EXCOLOR_137; } set { _EXCOLOR_137 = value; OnPropertyChanged("EXCOLOR_137"); } }
        private string _EXCOLOR_138; public string EXCOLOR_138 { get { return _EXCOLOR_138; } set { _EXCOLOR_138 = value; OnPropertyChanged("EXCOLOR_138"); } }
        private string _EXCOLOR_139; public string EXCOLOR_139 { get { return _EXCOLOR_139; } set { _EXCOLOR_139 = value; OnPropertyChanged("EXCOLOR_139"); } }
        private string _EXCOLOR_140; public string EXCOLOR_140 { get { return _EXCOLOR_140; } set { _EXCOLOR_140 = value; OnPropertyChanged("EXCOLOR_140"); } }
        private string _EXCOLOR_141; public string EXCOLOR_141 { get { return _EXCOLOR_141; } set { _EXCOLOR_141 = value; OnPropertyChanged("EXCOLOR_141"); } }
        private string _EXCOLOR_142; public string EXCOLOR_142 { get { return _EXCOLOR_142; } set { _EXCOLOR_142 = value; OnPropertyChanged("EXCOLOR_142"); } }
        private string _EXCOLOR_143; public string EXCOLOR_143 { get { return _EXCOLOR_143; } set { _EXCOLOR_143 = value; OnPropertyChanged("EXCOLOR_143"); } }

        private string _FLW_0; public string FLW_0 { get { return _FLW_0; } set { _FLW_0 = value; OnPropertyChanged("FLW_0"); } }
        private string _FLW_1; public string FLW_1 { get { return _FLW_1; } set { _FLW_1 = value; OnPropertyChanged("FLW_1"); } }
        private string _FLW_2; public string FLW_2 { get { return _FLW_2; } set { _FLW_2 = value; OnPropertyChanged("FLW_2"); } }
        private string _FLW_3; public string FLW_3 { get { return _FLW_3; } set { _FLW_3 = value; OnPropertyChanged("FLW_3"); } }
        private string _FLW_4; public string FLW_4 { get { return _FLW_4; } set { _FLW_4 = value; OnPropertyChanged("FLW_4"); } }
        private string _FLW_5; public string FLW_5 { get { return _FLW_5; } set { _FLW_5 = value; OnPropertyChanged("FLW_5"); } }
        private string _FLW_6; public string FLW_6 { get { return _FLW_6; } set { _FLW_6 = value; OnPropertyChanged("FLW_6"); } }
        private string _FLW_7; public string FLW_7 { get { return _FLW_7; } set { _FLW_7 = value; OnPropertyChanged("FLW_7"); } }
        private string _FLW_8; public string FLW_8 { get { return _FLW_8; } set { _FLW_8 = value; OnPropertyChanged("FLW_8"); } }
        private string _FLW_9; public string FLW_9 { get { return _FLW_9; } set { _FLW_9 = value; OnPropertyChanged("FLW_9"); } }
        private string _FLW_10; public string FLW_10 { get { return _FLW_10; } set { _FLW_10 = value; OnPropertyChanged("FLW_10"); } }
        private string _FLW_11; public string FLW_11 { get { return _FLW_11; } set { _FLW_11 = value; OnPropertyChanged("FLW_11"); } }
        private string _FLW_12; public string FLW_12 { get { return _FLW_12; } set { _FLW_12 = value; OnPropertyChanged("FLW_12"); } }
        private string _FLW_13; public string FLW_13 { get { return _FLW_13; } set { _FLW_13 = value; OnPropertyChanged("FLW_13"); } }
        private string _FLW_14; public string FLW_14 { get { return _FLW_14; } set { _FLW_14 = value; OnPropertyChanged("FLW_14"); } }
        private string _FLW_15; public string FLW_15 { get { return _FLW_15; } set { _FLW_15 = value; OnPropertyChanged("FLW_15"); } }
        private string _FLW_16; public string FLW_16 { get { return _FLW_16; } set { _FLW_16 = value; OnPropertyChanged("FLW_16"); } }
        private string _FLW_17; public string FLW_17 { get { return _FLW_17; } set { _FLW_17 = value; OnPropertyChanged("FLW_17"); } }
        private string _FLW_18; public string FLW_18 { get { return _FLW_18; } set { _FLW_18 = value; OnPropertyChanged("FLW_18"); } }
        private string _FLW_19; public string FLW_19 { get { return _FLW_19; } set { _FLW_19 = value; OnPropertyChanged("FLW_19"); } }
        private string _FLW_20; public string FLW_20 { get { return _FLW_20; } set { _FLW_20 = value; OnPropertyChanged("FLW_20"); } }
        private string _FLW_21; public string FLW_21 { get { return _FLW_21; } set { _FLW_21 = value; OnPropertyChanged("FLW_21"); } }
        private string _FLW_22; public string FLW_22 { get { return _FLW_22; } set { _FLW_22 = value; OnPropertyChanged("FLW_22"); } }
        private string _FLW_23; public string FLW_23 { get { return _FLW_23; } set { _FLW_23 = value; OnPropertyChanged("FLW_23"); } }
        private string _FLW_24; public string FLW_24 { get { return _FLW_24; } set { _FLW_24 = value; OnPropertyChanged("FLW_24"); } }
        private string _FLW_25; public string FLW_25 { get { return _FLW_25; } set { _FLW_25 = value; OnPropertyChanged("FLW_25"); } }
        private string _FLW_26; public string FLW_26 { get { return _FLW_26; } set { _FLW_26 = value; OnPropertyChanged("FLW_26"); } }
        private string _FLW_27; public string FLW_27 { get { return _FLW_27; } set { _FLW_27 = value; OnPropertyChanged("FLW_27"); } }
        private string _FLW_28; public string FLW_28 { get { return _FLW_28; } set { _FLW_28 = value; OnPropertyChanged("FLW_28"); } }
        private string _FLW_29; public string FLW_29 { get { return _FLW_29; } set { _FLW_29 = value; OnPropertyChanged("FLW_29"); } }
        private string _FLW_30; public string FLW_30 { get { return _FLW_30; } set { _FLW_30 = value; OnPropertyChanged("FLW_30"); } }
        private string _FLW_31; public string FLW_31 { get { return _FLW_31; } set { _FLW_31 = value; OnPropertyChanged("FLW_31"); } }
        private string _FLW_32; public string FLW_32 { get { return _FLW_32; } set { _FLW_32 = value; OnPropertyChanged("FLW_32"); } }
        private string _FLW_33; public string FLW_33 { get { return _FLW_33; } set { _FLW_33 = value; OnPropertyChanged("FLW_33"); } }
        private string _FLW_34; public string FLW_34 { get { return _FLW_34; } set { _FLW_34 = value; OnPropertyChanged("FLW_34"); } }
        private string _FLW_35; public string FLW_35 { get { return _FLW_35; } set { _FLW_35 = value; OnPropertyChanged("FLW_35"); } }
        private string _FLW_36; public string FLW_36 { get { return _FLW_36; } set { _FLW_36 = value; OnPropertyChanged("FLW_36"); } }
        private string _FLW_37; public string FLW_37 { get { return _FLW_37; } set { _FLW_37 = value; OnPropertyChanged("FLW_37"); } }
        private string _FLW_38; public string FLW_38 { get { return _FLW_38; } set { _FLW_38 = value; OnPropertyChanged("FLW_38"); } }
        private string _FLW_39; public string FLW_39 { get { return _FLW_39; } set { _FLW_39 = value; OnPropertyChanged("FLW_39"); } }
        private string _FLW_40; public string FLW_40 { get { return _FLW_40; } set { _FLW_40 = value; OnPropertyChanged("FLW_40"); } }
        private string _FLW_41; public string FLW_41 { get { return _FLW_41; } set { _FLW_41 = value; OnPropertyChanged("FLW_41"); } }
        private string _FLW_42; public string FLW_42 { get { return _FLW_42; } set { _FLW_42 = value; OnPropertyChanged("FLW_42"); } }
        private string _FLW_43; public string FLW_43 { get { return _FLW_43; } set { _FLW_43 = value; OnPropertyChanged("FLW_43"); } }
        private string _FLW_44; public string FLW_44 { get { return _FLW_44; } set { _FLW_44 = value; OnPropertyChanged("FLW_44"); } }
        private string _FLW_45; public string FLW_45 { get { return _FLW_45; } set { _FLW_45 = value; OnPropertyChanged("FLW_45"); } }
        private string _FLW_46; public string FLW_46 { get { return _FLW_46; } set { _FLW_46 = value; OnPropertyChanged("FLW_46"); } }
        private string _FLW_47; public string FLW_47 { get { return _FLW_47; } set { _FLW_47 = value; OnPropertyChanged("FLW_47"); } }
        private string _FLW_48; public string FLW_48 { get { return _FLW_48; } set { _FLW_48 = value; OnPropertyChanged("FLW_48"); } }
        private string _FLW_49; public string FLW_49 { get { return _FLW_49; } set { _FLW_49 = value; OnPropertyChanged("FLW_49"); } }
        private string _FLW_50; public string FLW_50 { get { return _FLW_50; } set { _FLW_50 = value; OnPropertyChanged("FLW_50"); } }
        private string _FLW_51; public string FLW_51 { get { return _FLW_51; } set { _FLW_51 = value; OnPropertyChanged("FLW_51"); } }
        private string _FLW_52; public string FLW_52 { get { return _FLW_52; } set { _FLW_52 = value; OnPropertyChanged("FLW_52"); } }
        private string _FLW_53; public string FLW_53 { get { return _FLW_53; } set { _FLW_53 = value; OnPropertyChanged("FLW_53"); } }
        private string _FLW_54; public string FLW_54 { get { return _FLW_54; } set { _FLW_54 = value; OnPropertyChanged("FLW_54"); } }
        private string _FLW_55; public string FLW_55 { get { return _FLW_55; } set { _FLW_55 = value; OnPropertyChanged("FLW_55"); } }
        private string _FLW_56; public string FLW_56 { get { return _FLW_56; } set { _FLW_56 = value; OnPropertyChanged("FLW_56"); } }
        private string _FLW_57; public string FLW_57 { get { return _FLW_57; } set { _FLW_57 = value; OnPropertyChanged("FLW_57"); } }
        private string _FLW_58; public string FLW_58 { get { return _FLW_58; } set { _FLW_58 = value; OnPropertyChanged("FLW_58"); } }
        private string _FLW_59; public string FLW_59 { get { return _FLW_59; } set { _FLW_59 = value; OnPropertyChanged("FLW_59"); } }
        private string _FLW_60; public string FLW_60 { get { return _FLW_60; } set { _FLW_60 = value; OnPropertyChanged("FLW_60"); } }
        private string _FLW_61; public string FLW_61 { get { return _FLW_61; } set { _FLW_61 = value; OnPropertyChanged("FLW_61"); } }
        private string _FLW_62; public string FLW_62 { get { return _FLW_62; } set { _FLW_62 = value; OnPropertyChanged("FLW_62"); } }
        private string _FLW_63; public string FLW_63 { get { return _FLW_63; } set { _FLW_63 = value; OnPropertyChanged("FLW_63"); } }
        private string _FLW_64; public string FLW_64 { get { return _FLW_64; } set { _FLW_64 = value; OnPropertyChanged("FLW_64"); } }
        private string _FLW_65; public string FLW_65 { get { return _FLW_65; } set { _FLW_65 = value; OnPropertyChanged("FLW_65"); } }
        private string _FLW_66; public string FLW_66 { get { return _FLW_66; } set { _FLW_66 = value; OnPropertyChanged("FLW_66"); } }
        private string _FLW_67; public string FLW_67 { get { return _FLW_67; } set { _FLW_67 = value; OnPropertyChanged("FLW_67"); } }
        private string _FLW_68; public string FLW_68 { get { return _FLW_68; } set { _FLW_68 = value; OnPropertyChanged("FLW_68"); } }
        private string _FLW_69; public string FLW_69 { get { return _FLW_69; } set { _FLW_69 = value; OnPropertyChanged("FLW_69"); } }
        private string _FLW_70; public string FLW_70 { get { return _FLW_70; } set { _FLW_70 = value; OnPropertyChanged("FLW_70"); } }
        private string _FLW_71; public string FLW_71 { get { return _FLW_71; } set { _FLW_71 = value; OnPropertyChanged("FLW_71"); } }
        private string _FLW_72; public string FLW_72 { get { return _FLW_72; } set { _FLW_72 = value; OnPropertyChanged("FLW_72"); } }
        private string _FLW_73; public string FLW_73 { get { return _FLW_73; } set { _FLW_73 = value; OnPropertyChanged("FLW_73"); } }
        private string _FLW_74; public string FLW_74 { get { return _FLW_74; } set { _FLW_74 = value; OnPropertyChanged("FLW_74"); } }
        private string _FLW_75; public string FLW_75 { get { return _FLW_75; } set { _FLW_75 = value; OnPropertyChanged("FLW_75"); } }
        private string _FLW_76; public string FLW_76 { get { return _FLW_76; } set { _FLW_76 = value; OnPropertyChanged("FLW_76"); } }
        private string _FLW_77; public string FLW_77 { get { return _FLW_77; } set { _FLW_77 = value; OnPropertyChanged("FLW_77"); } }
        private string _FLW_78; public string FLW_78 { get { return _FLW_78; } set { _FLW_78 = value; OnPropertyChanged("FLW_78"); } }
        private string _FLW_79; public string FLW_79 { get { return _FLW_79; } set { _FLW_79 = value; OnPropertyChanged("FLW_79"); } }
        private string _FLW_80; public string FLW_80 { get { return _FLW_80; } set { _FLW_80 = value; OnPropertyChanged("FLW_80"); } }
        private string _FLW_81; public string FLW_81 { get { return _FLW_81; } set { _FLW_81 = value; OnPropertyChanged("FLW_81"); } }
        private string _FLW_82; public string FLW_82 { get { return _FLW_82; } set { _FLW_82 = value; OnPropertyChanged("FLW_82"); } }
        private string _FLW_83; public string FLW_83 { get { return _FLW_83; } set { _FLW_83 = value; OnPropertyChanged("FLW_83"); } }
        private string _FLW_84; public string FLW_84 { get { return _FLW_84; } set { _FLW_84 = value; OnPropertyChanged("FLW_84"); } }
        private string _FLW_85; public string FLW_85 { get { return _FLW_85; } set { _FLW_85 = value; OnPropertyChanged("FLW_85"); } }
        private string _FLW_86; public string FLW_86 { get { return _FLW_86; } set { _FLW_86 = value; OnPropertyChanged("FLW_86"); } }
        private string _FLW_87; public string FLW_87 { get { return _FLW_87; } set { _FLW_87 = value; OnPropertyChanged("FLW_87"); } }
        private string _FLW_88; public string FLW_88 { get { return _FLW_88; } set { _FLW_88 = value; OnPropertyChanged("FLW_88"); } }
        private string _FLW_89; public string FLW_89 { get { return _FLW_89; } set { _FLW_89 = value; OnPropertyChanged("FLW_89"); } }
        private string _FLW_90; public string FLW_90 { get { return _FLW_90; } set { _FLW_90 = value; OnPropertyChanged("FLW_90"); } }
        private string _FLW_91; public string FLW_91 { get { return _FLW_91; } set { _FLW_91 = value; OnPropertyChanged("FLW_91"); } }
        private string _FLW_92; public string FLW_92 { get { return _FLW_92; } set { _FLW_92 = value; OnPropertyChanged("FLW_92"); } }
        private string _FLW_93; public string FLW_93 { get { return _FLW_93; } set { _FLW_93 = value; OnPropertyChanged("FLW_93"); } }
        private string _FLW_94; public string FLW_94 { get { return _FLW_94; } set { _FLW_94 = value; OnPropertyChanged("FLW_94"); } }
        private string _FLW_95; public string FLW_95 { get { return _FLW_95; } set { _FLW_95 = value; OnPropertyChanged("FLW_95"); } }
        private string _FLW_96; public string FLW_96 { get { return _FLW_96; } set { _FLW_96 = value; OnPropertyChanged("FLW_96"); } }
        private string _FLW_97; public string FLW_97 { get { return _FLW_97; } set { _FLW_97 = value; OnPropertyChanged("FLW_97"); } }
        private string _FLW_98; public string FLW_98 { get { return _FLW_98; } set { _FLW_98 = value; OnPropertyChanged("FLW_98"); } }
        private string _FLW_99; public string FLW_99 { get { return _FLW_99; } set { _FLW_99 = value; OnPropertyChanged("FLW_99"); } }
        private string _FLW_100; public string FLW_100 { get { return _FLW_100; } set { _FLW_100 = value; OnPropertyChanged("FLW_100"); } }
        private string _FLW_101; public string FLW_101 { get { return _FLW_101; } set { _FLW_101 = value; OnPropertyChanged("FLW_101"); } }
        private string _FLW_102; public string FLW_102 { get { return _FLW_102; } set { _FLW_102 = value; OnPropertyChanged("FLW_102"); } }
        private string _FLW_103; public string FLW_103 { get { return _FLW_103; } set { _FLW_103 = value; OnPropertyChanged("FLW_103"); } }
        private string _FLW_104; public string FLW_104 { get { return _FLW_104; } set { _FLW_104 = value; OnPropertyChanged("FLW_104"); } }
        private string _FLW_105; public string FLW_105 { get { return _FLW_105; } set { _FLW_105 = value; OnPropertyChanged("FLW_105"); } }
        private string _FLW_106; public string FLW_106 { get { return _FLW_106; } set { _FLW_106 = value; OnPropertyChanged("FLW_106"); } }
        private string _FLW_107; public string FLW_107 { get { return _FLW_107; } set { _FLW_107 = value; OnPropertyChanged("FLW_107"); } }
        private string _FLW_108; public string FLW_108 { get { return _FLW_108; } set { _FLW_108 = value; OnPropertyChanged("FLW_108"); } }
        private string _FLW_109; public string FLW_109 { get { return _FLW_109; } set { _FLW_109 = value; OnPropertyChanged("FLW_109"); } }
        private string _FLW_110; public string FLW_110 { get { return _FLW_110; } set { _FLW_110 = value; OnPropertyChanged("FLW_110"); } }
        private string _FLW_111; public string FLW_111 { get { return _FLW_111; } set { _FLW_111 = value; OnPropertyChanged("FLW_111"); } }
        private string _FLW_112; public string FLW_112 { get { return _FLW_112; } set { _FLW_112 = value; OnPropertyChanged("FLW_112"); } }
        private string _FLW_113; public string FLW_113 { get { return _FLW_113; } set { _FLW_113 = value; OnPropertyChanged("FLW_113"); } }
        private string _FLW_114; public string FLW_114 { get { return _FLW_114; } set { _FLW_114 = value; OnPropertyChanged("FLW_114"); } }
        private string _FLW_115; public string FLW_115 { get { return _FLW_115; } set { _FLW_115 = value; OnPropertyChanged("FLW_115"); } }
        private string _FLW_116; public string FLW_116 { get { return _FLW_116; } set { _FLW_116 = value; OnPropertyChanged("FLW_116"); } }
        private string _FLW_117; public string FLW_117 { get { return _FLW_117; } set { _FLW_117 = value; OnPropertyChanged("FLW_117"); } }
        private string _FLW_118; public string FLW_118 { get { return _FLW_118; } set { _FLW_118 = value; OnPropertyChanged("FLW_118"); } }
        private string _FLW_119; public string FLW_119 { get { return _FLW_119; } set { _FLW_119 = value; OnPropertyChanged("FLW_119"); } }
        private string _FLW_120; public string FLW_120 { get { return _FLW_120; } set { _FLW_120 = value; OnPropertyChanged("FLW_120"); } }
        private string _FLW_121; public string FLW_121 { get { return _FLW_121; } set { _FLW_121 = value; OnPropertyChanged("FLW_121"); } }
        private string _FLW_122; public string FLW_122 { get { return _FLW_122; } set { _FLW_122 = value; OnPropertyChanged("FLW_122"); } }
        private string _FLW_123; public string FLW_123 { get { return _FLW_123; } set { _FLW_123 = value; OnPropertyChanged("FLW_123"); } }
        private string _FLW_124; public string FLW_124 { get { return _FLW_124; } set { _FLW_124 = value; OnPropertyChanged("FLW_124"); } }
        private string _FLW_125; public string FLW_125 { get { return _FLW_125; } set { _FLW_125 = value; OnPropertyChanged("FLW_125"); } }
        private string _FLW_126; public string FLW_126 { get { return _FLW_126; } set { _FLW_126 = value; OnPropertyChanged("FLW_126"); } }
        private string _FLW_127; public string FLW_127 { get { return _FLW_127; } set { _FLW_127 = value; OnPropertyChanged("FLW_127"); } }
        private string _FLW_128; public string FLW_128 { get { return _FLW_128; } set { _FLW_128 = value; OnPropertyChanged("FLW_128"); } }
        private string _FLW_129; public string FLW_129 { get { return _FLW_129; } set { _FLW_129 = value; OnPropertyChanged("FLW_129"); } }
        private string _FLW_130; public string FLW_130 { get { return _FLW_130; } set { _FLW_130 = value; OnPropertyChanged("FLW_130"); } }
        private string _FLW_131; public string FLW_131 { get { return _FLW_131; } set { _FLW_131 = value; OnPropertyChanged("FLW_131"); } }
        private string _FLW_132; public string FLW_132 { get { return _FLW_132; } set { _FLW_132 = value; OnPropertyChanged("FLW_132"); } }
        private string _FLW_133; public string FLW_133 { get { return _FLW_133; } set { _FLW_133 = value; OnPropertyChanged("FLW_133"); } }
        private string _FLW_134; public string FLW_134 { get { return _FLW_134; } set { _FLW_134 = value; OnPropertyChanged("FLW_134"); } }
        private string _FLW_135; public string FLW_135 { get { return _FLW_135; } set { _FLW_135 = value; OnPropertyChanged("FLW_135"); } }
        private string _FLW_136; public string FLW_136 { get { return _FLW_136; } set { _FLW_136 = value; OnPropertyChanged("FLW_136"); } }
        private string _FLW_137; public string FLW_137 { get { return _FLW_137; } set { _FLW_137 = value; OnPropertyChanged("FLW_137"); } }
        private string _FLW_138; public string FLW_138 { get { return _FLW_138; } set { _FLW_138 = value; OnPropertyChanged("FLW_138"); } }
        private string _FLW_139; public string FLW_139 { get { return _FLW_139; } set { _FLW_139 = value; OnPropertyChanged("FLW_139"); } }
        private string _FLW_140; public string FLW_140 { get { return _FLW_140; } set { _FLW_140 = value; OnPropertyChanged("FLW_140"); } }
        private string _FLW_141; public string FLW_141 { get { return _FLW_141; } set { _FLW_141 = value; OnPropertyChanged("FLW_141"); } }
        private string _FLW_142; public string FLW_142 { get { return _FLW_142; } set { _FLW_142 = value; OnPropertyChanged("FLW_142"); } }
        private string _FLW_143; public string FLW_143 { get { return _FLW_143; } set { _FLW_143 = value; OnPropertyChanged("FLW_143"); } }


        public bool IsDirty_0 { get; private set; }
        public bool IsDirty_1 { get; private set; }
        public bool IsDirty_2 { get; private set; }
        public bool IsDirty_3 { get; private set; }
        public bool IsDirty_4 { get; private set; }
        public bool IsDirty_5 { get; private set; }
        public bool IsDirty_6 { get; private set; }
        public bool IsDirty_7 { get; private set; }
        public bool IsDirty_8 { get; private set; }
        public bool IsDirty_9 { get; private set; }
        public bool IsDirty_10 { get; private set; }
        public bool IsDirty_11 { get; private set; }
        public bool IsDirty_12 { get; private set; }
        public bool IsDirty_13 { get; private set; }
        public bool IsDirty_14 { get; private set; }
        public bool IsDirty_15 { get; private set; }
        public bool IsDirty_16 { get; private set; }
        public bool IsDirty_17 { get; private set; }
        public bool IsDirty_18 { get; private set; }
        public bool IsDirty_19 { get; private set; }
        public bool IsDirty_20 { get; private set; }
        public bool IsDirty_21 { get; private set; }
        public bool IsDirty_22 { get; private set; }
        public bool IsDirty_23 { get; private set; }
        public bool IsDirty_24 { get; private set; }
        public bool IsDirty_25 { get; private set; }
        public bool IsDirty_26 { get; private set; }
        public bool IsDirty_27 { get; private set; }
        public bool IsDirty_28 { get; private set; }
        public bool IsDirty_29 { get; private set; }
        public bool IsDirty_30 { get; private set; }
        public bool IsDirty_31 { get; private set; }
        public bool IsDirty_32 { get; private set; }
        public bool IsDirty_33 { get; private set; }
        public bool IsDirty_34 { get; private set; }
        public bool IsDirty_35 { get; private set; }
        public bool IsDirty_36 { get; private set; }
        public bool IsDirty_37 { get; private set; }
        public bool IsDirty_38 { get; private set; }
        public bool IsDirty_39 { get; private set; }
        public bool IsDirty_40 { get; private set; }
        public bool IsDirty_41 { get; private set; }
        public bool IsDirty_42 { get; private set; }
        public bool IsDirty_43 { get; private set; }
        public bool IsDirty_44 { get; private set; }
        public bool IsDirty_45 { get; private set; }
        public bool IsDirty_46 { get; private set; }
        public bool IsDirty_47 { get; private set; }
        public bool IsDirty_48 { get; private set; }
        public bool IsDirty_49 { get; private set; }
        public bool IsDirty_50 { get; private set; }
        public bool IsDirty_51 { get; private set; }
        public bool IsDirty_52 { get; private set; }
        public bool IsDirty_53 { get; private set; }
        public bool IsDirty_54 { get; private set; }
        public bool IsDirty_55 { get; private set; }
        public bool IsDirty_56 { get; private set; }
        public bool IsDirty_57 { get; private set; }
        public bool IsDirty_58 { get; private set; }
        public bool IsDirty_59 { get; private set; }
        public bool IsDirty_60 { get; private set; }
        public bool IsDirty_61 { get; private set; }
        public bool IsDirty_62 { get; private set; }
        public bool IsDirty_63 { get; private set; }
        public bool IsDirty_64 { get; private set; }
        public bool IsDirty_65 { get; private set; }
        public bool IsDirty_66 { get; private set; }
        public bool IsDirty_67 { get; private set; }
        public bool IsDirty_68 { get; private set; }
        public bool IsDirty_69 { get; private set; }
        public bool IsDirty_70 { get; private set; }
        public bool IsDirty_71 { get; private set; }
        public bool IsDirty_72 { get; private set; }
        public bool IsDirty_73 { get; private set; }
        public bool IsDirty_74 { get; private set; }
        public bool IsDirty_75 { get; private set; }
        public bool IsDirty_76 { get; private set; }
        public bool IsDirty_77 { get; private set; }
        public bool IsDirty_78 { get; private set; }
        public bool IsDirty_79 { get; private set; }
        public bool IsDirty_80 { get; private set; }
        public bool IsDirty_81 { get; private set; }
        public bool IsDirty_82 { get; private set; }
        public bool IsDirty_83 { get; private set; }
        public bool IsDirty_84 { get; private set; }
        public bool IsDirty_85 { get; private set; }
        public bool IsDirty_86 { get; private set; }
        public bool IsDirty_87 { get; private set; }
        public bool IsDirty_88 { get; private set; }
        public bool IsDirty_89 { get; private set; }
        public bool IsDirty_90 { get; private set; }
        public bool IsDirty_91 { get; private set; }
        public bool IsDirty_92 { get; private set; }
        public bool IsDirty_93 { get; private set; }
        public bool IsDirty_94 { get; private set; }
        public bool IsDirty_95 { get; private set; }
        public bool IsDirty_96 { get; private set; }
        public bool IsDirty_97 { get; private set; }
        public bool IsDirty_98 { get; private set; }
        public bool IsDirty_99 { get; private set; }
        public bool IsDirty_100 { get; private set; }
        public bool IsDirty_101 { get; private set; }
        public bool IsDirty_102 { get; private set; }
        public bool IsDirty_103 { get; private set; }
        public bool IsDirty_104 { get; private set; }
        public bool IsDirty_105 { get; private set; }
        public bool IsDirty_106 { get; private set; }
        public bool IsDirty_107 { get; private set; }
        public bool IsDirty_108 { get; private set; }
        public bool IsDirty_109 { get; private set; }
        public bool IsDirty_110 { get; private set; }
        public bool IsDirty_111 { get; private set; }
        public bool IsDirty_112 { get; private set; }
        public bool IsDirty_113 { get; private set; }
        public bool IsDirty_114 { get; private set; }
        public bool IsDirty_115 { get; private set; }
        public bool IsDirty_116 { get; private set; }
        public bool IsDirty_117 { get; private set; }
        public bool IsDirty_118 { get; private set; }
        public bool IsDirty_119 { get; private set; }
        public bool IsDirty_120 { get; private set; }
        public bool IsDirty_121 { get; private set; }
        public bool IsDirty_122 { get; private set; }
        public bool IsDirty_123 { get; private set; }
        public bool IsDirty_124 { get; private set; }
        public bool IsDirty_125 { get; private set; }
        public bool IsDirty_126 { get; private set; }
        public bool IsDirty_127 { get; private set; }
        public bool IsDirty_128 { get; private set; }
        public bool IsDirty_129 { get; private set; }
        public bool IsDirty_130 { get; private set; }
        public bool IsDirty_131 { get; private set; }
        public bool IsDirty_132 { get; private set; }
        public bool IsDirty_133 { get; private set; }
        public bool IsDirty_134 { get; private set; }
        public bool IsDirty_135 { get; private set; }
        public bool IsDirty_136 { get; private set; }
        public bool IsDirty_137 { get; private set; }
        public bool IsDirty_138 { get; private set; }
        public bool IsDirty_139 { get; private set; }
        public bool IsDirty_140 { get; private set; }
        public bool IsDirty_141 { get; private set; }
        public bool IsDirty_142 { get; private set; }
        public bool IsDirty_143 { get; private set; }



        public void Initialize()
        {
            IsDirty_0 = false;
            IsDirty_1 = false;
            IsDirty_2 = false;
            IsDirty_3 = false;
            IsDirty_4 = false;
            IsDirty_5 = false;
            IsDirty_6 = false;
            IsDirty_7 = false;
            IsDirty_8 = false;
            IsDirty_9 = false;
            IsDirty_10 = false;
            IsDirty_11 = false;
            IsDirty_12 = false;
            IsDirty_13 = false;
            IsDirty_14 = false;
            IsDirty_15 = false;
            IsDirty_16 = false;
            IsDirty_17 = false;
            IsDirty_18 = false;
            IsDirty_19 = false;
            IsDirty_20 = false;
            IsDirty_21 = false;
            IsDirty_22 = false;
            IsDirty_23 = false;
            IsDirty_24 = false;
            IsDirty_25 = false;
            IsDirty_26 = false;
            IsDirty_27 = false;
            IsDirty_28 = false;
            IsDirty_29 = false;
            IsDirty_30 = false;
            IsDirty_31 = false;
            IsDirty_32 = false;
            IsDirty_33 = false;
            IsDirty_34 = false;
            IsDirty_35 = false;
            IsDirty_36 = false;
            IsDirty_37 = false;
            IsDirty_38 = false;
            IsDirty_39 = false;
            IsDirty_40 = false;
            IsDirty_41 = false;
            IsDirty_42 = false;
            IsDirty_43 = false;
            IsDirty_44 = false;
            IsDirty_45 = false;
            IsDirty_46 = false;
            IsDirty_47 = false;
            IsDirty_48 = false;
            IsDirty_49 = false;
            IsDirty_50 = false;
            IsDirty_51 = false;
            IsDirty_52 = false;
            IsDirty_53 = false;
            IsDirty_54 = false;
            IsDirty_55 = false;
            IsDirty_56 = false;
            IsDirty_57 = false;
            IsDirty_58 = false;
            IsDirty_59 = false;
            IsDirty_60 = false;
            IsDirty_61 = false;
            IsDirty_62 = false;
            IsDirty_63 = false;
            IsDirty_64 = false;
            IsDirty_65 = false;
            IsDirty_66 = false;
            IsDirty_67 = false;
            IsDirty_68 = false;
            IsDirty_69 = false;
            IsDirty_70 = false;
            IsDirty_71 = false;
            IsDirty_72 = false;
            IsDirty_73 = false;
            IsDirty_74 = false;
            IsDirty_75 = false;
            IsDirty_76 = false;
            IsDirty_77 = false;
            IsDirty_78 = false;
            IsDirty_79 = false;
            IsDirty_80 = false;
            IsDirty_81 = false;
            IsDirty_82 = false;
            IsDirty_83 = false;
            IsDirty_84 = false;
            IsDirty_85 = false;
            IsDirty_86 = false;
            IsDirty_87 = false;
            IsDirty_88 = false;
            IsDirty_89 = false;
            IsDirty_90 = false;
            IsDirty_91 = false;
            IsDirty_92 = false;
            IsDirty_93 = false;
            IsDirty_94 = false;
            IsDirty_95 = false;
            IsDirty_96 = false;
            IsDirty_97 = false;
            IsDirty_98 = false;
            IsDirty_99 = false;
            IsDirty_100 = false;
            IsDirty_101 = false;
            IsDirty_102 = false;
            IsDirty_103 = false;
            IsDirty_104 = false;
            IsDirty_105 = false;
            IsDirty_106 = false;
            IsDirty_107 = false;
            IsDirty_108 = false;
            IsDirty_109 = false;
            IsDirty_110 = false;
            IsDirty_111 = false;
            IsDirty_112 = false;
            IsDirty_113 = false;
            IsDirty_114 = false;
            IsDirty_115 = false;
            IsDirty_116 = false;
            IsDirty_117 = false;
            IsDirty_118 = false;
            IsDirty_119 = false;
            IsDirty_120 = false;
            IsDirty_121 = false;
            IsDirty_122 = false;
            IsDirty_123 = false;
            IsDirty_124 = false;
            IsDirty_125 = false;
            IsDirty_126 = false;
            IsDirty_127 = false;
            IsDirty_128 = false;
            IsDirty_129 = false;
            IsDirty_130 = false;
            IsDirty_131 = false;
            IsDirty_132 = false;
            IsDirty_133 = false;
            IsDirty_134 = false;
            IsDirty_135 = false;
            IsDirty_136 = false;
            IsDirty_137 = false;
            IsDirty_138 = false;
            IsDirty_139 = false;
            IsDirty_140 = false;
            IsDirty_141 = false;
            IsDirty_142 = false;
            IsDirty_143 = false;
        }

        #region -- 출력용 변환 필드 목록 --
        public string P_OBSDT { get { return DateUtil.formatDate(_OBSDT); } }
        public string P_PRESENTER { get { return OBSNM + ":" + P_OBSDT; } }
        #endregion

        #region -- 유효성 검사 --
        private void DoubleValidationCheck(string value)
        {
            try
            {   // " " 공백문자는 허가함. 결측 자료가 " "으로 표현되기 때문
                if (value != null && " ".Equals(value) == false)
                {
                    double.Parse(value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("숫자만을 입력 할 수 있습니다.", ex);
            }
        }
        #endregion
    }
}
