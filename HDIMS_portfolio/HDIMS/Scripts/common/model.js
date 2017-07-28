/* 품질등급 모델 정의 */
Ext.define('Common.ExCodeModel', {
    extend: 'Ext.data.Model',
    fields: [
          { name: 'EXCD', type: 'string' }
        , { name: 'EXTP', type: 'string' }
        , { name: 'EXORD', type: 'int' }
        , { name: 'EXCONT', type: 'string' }
        , { name: 'EXNOTE', type: 'string' }
        , { name: 'EXYN', type: 'string' }
        , { name: 'EXCOLOR', type: 'string' }
    ]
});

/* 기타범례 모델 정의 */
Ext.define('Common.EtcCodeModel', {
    extend: 'Ext.data.Model',
    fields: [
          { name: 'ETCCD', type: 'string' }
        , { name: 'ETCTP', type: 'string' }
        , { name: 'ETCTITLE', type: 'string' }
        , { name: 'ETCCONT', type: 'string' }
        , { name: 'ETCYN', type: 'string' }
        , { name: 'ETCCOLOR', type: 'string' }
    ]
});

/* 사용자 정보 모델 정의 */
Ext.define('Common.UserInfo', {
    extend: 'Ext.data.Model',
    fields: [
          { name: 'EMPNO', type: 'string' }
        , { name: 'EMPNM', type: 'string' }
        , { name: 'PASSWD', type: 'string' }
        , { name: 'DEPT', type: 'string' }
        , { name: 'ORGLEVEL', type: 'string' }
        , { name: 'EMAIL', type: 'string' }
        , { name: 'WEATHER_AREA', type: 'string' }
        , { name: 'DEFUI', type: 'int' }
        , { name: 'REGDT', type: 'string' }
        , { name: 'DEFUI_KOR', type: 'string' }
        , { name: 'HPTEL', type: 'string' }
        , { name: 'HOMETEL', type: 'string' }
        , { name: 'OFFICETEL', type: 'string' }
    ]
});

/* 댐코드 정보 모델 정의 */
Ext.define('Common.DamCode', {
    extend: 'Ext.data.Model',
    idProperty: 'DAMCD',
    fields: [
        { name: 'DAMCD', type: 'string' },
        { name: 'DAMNM', type: 'string' },
        { name: 'DAMTP', type: 'string' }
    ]
});

//댐구분 모델
var damTypeModel = Ext.define('Common.DamTypeModel', {
    extend: 'Ext.data.Model',
    idProperty: 'DAMTYPE',
    fields: [
        { name: 'DAMTYPE', type: 'string' },
        { name: 'DAMTPNM', type: 'string' }
    ]
});
