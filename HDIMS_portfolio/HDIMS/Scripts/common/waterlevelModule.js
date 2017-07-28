/**
* 수위 이력조회 & 수위 검정 페이지 공통 
*
*/


Ext.define('WaterLevelModel', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: [
          { name: 'DAMCD', type: 'string' }
        , { name: 'DAMNM', type: 'string' }
        , { name: 'OBSDT', type: 'string' }
        , { name: 'OBSCD', type: 'string' }
        , { name: 'OBSNM', type: 'string' }
        , { name: 'WL', type: 'float' }
        , { name: 'FLW', type: 'float' }
        , { name: 'EXVL', type: 'float' }
        , { name: 'EXCD', type: 'string' }
        , { name: 'EXCOLOR', type: 'string' }
//        , { name: 'ETCCD', type: 'string' }
//        , { name: 'ETCTITLE', type: 'string' }
        , { name: 'CHKEMPNO', type: 'string' }
        , { name: 'CHKEMPNM', type: 'string' }
        , { name: 'CHKDT', type: 'string' }
        , { name: 'CGEMPNO', type: 'string' }
        , { name: 'CGEMPNM', type: 'string' }
        , { name: 'CGDT', type: 'string' }
        , { name: 'CNRSN', type: 'string' }
        , { name: 'CNDS', type: 'string' }
        , { name: 'EDEXWAY', type: 'string' }
        , { name: 'EDEXLVL', type: 'string' }
        , { name: 'ID', type: 'string' }
    ]
});

var wlDataColumns =
    [
        { text: '<div style="font-weight: bold; height: 45px;">측정일시</div>', flex: 1.4, minWidth: 105, sortable: true, align: 'center', dataIndex: 'OBSDT', locked: true, renderer: formatDate },
        { text: '관측국명', flex: 1.2, minWidth: 90, sortable: true, align: 'center', dataIndex: 'OBSNM' },
        { text: '원시자료',
            columns: [
                { text: '수위', flex: 0.5, width: 70, sortable: false, align: 'center', dataIndex: 'WL' },
                { text: '유량', flex: 0.5, width: 70, sortable: false, align: 'center', dataIndex: 'FLW' },
                { text: '등급', flex: 0.5, width: 70, sortable: false, align: 'center', dataIndex: 'PEXCD', renderer: getColor }
            ]
        },
        { text: '추정치자료',
            columns: [
                { text: '수위', flex: 0.5, width: 70, sortable: false, align: 'center', dataIndex: 'EXVL' },
                { text: '등급', flex: 0.5, width: 70, sortable: false, align: 'center', dataIndex: 'EXCD', renderer: getColor }
            ]
        },
        { text: '기타범례', flex: 1, minWidth: 90, sortable: false, align: 'center', dataIndex: 'ETCTITLE' },
        { text: '보정자', flex: 1, minWidth: 80, sortable: true, align: 'center', dataIndex: 'EXEMPNM' },
        { text: '보정일시', flex: 1.4, minWidth: 105, sortable: true, align: 'center', dataIndex: 'EXDT', renderer: formatDate },
        { text: '확인자', flex: 1, minWidth: 80, sortable: true, align: 'center', dataIndex: 'CHKEMPNM' },
        { text: '확인일시', flex: 1.4, minWidth: 105, sortable: true, align: 'center', dataIndex: 'CHKDT', renderer: formatDate }
    ];


/* 모델 정의 */
var WLDataSearchModel = Ext.define('WLDataHistoryModel', {
    extend: 'Ext.data.Model',
    fields: [
//        { name: 'DAMCD', type: 'string' },
//        { name: 'OBSCD', type: 'string' },
//        { name: 'OBSNM', type: 'string' },
//        { name: 'OBSDT', type: 'string' },
        { name: 'CGDT', type: 'string' },
        { name: 'WL', type: 'string' },
        { name: 'FLW', type: 'string' },
//        { name: 'EXCD', type: 'string' },
//        { name: 'EXCOLOR', type: 'string' },
//        { name: 'PETCTITLE', type: 'string' },
        { name: 'CGEMPNM', type: 'string' },
        { name: 'CNRSN', type: 'string' },
        { name: 'CNDS', type: 'string' },
        { name: 'EDEXWAYCONT', type: 'string' },
        { name: 'EDEXLVLCONT', type: 'string' }
    ]
});

var wlDataHistoryColumns =
[
    //{ text: '<div style="font-weight: bold; height: 45px;">측정일시</div>', flex: 1.4, minWidth: 40, sortable: true, align: 'center', dataIndex: 'OBSDT', renderer: formatDate },
    //{ text: '관측국명', flex: 1.2, minWidth: 40, sortable: true, align: 'center', dataIndex: 'OBSNM' },
    {text: '보정일시', width: 105, sortable: true, align: 'center', dataIndex: 'CGDT', renderer: formatDate },
    { text: '수위', width: 70, sortable: false, align: 'center', dataIndex: 'WL' },
    { text: '유량', width: 70, sortable: false, align: 'center', dataIndex: 'FLW' },
    { text: '보정방법', width: 100, sortable: false, align: 'center', dataIndex: 'EDEXWAYCONT' },
    { text: '보정등급', width: 150, sortable: false, align: 'center', dataIndex: 'EDEXLVLCONT' },
    { text: '보정자', width: 80, sortable: false, align: 'center', dataIndex: 'CGEMPNM' },
    { text: '사유', flex: 1, width: 200, sortable: false, align: 'center', dataIndex: 'CNRSN' },
    { text: '내역', flex: 1, width: 200, sortable: false, align: 'center', dataIndex: 'CNDS' }
];


