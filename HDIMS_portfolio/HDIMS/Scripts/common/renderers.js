var formatDate = function (v) {
    var cText = "";
    if (v) {
        if (v.length >= 8) {
            cText = v.substring(0, 4) + "-" + v.substring(4, 6) + "-" + v.substring(6, 8);
        }
        if (v.length >= 10) {
            cText += " " + v.substring(8, 10);
        }
        if (v.length >= 12) {
            cText += ":" + v.substring(10, 12);
        }
    }
    return cText;
}

function renderTdate(value) {
    return value.substring(0, 4) + "/" + value.substring(4, 6) + "/" + value.substring(6, 8);
}
function renderRdate(value) {
    return value.substring(0, 4) + "/" + value.substring(4, 6) + "/" + value.substring(6, 8) + " " + value.substring(8, 10) + ":" + value.substring(10, 12);
}
/////////////////////////////////////////// function 시작 /////////////////////////////////////////////////

function getColor(value, metaData, record, rowIndex, colIndex, store, view) {
    var retVal = '<div style="background-color:#' + record.data.EXCOLOR + '">' + value + '</div>';
    return retVal;
}
/////////////////////////////////////////// function 종료 /////////////////////////////////////////////////


//// 'VTypes' 추가하기
//Ext.apply(Ext.form.VTypes, {
//    daterange: function (val, field) {
//        var date = field.parseDate(val);

//        if (!date) {
//            return false;
//        }
//        if (field.startDateField) {
//            var start = Ext.getCmp(field.startDateField);
//            if (!start.maxValue || (date.getTime() != start.maxValue.getTime())) {
//                start.setMaxValue(date);
//                start.validate();
//            }
//        }
//        else if (field.endDateField) {
//            var end = Ext.getCmp(field.endDateField);
//            if (!end.minValue || (date.getTime() != end.minValue.getTime())) {
//                end.setMinValue(date);
//                end.validate();
//            }
//        }


//        return true;
//    },

//    daterangeText: '시작날짜가 끝날짜보다 커야 합니다.'
//});
