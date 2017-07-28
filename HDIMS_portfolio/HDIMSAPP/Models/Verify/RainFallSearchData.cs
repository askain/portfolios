using System;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.Verify
{
    public class RainFallSearchData : Data
    {
        private string _ID;
        //private new string _OBSDT ;
        public string OBSCD { get; set; }
        public string OBSNM { get; set; }
        public string DAMNM { get; set; }
        public string DAMCD { get; set; }
        public string ORIG_DAMCD { get; set; }
        public string DBSNTSN { get; set; }
        public string PDACURF { get; set; }
        public string PTACURF { get; set; }
        public string PPDACURF { get; set; }
        public string EDRF { get; set; }
        
        //private new string _TRMDV ;
        private string _ACURF_0; public string ACURF_0 { get { return _ACURF_0; } set { DoubleValidationCheck(value); IsDirty_0 = true; _ACURF_0 = value; OnPropertyChanged("ACURF_0"); } }
        private string _ACURF_1; public string ACURF_1 { get { return _ACURF_1; } set { DoubleValidationCheck(value); IsDirty_1 = true; _ACURF_1 = value; OnPropertyChanged("ACURF_1"); } }
        private string _ACURF_2; public string ACURF_2 { get { return _ACURF_2; } set { DoubleValidationCheck(value); IsDirty_2 = true; _ACURF_2 = value; OnPropertyChanged("ACURF_2"); } }
        private string _ACURF_3; public string ACURF_3 { get { return _ACURF_3; } set { DoubleValidationCheck(value); IsDirty_3 = true; _ACURF_3 = value; OnPropertyChanged("ACURF_3"); } }
        private string _ACURF_4; public string ACURF_4 { get { return _ACURF_4; } set { DoubleValidationCheck(value); IsDirty_4 = true; _ACURF_4 = value; OnPropertyChanged("ACURF_4"); } }
        private string _ACURF_5; public string ACURF_5 { get { return _ACURF_5; } set { DoubleValidationCheck(value); IsDirty_5 = true; _ACURF_5 = value; OnPropertyChanged("ACURF_5"); } }
        private string _ACURF_6; public string ACURF_6 { get { return _ACURF_6; } set { DoubleValidationCheck(value); IsDirty_6 = true; _ACURF_6 = value; OnPropertyChanged("ACURF_6"); } }
        private string _ACURF_7; public string ACURF_7 { get { return _ACURF_7; } set { DoubleValidationCheck(value); IsDirty_7 = true; _ACURF_7 = value; OnPropertyChanged("ACURF_7"); } }
        private string _ACURF_8; public string ACURF_8 { get { return _ACURF_8; } set { DoubleValidationCheck(value); IsDirty_8 = true; _ACURF_8 = value; OnPropertyChanged("ACURF_8"); } }
        private string _ACURF_9; public string ACURF_9 { get { return _ACURF_9; } set { DoubleValidationCheck(value); IsDirty_9 = true; _ACURF_9 = value; OnPropertyChanged("ACURF_9"); } }
        private string _ACURF_10; public string ACURF_10 { get { return _ACURF_10; } set { DoubleValidationCheck(value); IsDirty_10 = true; _ACURF_10 = value; OnPropertyChanged("ACURF_10"); } }
        private string _ACURF_11; public string ACURF_11 { get { return _ACURF_11; } set { DoubleValidationCheck(value); IsDirty_11 = true; _ACURF_11 = value; OnPropertyChanged("ACURF_11"); } }
        private string _ACURF_12; public string ACURF_12 { get { return _ACURF_12; } set { DoubleValidationCheck(value); IsDirty_12 = true; _ACURF_12 = value; OnPropertyChanged("ACURF_12"); } }
        private string _ACURF_13; public string ACURF_13 { get { return _ACURF_13; } set { DoubleValidationCheck(value); IsDirty_13 = true; _ACURF_13 = value; OnPropertyChanged("ACURF_13"); } }
        private string _ACURF_14; public string ACURF_14 { get { return _ACURF_14; } set { DoubleValidationCheck(value); IsDirty_14 = true; _ACURF_14 = value; OnPropertyChanged("ACURF_14"); } }
        private string _ACURF_15; public string ACURF_15 { get { return _ACURF_15; } set { DoubleValidationCheck(value); IsDirty_15 = true; _ACURF_15 = value; OnPropertyChanged("ACURF_15"); } }
        private string _ACURF_16; public string ACURF_16 { get { return _ACURF_16; } set { DoubleValidationCheck(value); IsDirty_16 = true; _ACURF_16 = value; OnPropertyChanged("ACURF_16"); } }
        private string _ACURF_17; public string ACURF_17 { get { return _ACURF_17; } set { DoubleValidationCheck(value); IsDirty_17 = true; _ACURF_17 = value; OnPropertyChanged("ACURF_17"); } }
        private string _ACURF_18; public string ACURF_18 { get { return _ACURF_18; } set { DoubleValidationCheck(value); IsDirty_18 = true; _ACURF_18 = value; OnPropertyChanged("ACURF_18"); } }
        private string _ACURF_19; public string ACURF_19 { get { return _ACURF_19; } set { DoubleValidationCheck(value); IsDirty_19 = true; _ACURF_19 = value; OnPropertyChanged("ACURF_19"); } }
        private string _ACURF_20; public string ACURF_20 { get { return _ACURF_20; } set { DoubleValidationCheck(value); IsDirty_20 = true; _ACURF_20 = value; OnPropertyChanged("ACURF_20"); } }
        private string _ACURF_21; public string ACURF_21 { get { return _ACURF_21; } set { DoubleValidationCheck(value); IsDirty_21 = true; _ACURF_21 = value; OnPropertyChanged("ACURF_21"); } }
        private string _ACURF_22; public string ACURF_22 { get { return _ACURF_22; } set { DoubleValidationCheck(value); IsDirty_22 = true; _ACURF_22 = value; OnPropertyChanged("ACURF_22"); } }
        private string _ACURF_23; public string ACURF_23 { get { return _ACURF_23; } set { DoubleValidationCheck(value); IsDirty_23 = true; _ACURF_23 = value; OnPropertyChanged("ACURF_23"); } }
        private string _ACURF_24; public string ACURF_24 { get { return _ACURF_24; } set { DoubleValidationCheck(value); IsDirty_24 = true; _ACURF_24 = value; OnPropertyChanged("ACURF_24"); } }
        private string _ACURF_25; public string ACURF_25 { get { return _ACURF_25; } set { DoubleValidationCheck(value); IsDirty_25 = true; _ACURF_25 = value; OnPropertyChanged("ACURF_25"); } }
        private string _ACURF_26; public string ACURF_26 { get { return _ACURF_26; } set { DoubleValidationCheck(value); IsDirty_26 = true; _ACURF_26 = value; OnPropertyChanged("ACURF_26"); } }
        private string _ACURF_27; public string ACURF_27 { get { return _ACURF_27; } set { DoubleValidationCheck(value); IsDirty_27 = true; _ACURF_27 = value; OnPropertyChanged("ACURF_27"); } }
        private string _ACURF_28; public string ACURF_28 { get { return _ACURF_28; } set { DoubleValidationCheck(value); IsDirty_28 = true; _ACURF_28 = value; OnPropertyChanged("ACURF_28"); } }
        private string _ACURF_29; public string ACURF_29 { get { return _ACURF_29; } set { DoubleValidationCheck(value); IsDirty_29 = true; _ACURF_29 = value; OnPropertyChanged("ACURF_29"); } }
        private string _ACURF_30; public string ACURF_30 { get { return _ACURF_30; } set { DoubleValidationCheck(value); IsDirty_30 = true; _ACURF_30 = value; OnPropertyChanged("ACURF_30"); } }
        private string _ACURF_31; public string ACURF_31 { get { return _ACURF_31; } set { DoubleValidationCheck(value); IsDirty_31 = true; _ACURF_31 = value; OnPropertyChanged("ACURF_31"); } }
        private string _ACURF_32; public string ACURF_32 { get { return _ACURF_32; } set { DoubleValidationCheck(value); IsDirty_32 = true; _ACURF_32 = value; OnPropertyChanged("ACURF_32"); } }
        private string _ACURF_33; public string ACURF_33 { get { return _ACURF_33; } set { DoubleValidationCheck(value); IsDirty_33 = true; _ACURF_33 = value; OnPropertyChanged("ACURF_33"); } }
        private string _ACURF_34; public string ACURF_34 { get { return _ACURF_34; } set { DoubleValidationCheck(value); IsDirty_34 = true; _ACURF_34 = value; OnPropertyChanged("ACURF_34"); } }
        private string _ACURF_35; public string ACURF_35 { get { return _ACURF_35; } set { DoubleValidationCheck(value); IsDirty_35 = true; _ACURF_35 = value; OnPropertyChanged("ACURF_35"); } }
        private string _ACURF_36; public string ACURF_36 { get { return _ACURF_36; } set { DoubleValidationCheck(value); IsDirty_36 = true; _ACURF_36 = value; OnPropertyChanged("ACURF_36"); } }
        private string _ACURF_37; public string ACURF_37 { get { return _ACURF_37; } set { DoubleValidationCheck(value); IsDirty_37 = true; _ACURF_37 = value; OnPropertyChanged("ACURF_37"); } }
        private string _ACURF_38; public string ACURF_38 { get { return _ACURF_38; } set { DoubleValidationCheck(value); IsDirty_38 = true; _ACURF_38 = value; OnPropertyChanged("ACURF_38"); } }
        private string _ACURF_39; public string ACURF_39 { get { return _ACURF_39; } set { DoubleValidationCheck(value); IsDirty_39 = true; _ACURF_39 = value; OnPropertyChanged("ACURF_39"); } }
        private string _ACURF_40; public string ACURF_40 { get { return _ACURF_40; } set { DoubleValidationCheck(value); IsDirty_40 = true; _ACURF_40 = value; OnPropertyChanged("ACURF_40"); } }
        private string _ACURF_41; public string ACURF_41 { get { return _ACURF_41; } set { DoubleValidationCheck(value); IsDirty_41 = true; _ACURF_41 = value; OnPropertyChanged("ACURF_41"); } }
        private string _ACURF_42; public string ACURF_42 { get { return _ACURF_42; } set { DoubleValidationCheck(value); IsDirty_42 = true; _ACURF_42 = value; OnPropertyChanged("ACURF_42"); } }
        private string _ACURF_43; public string ACURF_43 { get { return _ACURF_43; } set { DoubleValidationCheck(value); IsDirty_43 = true; _ACURF_43 = value; OnPropertyChanged("ACURF_43"); } }
        private string _ACURF_44; public string ACURF_44 { get { return _ACURF_44; } set { DoubleValidationCheck(value); IsDirty_44 = true; _ACURF_44 = value; OnPropertyChanged("ACURF_44"); } }
        private string _ACURF_45; public string ACURF_45 { get { return _ACURF_45; } set { DoubleValidationCheck(value); IsDirty_45 = true; _ACURF_45 = value; OnPropertyChanged("ACURF_45"); } }
        private string _ACURF_46; public string ACURF_46 { get { return _ACURF_46; } set { DoubleValidationCheck(value); IsDirty_46 = true; _ACURF_46 = value; OnPropertyChanged("ACURF_46"); } }
        private string _ACURF_47; public string ACURF_47 { get { return _ACURF_47; } set { DoubleValidationCheck(value); IsDirty_47 = true; _ACURF_47 = value; OnPropertyChanged("ACURF_47"); } }
        private string _ACURF_48; public string ACURF_48 { get { return _ACURF_48; } set { DoubleValidationCheck(value); IsDirty_48 = true; _ACURF_48 = value; OnPropertyChanged("ACURF_48"); } }
        private string _ACURF_49; public string ACURF_49 { get { return _ACURF_49; } set { DoubleValidationCheck(value); IsDirty_49 = true; _ACURF_49 = value; OnPropertyChanged("ACURF_49"); } }
        private string _ACURF_50; public string ACURF_50 { get { return _ACURF_50; } set { DoubleValidationCheck(value); IsDirty_50 = true; _ACURF_50 = value; OnPropertyChanged("ACURF_50"); } }
        private string _ACURF_51; public string ACURF_51 { get { return _ACURF_51; } set { DoubleValidationCheck(value); IsDirty_51 = true; _ACURF_51 = value; OnPropertyChanged("ACURF_51"); } }
        private string _ACURF_52; public string ACURF_52 { get { return _ACURF_52; } set { DoubleValidationCheck(value); IsDirty_52 = true; _ACURF_52 = value; OnPropertyChanged("ACURF_52"); } }
        private string _ACURF_53; public string ACURF_53 { get { return _ACURF_53; } set { DoubleValidationCheck(value); IsDirty_53 = true; _ACURF_53 = value; OnPropertyChanged("ACURF_53"); } }
        private string _ACURF_54; public string ACURF_54 { get { return _ACURF_54; } set { DoubleValidationCheck(value); IsDirty_54 = true; _ACURF_54 = value; OnPropertyChanged("ACURF_54"); } }
        private string _ACURF_55; public string ACURF_55 { get { return _ACURF_55; } set { DoubleValidationCheck(value); IsDirty_55 = true; _ACURF_55 = value; OnPropertyChanged("ACURF_55"); } }
        private string _ACURF_56; public string ACURF_56 { get { return _ACURF_56; } set { DoubleValidationCheck(value); IsDirty_56 = true; _ACURF_56 = value; OnPropertyChanged("ACURF_56"); } }
        private string _ACURF_57; public string ACURF_57 { get { return _ACURF_57; } set { DoubleValidationCheck(value); IsDirty_57 = true; _ACURF_57 = value; OnPropertyChanged("ACURF_57"); } }
        private string _ACURF_58; public string ACURF_58 { get { return _ACURF_58; } set { DoubleValidationCheck(value); IsDirty_58 = true; _ACURF_58 = value; OnPropertyChanged("ACURF_58"); } }
        private string _ACURF_59; public string ACURF_59 { get { return _ACURF_59; } set { DoubleValidationCheck(value); IsDirty_59 = true; _ACURF_59 = value; OnPropertyChanged("ACURF_59"); } }
        private string _ACURF_60; public string ACURF_60 { get { return _ACURF_60; } set { DoubleValidationCheck(value); IsDirty_60 = true; _ACURF_60 = value; OnPropertyChanged("ACURF_60"); } }
        private string _ACURF_61; public string ACURF_61 { get { return _ACURF_61; } set { DoubleValidationCheck(value); IsDirty_61 = true; _ACURF_61 = value; OnPropertyChanged("ACURF_61"); } }
        private string _ACURF_62; public string ACURF_62 { get { return _ACURF_62; } set { DoubleValidationCheck(value); IsDirty_62 = true; _ACURF_62 = value; OnPropertyChanged("ACURF_62"); } }
        private string _ACURF_63; public string ACURF_63 { get { return _ACURF_63; } set { DoubleValidationCheck(value); IsDirty_63 = true; _ACURF_63 = value; OnPropertyChanged("ACURF_63"); } }
        private string _ACURF_64; public string ACURF_64 { get { return _ACURF_64; } set { DoubleValidationCheck(value); IsDirty_64 = true; _ACURF_64 = value; OnPropertyChanged("ACURF_64"); } }
        private string _ACURF_65; public string ACURF_65 { get { return _ACURF_65; } set { DoubleValidationCheck(value); IsDirty_65 = true; _ACURF_65 = value; OnPropertyChanged("ACURF_65"); } }
        private string _ACURF_66; public string ACURF_66 { get { return _ACURF_66; } set { DoubleValidationCheck(value); IsDirty_66 = true; _ACURF_66 = value; OnPropertyChanged("ACURF_66"); } }
        private string _ACURF_67; public string ACURF_67 { get { return _ACURF_67; } set { DoubleValidationCheck(value); IsDirty_67 = true; _ACURF_67 = value; OnPropertyChanged("ACURF_67"); } }
        private string _ACURF_68; public string ACURF_68 { get { return _ACURF_68; } set { DoubleValidationCheck(value); IsDirty_68 = true; _ACURF_68 = value; OnPropertyChanged("ACURF_68"); } }
        private string _ACURF_69; public string ACURF_69 { get { return _ACURF_69; } set { DoubleValidationCheck(value); IsDirty_69 = true; _ACURF_69 = value; OnPropertyChanged("ACURF_69"); } }
        private string _ACURF_70; public string ACURF_70 { get { return _ACURF_70; } set { DoubleValidationCheck(value); IsDirty_70 = true; _ACURF_70 = value; OnPropertyChanged("ACURF_70"); } }
        private string _ACURF_71; public string ACURF_71 { get { return _ACURF_71; } set { DoubleValidationCheck(value); IsDirty_71 = true; _ACURF_71 = value; OnPropertyChanged("ACURF_71"); } }
        private string _ACURF_72; public string ACURF_72 { get { return _ACURF_72; } set { DoubleValidationCheck(value); IsDirty_72 = true; _ACURF_72 = value; OnPropertyChanged("ACURF_72"); } }
        private string _ACURF_73; public string ACURF_73 { get { return _ACURF_73; } set { DoubleValidationCheck(value); IsDirty_73 = true; _ACURF_73 = value; OnPropertyChanged("ACURF_73"); } }
        private string _ACURF_74; public string ACURF_74 { get { return _ACURF_74; } set { DoubleValidationCheck(value); IsDirty_74 = true; _ACURF_74 = value; OnPropertyChanged("ACURF_74"); } }
        private string _ACURF_75; public string ACURF_75 { get { return _ACURF_75; } set { DoubleValidationCheck(value); IsDirty_75 = true; _ACURF_75 = value; OnPropertyChanged("ACURF_75"); } }
        private string _ACURF_76; public string ACURF_76 { get { return _ACURF_76; } set { DoubleValidationCheck(value); IsDirty_76 = true; _ACURF_76 = value; OnPropertyChanged("ACURF_76"); } }
        private string _ACURF_77; public string ACURF_77 { get { return _ACURF_77; } set { DoubleValidationCheck(value); IsDirty_77 = true; _ACURF_77 = value; OnPropertyChanged("ACURF_77"); } }
        private string _ACURF_78; public string ACURF_78 { get { return _ACURF_78; } set { DoubleValidationCheck(value); IsDirty_78 = true; _ACURF_78 = value; OnPropertyChanged("ACURF_78"); } }
        private string _ACURF_79; public string ACURF_79 { get { return _ACURF_79; } set { DoubleValidationCheck(value); IsDirty_79 = true; _ACURF_79 = value; OnPropertyChanged("ACURF_79"); } }
        private string _ACURF_80; public string ACURF_80 { get { return _ACURF_80; } set { DoubleValidationCheck(value); IsDirty_80 = true; _ACURF_80 = value; OnPropertyChanged("ACURF_80"); } }
        private string _ACURF_81; public string ACURF_81 { get { return _ACURF_81; } set { DoubleValidationCheck(value); IsDirty_81 = true; _ACURF_81 = value; OnPropertyChanged("ACURF_81"); } }
        private string _ACURF_82; public string ACURF_82 { get { return _ACURF_82; } set { DoubleValidationCheck(value); IsDirty_82 = true; _ACURF_82 = value; OnPropertyChanged("ACURF_82"); } }
        private string _ACURF_83; public string ACURF_83 { get { return _ACURF_83; } set { DoubleValidationCheck(value); IsDirty_83 = true; _ACURF_83 = value; OnPropertyChanged("ACURF_83"); } }
        private string _ACURF_84; public string ACURF_84 { get { return _ACURF_84; } set { DoubleValidationCheck(value); IsDirty_84 = true; _ACURF_84 = value; OnPropertyChanged("ACURF_84"); } }
        private string _ACURF_85; public string ACURF_85 { get { return _ACURF_85; } set { DoubleValidationCheck(value); IsDirty_85 = true; _ACURF_85 = value; OnPropertyChanged("ACURF_85"); } }
        private string _ACURF_86; public string ACURF_86 { get { return _ACURF_86; } set { DoubleValidationCheck(value); IsDirty_86 = true; _ACURF_86 = value; OnPropertyChanged("ACURF_86"); } }
        private string _ACURF_87; public string ACURF_87 { get { return _ACURF_87; } set { DoubleValidationCheck(value); IsDirty_87 = true; _ACURF_87 = value; OnPropertyChanged("ACURF_87"); } }
        private string _ACURF_88; public string ACURF_88 { get { return _ACURF_88; } set { DoubleValidationCheck(value); IsDirty_88 = true; _ACURF_88 = value; OnPropertyChanged("ACURF_88"); } }
        private string _ACURF_89; public string ACURF_89 { get { return _ACURF_89; } set { DoubleValidationCheck(value); IsDirty_89 = true; _ACURF_89 = value; OnPropertyChanged("ACURF_89"); } }
        private string _ACURF_90; public string ACURF_90 { get { return _ACURF_90; } set { DoubleValidationCheck(value); IsDirty_90 = true; _ACURF_90 = value; OnPropertyChanged("ACURF_90"); } }
        private string _ACURF_91; public string ACURF_91 { get { return _ACURF_91; } set { DoubleValidationCheck(value); IsDirty_91 = true; _ACURF_91 = value; OnPropertyChanged("ACURF_91"); } }
        private string _ACURF_92; public string ACURF_92 { get { return _ACURF_92; } set { DoubleValidationCheck(value); IsDirty_92 = true; _ACURF_92 = value; OnPropertyChanged("ACURF_92"); } }
        private string _ACURF_93; public string ACURF_93 { get { return _ACURF_93; } set { DoubleValidationCheck(value); IsDirty_93 = true; _ACURF_93 = value; OnPropertyChanged("ACURF_93"); } }
        private string _ACURF_94; public string ACURF_94 { get { return _ACURF_94; } set { DoubleValidationCheck(value); IsDirty_94 = true; _ACURF_94 = value; OnPropertyChanged("ACURF_94"); } }
        private string _ACURF_95; public string ACURF_95 { get { return _ACURF_95; } set { DoubleValidationCheck(value); IsDirty_95 = true; _ACURF_95 = value; OnPropertyChanged("ACURF_95"); } }
        private string _ACURF_96; public string ACURF_96 { get { return _ACURF_96; } set { DoubleValidationCheck(value); IsDirty_96 = true; _ACURF_96 = value; OnPropertyChanged("ACURF_96"); } }
        private string _ACURF_97; public string ACURF_97 { get { return _ACURF_97; } set { DoubleValidationCheck(value); IsDirty_97 = true; _ACURF_97 = value; OnPropertyChanged("ACURF_97"); } }
        private string _ACURF_98; public string ACURF_98 { get { return _ACURF_98; } set { DoubleValidationCheck(value); IsDirty_98 = true; _ACURF_98 = value; OnPropertyChanged("ACURF_98"); } }
        private string _ACURF_99; public string ACURF_99 { get { return _ACURF_99; } set { DoubleValidationCheck(value); IsDirty_99 = true; _ACURF_99 = value; OnPropertyChanged("ACURF_99"); } }
        private string _ACURF_100; public string ACURF_100 { get { return _ACURF_100; } set { DoubleValidationCheck(value); IsDirty_100 = true; _ACURF_100 = value; OnPropertyChanged("ACURF_100"); } }
        private string _ACURF_101; public string ACURF_101 { get { return _ACURF_101; } set { DoubleValidationCheck(value); IsDirty_101 = true; _ACURF_101 = value; OnPropertyChanged("ACURF_101"); } }
        private string _ACURF_102; public string ACURF_102 { get { return _ACURF_102; } set { DoubleValidationCheck(value); IsDirty_102 = true; _ACURF_102 = value; OnPropertyChanged("ACURF_102"); } }
        private string _ACURF_103; public string ACURF_103 { get { return _ACURF_103; } set { DoubleValidationCheck(value); IsDirty_103 = true; _ACURF_103 = value; OnPropertyChanged("ACURF_103"); } }
        private string _ACURF_104; public string ACURF_104 { get { return _ACURF_104; } set { DoubleValidationCheck(value); IsDirty_104 = true; _ACURF_104 = value; OnPropertyChanged("ACURF_104"); } }
        private string _ACURF_105; public string ACURF_105 { get { return _ACURF_105; } set { DoubleValidationCheck(value); IsDirty_105 = true; _ACURF_105 = value; OnPropertyChanged("ACURF_105"); } }
        private string _ACURF_106; public string ACURF_106 { get { return _ACURF_106; } set { DoubleValidationCheck(value); IsDirty_106 = true; _ACURF_106 = value; OnPropertyChanged("ACURF_106"); } }
        private string _ACURF_107; public string ACURF_107 { get { return _ACURF_107; } set { DoubleValidationCheck(value); IsDirty_107 = true; _ACURF_107 = value; OnPropertyChanged("ACURF_107"); } }
        private string _ACURF_108; public string ACURF_108 { get { return _ACURF_108; } set { DoubleValidationCheck(value); IsDirty_108 = true; _ACURF_108 = value; OnPropertyChanged("ACURF_108"); } }
        private string _ACURF_109; public string ACURF_109 { get { return _ACURF_109; } set { DoubleValidationCheck(value); IsDirty_109 = true; _ACURF_109 = value; OnPropertyChanged("ACURF_109"); } }
        private string _ACURF_110; public string ACURF_110 { get { return _ACURF_110; } set { DoubleValidationCheck(value); IsDirty_110 = true; _ACURF_110 = value; OnPropertyChanged("ACURF_110"); } }
        private string _ACURF_111; public string ACURF_111 { get { return _ACURF_111; } set { DoubleValidationCheck(value); IsDirty_111 = true; _ACURF_111 = value; OnPropertyChanged("ACURF_111"); } }
        private string _ACURF_112; public string ACURF_112 { get { return _ACURF_112; } set { DoubleValidationCheck(value); IsDirty_112 = true; _ACURF_112 = value; OnPropertyChanged("ACURF_112"); } }
        private string _ACURF_113; public string ACURF_113 { get { return _ACURF_113; } set { DoubleValidationCheck(value); IsDirty_113 = true; _ACURF_113 = value; OnPropertyChanged("ACURF_113"); } }
        private string _ACURF_114; public string ACURF_114 { get { return _ACURF_114; } set { DoubleValidationCheck(value); IsDirty_114 = true; _ACURF_114 = value; OnPropertyChanged("ACURF_114"); } }
        private string _ACURF_115; public string ACURF_115 { get { return _ACURF_115; } set { DoubleValidationCheck(value); IsDirty_115 = true; _ACURF_115 = value; OnPropertyChanged("ACURF_115"); } }
        private string _ACURF_116; public string ACURF_116 { get { return _ACURF_116; } set { DoubleValidationCheck(value); IsDirty_116 = true; _ACURF_116 = value; OnPropertyChanged("ACURF_116"); } }
        private string _ACURF_117; public string ACURF_117 { get { return _ACURF_117; } set { DoubleValidationCheck(value); IsDirty_117 = true; _ACURF_117 = value; OnPropertyChanged("ACURF_117"); } }
        private string _ACURF_118; public string ACURF_118 { get { return _ACURF_118; } set { DoubleValidationCheck(value); IsDirty_118 = true; _ACURF_118 = value; OnPropertyChanged("ACURF_118"); } }
        private string _ACURF_119; public string ACURF_119 { get { return _ACURF_119; } set { DoubleValidationCheck(value); IsDirty_119 = true; _ACURF_119 = value; OnPropertyChanged("ACURF_119"); } }
        private string _ACURF_120; public string ACURF_120 { get { return _ACURF_120; } set { DoubleValidationCheck(value); IsDirty_120 = true; _ACURF_120 = value; OnPropertyChanged("ACURF_120"); } }
        private string _ACURF_121; public string ACURF_121 { get { return _ACURF_121; } set { DoubleValidationCheck(value); IsDirty_121 = true; _ACURF_121 = value; OnPropertyChanged("ACURF_121"); } }
        private string _ACURF_122; public string ACURF_122 { get { return _ACURF_122; } set { DoubleValidationCheck(value); IsDirty_122 = true; _ACURF_122 = value; OnPropertyChanged("ACURF_122"); } }
        private string _ACURF_123; public string ACURF_123 { get { return _ACURF_123; } set { DoubleValidationCheck(value); IsDirty_123 = true; _ACURF_123 = value; OnPropertyChanged("ACURF_123"); } }
        private string _ACURF_124; public string ACURF_124 { get { return _ACURF_124; } set { DoubleValidationCheck(value); IsDirty_124 = true; _ACURF_124 = value; OnPropertyChanged("ACURF_124"); } }
        private string _ACURF_125; public string ACURF_125 { get { return _ACURF_125; } set { DoubleValidationCheck(value); IsDirty_125 = true; _ACURF_125 = value; OnPropertyChanged("ACURF_125"); } }
        private string _ACURF_126; public string ACURF_126 { get { return _ACURF_126; } set { DoubleValidationCheck(value); IsDirty_126 = true; _ACURF_126 = value; OnPropertyChanged("ACURF_126"); } }
        private string _ACURF_127; public string ACURF_127 { get { return _ACURF_127; } set { DoubleValidationCheck(value); IsDirty_127 = true; _ACURF_127 = value; OnPropertyChanged("ACURF_127"); } }
        private string _ACURF_128; public string ACURF_128 { get { return _ACURF_128; } set { DoubleValidationCheck(value); IsDirty_128 = true; _ACURF_128 = value; OnPropertyChanged("ACURF_128"); } }
        private string _ACURF_129; public string ACURF_129 { get { return _ACURF_129; } set { DoubleValidationCheck(value); IsDirty_129 = true; _ACURF_129 = value; OnPropertyChanged("ACURF_129"); } }
        private string _ACURF_130; public string ACURF_130 { get { return _ACURF_130; } set { DoubleValidationCheck(value); IsDirty_130 = true; _ACURF_130 = value; OnPropertyChanged("ACURF_130"); } }
        private string _ACURF_131; public string ACURF_131 { get { return _ACURF_131; } set { DoubleValidationCheck(value); IsDirty_131 = true; _ACURF_131 = value; OnPropertyChanged("ACURF_131"); } }
        private string _ACURF_132; public string ACURF_132 { get { return _ACURF_132; } set { DoubleValidationCheck(value); IsDirty_132 = true; _ACURF_132 = value; OnPropertyChanged("ACURF_132"); } }
        private string _ACURF_133; public string ACURF_133 { get { return _ACURF_133; } set { DoubleValidationCheck(value); IsDirty_133 = true; _ACURF_133 = value; OnPropertyChanged("ACURF_133"); } }
        private string _ACURF_134; public string ACURF_134 { get { return _ACURF_134; } set { DoubleValidationCheck(value); IsDirty_134 = true; _ACURF_134 = value; OnPropertyChanged("ACURF_134"); } }
        private string _ACURF_135; public string ACURF_135 { get { return _ACURF_135; } set { DoubleValidationCheck(value); IsDirty_135 = true; _ACURF_135 = value; OnPropertyChanged("ACURF_135"); } }
        private string _ACURF_136; public string ACURF_136 { get { return _ACURF_136; } set { DoubleValidationCheck(value); IsDirty_136 = true; _ACURF_136 = value; OnPropertyChanged("ACURF_136"); } }
        private string _ACURF_137; public string ACURF_137 { get { return _ACURF_137; } set { DoubleValidationCheck(value); IsDirty_137 = true; _ACURF_137 = value; OnPropertyChanged("ACURF_137"); } }
        private string _ACURF_138; public string ACURF_138 { get { return _ACURF_138; } set { DoubleValidationCheck(value); IsDirty_138 = true; _ACURF_138 = value; OnPropertyChanged("ACURF_138"); } }
        private string _ACURF_139; public string ACURF_139 { get { return _ACURF_139; } set { DoubleValidationCheck(value); IsDirty_139 = true; _ACURF_139 = value; OnPropertyChanged("ACURF_139"); } }
        private string _ACURF_140; public string ACURF_140 { get { return _ACURF_140; } set { DoubleValidationCheck(value); IsDirty_140 = true; _ACURF_140 = value; OnPropertyChanged("ACURF_140"); } }
        private string _ACURF_141; public string ACURF_141 { get { return _ACURF_141; } set { DoubleValidationCheck(value); IsDirty_141 = true; _ACURF_141 = value; OnPropertyChanged("ACURF_141"); } }
        private string _ACURF_142; public string ACURF_142 { get { return _ACURF_142; } set { DoubleValidationCheck(value); IsDirty_142 = true; _ACURF_142 = value; OnPropertyChanged("ACURF_142"); } }
        private string _ACURF_143; public string ACURF_143 { get { return _ACURF_143; } set { DoubleValidationCheck(value); IsDirty_143 = true; _ACURF_143 = value; OnPropertyChanged("ACURF_143"); } }



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

        private string _RF_0; public string RF_0 { get { return _RF_0; } set { DoubleValidationCheck(value); IsDirty_0 = true; _RF_0 = value; OnPropertyChanged("RF_0"); } }
        private string _RF_1; public string RF_1 { get { return _RF_1; } set { DoubleValidationCheck(value); IsDirty_1 = true; _RF_1 = value; OnPropertyChanged("RF_1"); } }
        private string _RF_2; public string RF_2 { get { return _RF_2; } set { DoubleValidationCheck(value); IsDirty_2 = true; _RF_2 = value; OnPropertyChanged("RF_2"); } }
        private string _RF_3; public string RF_3 { get { return _RF_3; } set { DoubleValidationCheck(value); IsDirty_3 = true; _RF_3 = value; OnPropertyChanged("RF_3"); } }
        private string _RF_4; public string RF_4 { get { return _RF_4; } set { DoubleValidationCheck(value); IsDirty_4 = true; _RF_4 = value; OnPropertyChanged("RF_4"); } }
        private string _RF_5; public string RF_5 { get { return _RF_5; } set { DoubleValidationCheck(value); IsDirty_5 = true; _RF_5 = value; OnPropertyChanged("RF_5"); } }
        private string _RF_6; public string RF_6 { get { return _RF_6; } set { DoubleValidationCheck(value); IsDirty_6 = true; _RF_6 = value; OnPropertyChanged("RF_6"); } }
        private string _RF_7; public string RF_7 { get { return _RF_7; } set { DoubleValidationCheck(value); IsDirty_7 = true; _RF_7 = value; OnPropertyChanged("RF_7"); } }
        private string _RF_8; public string RF_8 { get { return _RF_8; } set { DoubleValidationCheck(value); IsDirty_8 = true; _RF_8 = value; OnPropertyChanged("RF_8"); } }
        private string _RF_9; public string RF_9 { get { return _RF_9; } set { DoubleValidationCheck(value); IsDirty_9 = true; _RF_9 = value; OnPropertyChanged("RF_9"); } }
        private string _RF_10; public string RF_10 { get { return _RF_10; } set { DoubleValidationCheck(value); IsDirty_10 = true; _RF_10 = value; OnPropertyChanged("RF_10"); } }
        private string _RF_11; public string RF_11 { get { return _RF_11; } set { DoubleValidationCheck(value); IsDirty_11 = true; _RF_11 = value; OnPropertyChanged("RF_11"); } }
        private string _RF_12; public string RF_12 { get { return _RF_12; } set { DoubleValidationCheck(value); IsDirty_12 = true; _RF_12 = value; OnPropertyChanged("RF_12"); } }
        private string _RF_13; public string RF_13 { get { return _RF_13; } set { DoubleValidationCheck(value); IsDirty_13 = true; _RF_13 = value; OnPropertyChanged("RF_13"); } }
        private string _RF_14; public string RF_14 { get { return _RF_14; } set { DoubleValidationCheck(value); IsDirty_14 = true; _RF_14 = value; OnPropertyChanged("RF_14"); } }
        private string _RF_15; public string RF_15 { get { return _RF_15; } set { DoubleValidationCheck(value); IsDirty_15 = true; _RF_15 = value; OnPropertyChanged("RF_15"); } }
        private string _RF_16; public string RF_16 { get { return _RF_16; } set { DoubleValidationCheck(value); IsDirty_16 = true; _RF_16 = value; OnPropertyChanged("RF_16"); } }
        private string _RF_17; public string RF_17 { get { return _RF_17; } set { DoubleValidationCheck(value); IsDirty_17 = true; _RF_17 = value; OnPropertyChanged("RF_17"); } }
        private string _RF_18; public string RF_18 { get { return _RF_18; } set { DoubleValidationCheck(value); IsDirty_18 = true; _RF_18 = value; OnPropertyChanged("RF_18"); } }
        private string _RF_19; public string RF_19 { get { return _RF_19; } set { DoubleValidationCheck(value); IsDirty_19 = true; _RF_19 = value; OnPropertyChanged("RF_19"); } }
        private string _RF_20; public string RF_20 { get { return _RF_20; } set { DoubleValidationCheck(value); IsDirty_20 = true; _RF_20 = value; OnPropertyChanged("RF_20"); } }
        private string _RF_21; public string RF_21 { get { return _RF_21; } set { DoubleValidationCheck(value); IsDirty_21 = true; _RF_21 = value; OnPropertyChanged("RF_21"); } }
        private string _RF_22; public string RF_22 { get { return _RF_22; } set { DoubleValidationCheck(value); IsDirty_22 = true; _RF_22 = value; OnPropertyChanged("RF_22"); } }
        private string _RF_23; public string RF_23 { get { return _RF_23; } set { DoubleValidationCheck(value); IsDirty_23 = true; _RF_23 = value; OnPropertyChanged("RF_23"); } }
        private string _RF_24; public string RF_24 { get { return _RF_24; } set { DoubleValidationCheck(value); IsDirty_24 = true; _RF_24 = value; OnPropertyChanged("RF_24"); } }
        private string _RF_25; public string RF_25 { get { return _RF_25; } set { DoubleValidationCheck(value); IsDirty_25 = true; _RF_25 = value; OnPropertyChanged("RF_25"); } }
        private string _RF_26; public string RF_26 { get { return _RF_26; } set { DoubleValidationCheck(value); IsDirty_26 = true; _RF_26 = value; OnPropertyChanged("RF_26"); } }
        private string _RF_27; public string RF_27 { get { return _RF_27; } set { DoubleValidationCheck(value); IsDirty_27 = true; _RF_27 = value; OnPropertyChanged("RF_27"); } }
        private string _RF_28; public string RF_28 { get { return _RF_28; } set { DoubleValidationCheck(value); IsDirty_28 = true; _RF_28 = value; OnPropertyChanged("RF_28"); } }
        private string _RF_29; public string RF_29 { get { return _RF_29; } set { DoubleValidationCheck(value); IsDirty_29 = true; _RF_29 = value; OnPropertyChanged("RF_29"); } }
        private string _RF_30; public string RF_30 { get { return _RF_30; } set { DoubleValidationCheck(value); IsDirty_30 = true; _RF_30 = value; OnPropertyChanged("RF_30"); } }
        private string _RF_31; public string RF_31 { get { return _RF_31; } set { DoubleValidationCheck(value); IsDirty_31 = true; _RF_31 = value; OnPropertyChanged("RF_31"); } }
        private string _RF_32; public string RF_32 { get { return _RF_32; } set { DoubleValidationCheck(value); IsDirty_32 = true; _RF_32 = value; OnPropertyChanged("RF_32"); } }
        private string _RF_33; public string RF_33 { get { return _RF_33; } set { DoubleValidationCheck(value); IsDirty_33 = true; _RF_33 = value; OnPropertyChanged("RF_33"); } }
        private string _RF_34; public string RF_34 { get { return _RF_34; } set { DoubleValidationCheck(value); IsDirty_34 = true; _RF_34 = value; OnPropertyChanged("RF_34"); } }
        private string _RF_35; public string RF_35 { get { return _RF_35; } set { DoubleValidationCheck(value); IsDirty_35 = true; _RF_35 = value; OnPropertyChanged("RF_35"); } }
        private string _RF_36; public string RF_36 { get { return _RF_36; } set { DoubleValidationCheck(value); IsDirty_36 = true; _RF_36 = value; OnPropertyChanged("RF_36"); } }
        private string _RF_37; public string RF_37 { get { return _RF_37; } set { DoubleValidationCheck(value); IsDirty_37 = true; _RF_37 = value; OnPropertyChanged("RF_37"); } }
        private string _RF_38; public string RF_38 { get { return _RF_38; } set { DoubleValidationCheck(value); IsDirty_38 = true; _RF_38 = value; OnPropertyChanged("RF_38"); } }
        private string _RF_39; public string RF_39 { get { return _RF_39; } set { DoubleValidationCheck(value); IsDirty_39 = true; _RF_39 = value; OnPropertyChanged("RF_39"); } }
        private string _RF_40; public string RF_40 { get { return _RF_40; } set { DoubleValidationCheck(value); IsDirty_40 = true; _RF_40 = value; OnPropertyChanged("RF_40"); } }
        private string _RF_41; public string RF_41 { get { return _RF_41; } set { DoubleValidationCheck(value); IsDirty_41 = true; _RF_41 = value; OnPropertyChanged("RF_41"); } }
        private string _RF_42; public string RF_42 { get { return _RF_42; } set { DoubleValidationCheck(value); IsDirty_42 = true; _RF_42 = value; OnPropertyChanged("RF_42"); } }
        private string _RF_43; public string RF_43 { get { return _RF_43; } set { DoubleValidationCheck(value); IsDirty_43 = true; _RF_43 = value; OnPropertyChanged("RF_43"); } }
        private string _RF_44; public string RF_44 { get { return _RF_44; } set { DoubleValidationCheck(value); IsDirty_44 = true; _RF_44 = value; OnPropertyChanged("RF_44"); } }
        private string _RF_45; public string RF_45 { get { return _RF_45; } set { DoubleValidationCheck(value); IsDirty_45 = true; _RF_45 = value; OnPropertyChanged("RF_45"); } }
        private string _RF_46; public string RF_46 { get { return _RF_46; } set { DoubleValidationCheck(value); IsDirty_46 = true; _RF_46 = value; OnPropertyChanged("RF_46"); } }
        private string _RF_47; public string RF_47 { get { return _RF_47; } set { DoubleValidationCheck(value); IsDirty_47 = true; _RF_47 = value; OnPropertyChanged("RF_47"); } }
        private string _RF_48; public string RF_48 { get { return _RF_48; } set { DoubleValidationCheck(value); IsDirty_48 = true; _RF_48 = value; OnPropertyChanged("RF_48"); } }
        private string _RF_49; public string RF_49 { get { return _RF_49; } set { DoubleValidationCheck(value); IsDirty_49 = true; _RF_49 = value; OnPropertyChanged("RF_49"); } }
        private string _RF_50; public string RF_50 { get { return _RF_50; } set { DoubleValidationCheck(value); IsDirty_50 = true; _RF_50 = value; OnPropertyChanged("RF_50"); } }
        private string _RF_51; public string RF_51 { get { return _RF_51; } set { DoubleValidationCheck(value); IsDirty_51 = true; _RF_51 = value; OnPropertyChanged("RF_51"); } }
        private string _RF_52; public string RF_52 { get { return _RF_52; } set { DoubleValidationCheck(value); IsDirty_52 = true; _RF_52 = value; OnPropertyChanged("RF_52"); } }
        private string _RF_53; public string RF_53 { get { return _RF_53; } set { DoubleValidationCheck(value); IsDirty_53 = true; _RF_53 = value; OnPropertyChanged("RF_53"); } }
        private string _RF_54; public string RF_54 { get { return _RF_54; } set { DoubleValidationCheck(value); IsDirty_54 = true; _RF_54 = value; OnPropertyChanged("RF_54"); } }
        private string _RF_55; public string RF_55 { get { return _RF_55; } set { DoubleValidationCheck(value); IsDirty_55 = true; _RF_55 = value; OnPropertyChanged("RF_55"); } }
        private string _RF_56; public string RF_56 { get { return _RF_56; } set { DoubleValidationCheck(value); IsDirty_56 = true; _RF_56 = value; OnPropertyChanged("RF_56"); } }
        private string _RF_57; public string RF_57 { get { return _RF_57; } set { DoubleValidationCheck(value); IsDirty_57 = true; _RF_57 = value; OnPropertyChanged("RF_57"); } }
        private string _RF_58; public string RF_58 { get { return _RF_58; } set { DoubleValidationCheck(value); IsDirty_58 = true; _RF_58 = value; OnPropertyChanged("RF_58"); } }
        private string _RF_59; public string RF_59 { get { return _RF_59; } set { DoubleValidationCheck(value); IsDirty_59 = true; _RF_59 = value; OnPropertyChanged("RF_59"); } }
        private string _RF_60; public string RF_60 { get { return _RF_60; } set { DoubleValidationCheck(value); IsDirty_60 = true; _RF_60 = value; OnPropertyChanged("RF_60"); } }
        private string _RF_61; public string RF_61 { get { return _RF_61; } set { DoubleValidationCheck(value); IsDirty_61 = true; _RF_61 = value; OnPropertyChanged("RF_61"); } }
        private string _RF_62; public string RF_62 { get { return _RF_62; } set { DoubleValidationCheck(value); IsDirty_62 = true; _RF_62 = value; OnPropertyChanged("RF_62"); } }
        private string _RF_63; public string RF_63 { get { return _RF_63; } set { DoubleValidationCheck(value); IsDirty_63 = true; _RF_63 = value; OnPropertyChanged("RF_63"); } }
        private string _RF_64; public string RF_64 { get { return _RF_64; } set { DoubleValidationCheck(value); IsDirty_64 = true; _RF_64 = value; OnPropertyChanged("RF_64"); } }
        private string _RF_65; public string RF_65 { get { return _RF_65; } set { DoubleValidationCheck(value); IsDirty_65 = true; _RF_65 = value; OnPropertyChanged("RF_65"); } }
        private string _RF_66; public string RF_66 { get { return _RF_66; } set { DoubleValidationCheck(value); IsDirty_66 = true; _RF_66 = value; OnPropertyChanged("RF_66"); } }
        private string _RF_67; public string RF_67 { get { return _RF_67; } set { DoubleValidationCheck(value); IsDirty_67 = true; _RF_67 = value; OnPropertyChanged("RF_67"); } }
        private string _RF_68; public string RF_68 { get { return _RF_68; } set { DoubleValidationCheck(value); IsDirty_68 = true; _RF_68 = value; OnPropertyChanged("RF_68"); } }
        private string _RF_69; public string RF_69 { get { return _RF_69; } set { DoubleValidationCheck(value); IsDirty_69 = true; _RF_69 = value; OnPropertyChanged("RF_69"); } }
        private string _RF_70; public string RF_70 { get { return _RF_70; } set { DoubleValidationCheck(value); IsDirty_70 = true; _RF_70 = value; OnPropertyChanged("RF_70"); } }
        private string _RF_71; public string RF_71 { get { return _RF_71; } set { DoubleValidationCheck(value); IsDirty_71 = true; _RF_71 = value; OnPropertyChanged("RF_71"); } }
        private string _RF_72; public string RF_72 { get { return _RF_72; } set { DoubleValidationCheck(value); IsDirty_72 = true; _RF_72 = value; OnPropertyChanged("RF_72"); } }
        private string _RF_73; public string RF_73 { get { return _RF_73; } set { DoubleValidationCheck(value); IsDirty_73 = true; _RF_73 = value; OnPropertyChanged("RF_73"); } }
        private string _RF_74; public string RF_74 { get { return _RF_74; } set { DoubleValidationCheck(value); IsDirty_74 = true; _RF_74 = value; OnPropertyChanged("RF_74"); } }
        private string _RF_75; public string RF_75 { get { return _RF_75; } set { DoubleValidationCheck(value); IsDirty_75 = true; _RF_75 = value; OnPropertyChanged("RF_75"); } }
        private string _RF_76; public string RF_76 { get { return _RF_76; } set { DoubleValidationCheck(value); IsDirty_76 = true; _RF_76 = value; OnPropertyChanged("RF_76"); } }
        private string _RF_77; public string RF_77 { get { return _RF_77; } set { DoubleValidationCheck(value); IsDirty_77 = true; _RF_77 = value; OnPropertyChanged("RF_77"); } }
        private string _RF_78; public string RF_78 { get { return _RF_78; } set { DoubleValidationCheck(value); IsDirty_78 = true; _RF_78 = value; OnPropertyChanged("RF_78"); } }
        private string _RF_79; public string RF_79 { get { return _RF_79; } set { DoubleValidationCheck(value); IsDirty_79 = true; _RF_79 = value; OnPropertyChanged("RF_79"); } }
        private string _RF_80; public string RF_80 { get { return _RF_80; } set { DoubleValidationCheck(value); IsDirty_80 = true; _RF_80 = value; OnPropertyChanged("RF_80"); } }
        private string _RF_81; public string RF_81 { get { return _RF_81; } set { DoubleValidationCheck(value); IsDirty_81 = true; _RF_81 = value; OnPropertyChanged("RF_81"); } }
        private string _RF_82; public string RF_82 { get { return _RF_82; } set { DoubleValidationCheck(value); IsDirty_82 = true; _RF_82 = value; OnPropertyChanged("RF_82"); } }
        private string _RF_83; public string RF_83 { get { return _RF_83; } set { DoubleValidationCheck(value); IsDirty_83 = true; _RF_83 = value; OnPropertyChanged("RF_83"); } }
        private string _RF_84; public string RF_84 { get { return _RF_84; } set { DoubleValidationCheck(value); IsDirty_84 = true; _RF_84 = value; OnPropertyChanged("RF_84"); } }
        private string _RF_85; public string RF_85 { get { return _RF_85; } set { DoubleValidationCheck(value); IsDirty_85 = true; _RF_85 = value; OnPropertyChanged("RF_85"); } }
        private string _RF_86; public string RF_86 { get { return _RF_86; } set { DoubleValidationCheck(value); IsDirty_86 = true; _RF_86 = value; OnPropertyChanged("RF_86"); } }
        private string _RF_87; public string RF_87 { get { return _RF_87; } set { DoubleValidationCheck(value); IsDirty_87 = true; _RF_87 = value; OnPropertyChanged("RF_87"); } }
        private string _RF_88; public string RF_88 { get { return _RF_88; } set { DoubleValidationCheck(value); IsDirty_88 = true; _RF_88 = value; OnPropertyChanged("RF_88"); } }
        private string _RF_89; public string RF_89 { get { return _RF_89; } set { DoubleValidationCheck(value); IsDirty_89 = true; _RF_89 = value; OnPropertyChanged("RF_89"); } }
        private string _RF_90; public string RF_90 { get { return _RF_90; } set { DoubleValidationCheck(value); IsDirty_90 = true; _RF_90 = value; OnPropertyChanged("RF_90"); } }
        private string _RF_91; public string RF_91 { get { return _RF_91; } set { DoubleValidationCheck(value); IsDirty_91 = true; _RF_91 = value; OnPropertyChanged("RF_91"); } }
        private string _RF_92; public string RF_92 { get { return _RF_92; } set { DoubleValidationCheck(value); IsDirty_92 = true; _RF_92 = value; OnPropertyChanged("RF_92"); } }
        private string _RF_93; public string RF_93 { get { return _RF_93; } set { DoubleValidationCheck(value); IsDirty_93 = true; _RF_93 = value; OnPropertyChanged("RF_93"); } }
        private string _RF_94; public string RF_94 { get { return _RF_94; } set { DoubleValidationCheck(value); IsDirty_94 = true; _RF_94 = value; OnPropertyChanged("RF_94"); } }
        private string _RF_95; public string RF_95 { get { return _RF_95; } set { DoubleValidationCheck(value); IsDirty_95 = true; _RF_95 = value; OnPropertyChanged("RF_95"); } }
        private string _RF_96; public string RF_96 { get { return _RF_96; } set { DoubleValidationCheck(value); IsDirty_96 = true; _RF_96 = value; OnPropertyChanged("RF_96"); } }
        private string _RF_97; public string RF_97 { get { return _RF_97; } set { DoubleValidationCheck(value); IsDirty_97 = true; _RF_97 = value; OnPropertyChanged("RF_97"); } }
        private string _RF_98; public string RF_98 { get { return _RF_98; } set { DoubleValidationCheck(value); IsDirty_98 = true; _RF_98 = value; OnPropertyChanged("RF_98"); } }
        private string _RF_99; public string RF_99 { get { return _RF_99; } set { DoubleValidationCheck(value); IsDirty_99 = true; _RF_99 = value; OnPropertyChanged("RF_99"); } }
        private string _RF_100; public string RF_100 { get { return _RF_100; } set { DoubleValidationCheck(value); IsDirty_100 = true; _RF_100 = value; OnPropertyChanged("RF_100"); } }
        private string _RF_101; public string RF_101 { get { return _RF_101; } set { DoubleValidationCheck(value); IsDirty_101 = true; _RF_101 = value; OnPropertyChanged("RF_101"); } }
        private string _RF_102; public string RF_102 { get { return _RF_102; } set { DoubleValidationCheck(value); IsDirty_102 = true; _RF_102 = value; OnPropertyChanged("RF_102"); } }
        private string _RF_103; public string RF_103 { get { return _RF_103; } set { DoubleValidationCheck(value); IsDirty_103 = true; _RF_103 = value; OnPropertyChanged("RF_103"); } }
        private string _RF_104; public string RF_104 { get { return _RF_104; } set { DoubleValidationCheck(value); IsDirty_104 = true; _RF_104 = value; OnPropertyChanged("RF_104"); } }
        private string _RF_105; public string RF_105 { get { return _RF_105; } set { DoubleValidationCheck(value); IsDirty_105 = true; _RF_105 = value; OnPropertyChanged("RF_105"); } }
        private string _RF_106; public string RF_106 { get { return _RF_106; } set { DoubleValidationCheck(value); IsDirty_106 = true; _RF_106 = value; OnPropertyChanged("RF_106"); } }
        private string _RF_107; public string RF_107 { get { return _RF_107; } set { DoubleValidationCheck(value); IsDirty_107 = true; _RF_107 = value; OnPropertyChanged("RF_107"); } }
        private string _RF_108; public string RF_108 { get { return _RF_108; } set { DoubleValidationCheck(value); IsDirty_108 = true; _RF_108 = value; OnPropertyChanged("RF_108"); } }
        private string _RF_109; public string RF_109 { get { return _RF_109; } set { DoubleValidationCheck(value); IsDirty_109 = true; _RF_109 = value; OnPropertyChanged("RF_109"); } }
        private string _RF_110; public string RF_110 { get { return _RF_110; } set { DoubleValidationCheck(value); IsDirty_110 = true; _RF_110 = value; OnPropertyChanged("RF_110"); } }
        private string _RF_111; public string RF_111 { get { return _RF_111; } set { DoubleValidationCheck(value); IsDirty_111 = true; _RF_111 = value; OnPropertyChanged("RF_111"); } }
        private string _RF_112; public string RF_112 { get { return _RF_112; } set { DoubleValidationCheck(value); IsDirty_112 = true; _RF_112 = value; OnPropertyChanged("RF_112"); } }
        private string _RF_113; public string RF_113 { get { return _RF_113; } set { DoubleValidationCheck(value); IsDirty_113 = true; _RF_113 = value; OnPropertyChanged("RF_113"); } }
        private string _RF_114; public string RF_114 { get { return _RF_114; } set { DoubleValidationCheck(value); IsDirty_114 = true; _RF_114 = value; OnPropertyChanged("RF_114"); } }
        private string _RF_115; public string RF_115 { get { return _RF_115; } set { DoubleValidationCheck(value); IsDirty_115 = true; _RF_115 = value; OnPropertyChanged("RF_115"); } }
        private string _RF_116; public string RF_116 { get { return _RF_116; } set { DoubleValidationCheck(value); IsDirty_116 = true; _RF_116 = value; OnPropertyChanged("RF_116"); } }
        private string _RF_117; public string RF_117 { get { return _RF_117; } set { DoubleValidationCheck(value); IsDirty_117 = true; _RF_117 = value; OnPropertyChanged("RF_117"); } }
        private string _RF_118; public string RF_118 { get { return _RF_118; } set { DoubleValidationCheck(value); IsDirty_118 = true; _RF_118 = value; OnPropertyChanged("RF_118"); } }
        private string _RF_119; public string RF_119 { get { return _RF_119; } set { DoubleValidationCheck(value); IsDirty_119 = true; _RF_119 = value; OnPropertyChanged("RF_119"); } }
        private string _RF_120; public string RF_120 { get { return _RF_120; } set { DoubleValidationCheck(value); IsDirty_120 = true; _RF_120 = value; OnPropertyChanged("RF_120"); } }
        private string _RF_121; public string RF_121 { get { return _RF_121; } set { DoubleValidationCheck(value); IsDirty_121 = true; _RF_121 = value; OnPropertyChanged("RF_121"); } }
        private string _RF_122; public string RF_122 { get { return _RF_122; } set { DoubleValidationCheck(value); IsDirty_122 = true; _RF_122 = value; OnPropertyChanged("RF_122"); } }
        private string _RF_123; public string RF_123 { get { return _RF_123; } set { DoubleValidationCheck(value); IsDirty_123 = true; _RF_123 = value; OnPropertyChanged("RF_123"); } }
        private string _RF_124; public string RF_124 { get { return _RF_124; } set { DoubleValidationCheck(value); IsDirty_124 = true; _RF_124 = value; OnPropertyChanged("RF_124"); } }
        private string _RF_125; public string RF_125 { get { return _RF_125; } set { DoubleValidationCheck(value); IsDirty_125 = true; _RF_125 = value; OnPropertyChanged("RF_125"); } }
        private string _RF_126; public string RF_126 { get { return _RF_126; } set { DoubleValidationCheck(value); IsDirty_126 = true; _RF_126 = value; OnPropertyChanged("RF_126"); } }
        private string _RF_127; public string RF_127 { get { return _RF_127; } set { DoubleValidationCheck(value); IsDirty_127 = true; _RF_127 = value; OnPropertyChanged("RF_127"); } }
        private string _RF_128; public string RF_128 { get { return _RF_128; } set { DoubleValidationCheck(value); IsDirty_128 = true; _RF_128 = value; OnPropertyChanged("RF_128"); } }
        private string _RF_129; public string RF_129 { get { return _RF_129; } set { DoubleValidationCheck(value); IsDirty_129 = true; _RF_129 = value; OnPropertyChanged("RF_129"); } }
        private string _RF_130; public string RF_130 { get { return _RF_130; } set { DoubleValidationCheck(value); IsDirty_130 = true; _RF_130 = value; OnPropertyChanged("RF_130"); } }
        private string _RF_131; public string RF_131 { get { return _RF_131; } set { DoubleValidationCheck(value); IsDirty_131 = true; _RF_131 = value; OnPropertyChanged("RF_131"); } }
        private string _RF_132; public string RF_132 { get { return _RF_132; } set { DoubleValidationCheck(value); IsDirty_132 = true; _RF_132 = value; OnPropertyChanged("RF_132"); } }
        private string _RF_133; public string RF_133 { get { return _RF_133; } set { DoubleValidationCheck(value); IsDirty_133 = true; _RF_133 = value; OnPropertyChanged("RF_133"); } }
        private string _RF_134; public string RF_134 { get { return _RF_134; } set { DoubleValidationCheck(value); IsDirty_134 = true; _RF_134 = value; OnPropertyChanged("RF_134"); } }
        private string _RF_135; public string RF_135 { get { return _RF_135; } set { DoubleValidationCheck(value); IsDirty_135 = true; _RF_135 = value; OnPropertyChanged("RF_135"); } }
        private string _RF_136; public string RF_136 { get { return _RF_136; } set { DoubleValidationCheck(value); IsDirty_136 = true; _RF_136 = value; OnPropertyChanged("RF_136"); } }
        private string _RF_137; public string RF_137 { get { return _RF_137; } set { DoubleValidationCheck(value); IsDirty_137 = true; _RF_137 = value; OnPropertyChanged("RF_137"); } }
        private string _RF_138; public string RF_138 { get { return _RF_138; } set { DoubleValidationCheck(value); IsDirty_138 = true; _RF_138 = value; OnPropertyChanged("RF_138"); } }
        private string _RF_139; public string RF_139 { get { return _RF_139; } set { DoubleValidationCheck(value); IsDirty_139 = true; _RF_139 = value; OnPropertyChanged("RF_139"); } }
        private string _RF_140; public string RF_140 { get { return _RF_140; } set { DoubleValidationCheck(value); IsDirty_140 = true; _RF_140 = value; OnPropertyChanged("RF_140"); } }
        private string _RF_141; public string RF_141 { get { return _RF_141; } set { DoubleValidationCheck(value); IsDirty_141 = true; _RF_141 = value; OnPropertyChanged("RF_141"); } }
        private string _RF_142; public string RF_142 { get { return _RF_142; } set { DoubleValidationCheck(value); IsDirty_142 = true; _RF_142 = value; OnPropertyChanged("RF_142"); } }
        private string _RF_143; public string RF_143 { get { return _RF_143; } set { DoubleValidationCheck(value); IsDirty_143 = true; _RF_143 = value; OnPropertyChanged("RF_143"); } }


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
