//검정등급 우량
var gl_excd_W = [
//{ value: '', text: '전체' },
{ value: '0992', text: '결측수위' },
{ value: '0971', text: '상한초과' },
{ value: '0972', text: '하한미달' },
{ value: '0954', text: '수위급변' },
{ value: '0961', text: '수위불변' },
{ value: '0952', text: '-1배이하' },
{ value: '0953', text: '3배이상' },
{ value: '0951', text: '수위값0' }
];

//검정등급 수위
var gl_excd_R = [
//{ value: '', text: '전체'},
{ value: '0991', text: '결측우량' },
{ value: '0912', text: '과대값2' },
{ value: '0911', text: '과대값1' },
{ value: '0931', text: '누계감소' },
{ value: '0922', text: 'RDS 280%초과' },
{ value: '0921', text: 'RDS 20%미만 ' }
];

var gl_excd_DAM = [
    { value: 'RWL', text: '저수위' },
    { value: 'OSPILWL', text: '방수로수위' },
    { value: 'ETCIQTY2', text: '기타유입량2' },
    { value: 'EDQTY', text: '발전방류량' },
    { value: 'ETCEDQTY', text: '기타발전방류량' },
    { value: 'SPDQTY', text: '수문방류량' },
    { value: 'ETCDQTY1', text: '기타발전방류량1' },
    { value: 'ETCDQTY2', text: '기타발전방류량2' },
    { value: 'ETCDQTY3', text: '기타발전방류량3' },
    { value: 'OTLTDQTY', text: '아울렛방류량' },
    { value: 'ITQTY1', text: '취수1' },
    { value: 'ITQTY2', text: '취수2' },
    { value: 'ITQTY3', text: '취수3' }
];

//검정등급 공통
var gl_excd_C = [
{text:	'0100', value:	'양호한자료'	}
];

//보정등급
var gl_edexlvl = [
{ text: '전체', value: '' },
{ text: '원시수문 자료', value: '0' },
{ text: 'S/W,RTU및분기', value: '1' },
{ text: '년간,대외 자료', value: '2' }
];

var gl_edexlvl2 = [
{ text: '선택하세요', value: '' },
{ text: '원시수문 자료', value: '0' },
{ text: 'S/W,RTU및분기', value: '1' },
{ text: '년간,대외 자료', value: '2' }
];

var gl_edexway_WL2 = [
{ text: '선택하세요', value: '' },
{ text: '자동계산', value: '1' },
{ text: '목측법', value: '2' },
{ text: '이동평균', value: '3' },
{ text: '예년자료', value: '4' },
{ text: '과거자료', value: '5' },
{ text: '기타방법', value: '6' }
];

var gl_edexway_RF2 = [
{ text: '선택하세요', value: '' },
{ text: '자동보정', value: '1' },
{ text: '목측보정', value: '2' },
{ text: 'RDS법', value: '3' },
{ text: '보간법', value: '4' },
{ text: '신뢰도분석', value: '5' },
{ text: '인근지자체', value: '6' },
{ text: '기상청AWS', value: '7' },
{ text: '회귀분석', value: '8' },
{ text: '기타방법', value: '9' }
];

var gl_edexway_DD2 = [
{ text: '선택하세요', value: '' },
{ text: '자동계산', value: '1' },
{ text: '목측법', value: '2' },
{ text: '이동평균', value: '3' },
{ text: '예년자료', value: '4' },
{ text: '과거자료', value: '5' },
{ text: '기타방법', value: '6' }
];

//사원
var gl_empno = [
{ text: '전체', value: '' }
];

var gl_equipExCode = [
{ text: '수위계 점검 결과 이상', value: 'WL_SEN' },
{ text: '우량계 점검 결과 이상', value: 'RF_SEN' },
{ text: '수질측정센서 결과 이상', value: 'WQ_SEN' },
{ text: '기타 센서 이상', value: 'ETC_SEN' },
{ text: '상전 Off 및 Battery 저전압 이상', value: 'PWRSTS' },
{ text: 'RTU 메모리 이상', value: 'RTU_MEMORY' },
{ text: 'RTU 외부 전원 Off에 의해 Reset', value: 'RTU_RESET' },
{ text: 'RTY 자체점검(와치독)에 의해 Reset', value: 'WDT_RESET' },
{ text: 'LAN Port 점검 결과 이상', value: 'LAN_PORT' },
{ text: 'CDMA 모뎀 점검 결과 이상', value: 'CDMA_MODEM' },
{ text: 'T200 위성(Serial) 통신 포트 이상', value: 'VSAT_PORT' },
{ text: 'CDMA 통신 포트 Open 이상', value: 'CDMA_PORT' },
{ text: '유선망 통신 포트 Open 이상', value: 'WIRE_PORT' },
{ text: 'Multicast 소켓 에러 발생', value: 'MULTICAST_SOCKET' },
{ text: '위성 이벤트 전송 통신 이상', value: 'VSAT_EVENT' },
{ text: 'CDMA 이벤트 전송 통신 이상', value: 'CDMA_EVENT' },
{ text: '유선망 이벤트 전송 통신 이상', value: 'WIRE_EVENT' },
{ text: 'UDP 이벤트 전송 통신 에러', value: 'UDP_EVENT' },
{ text: '위성단말(IDU) Ping 점검 이상', value: 'IDU_PING' },
{ text: '위성통신 수신 감도 기록', value: 'SNR' },
{ text: 'PRIMARY 통신 이상', value: 'PRIMARY_DATA' },
{ text: 'SECONDARY 호출 여부', value: 'SECONDARY_CALL' },
{ text: '결측', value: 'DATA_STATUS' },
{ text: '베터리 상태 이상', value: 'BATTSTS' },
{ text: '베터리 전압 이상', value: 'BATTVOLT' },
{ text: '도어상태 이상', value: 'DOORSTS' }
];



