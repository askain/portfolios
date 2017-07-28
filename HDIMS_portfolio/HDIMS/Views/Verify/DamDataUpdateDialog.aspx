<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub5.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    댐운영자료 보정
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PreHeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    .claro .dijitDialogUnderlay { background:transparent; } 
</style>
<%
    EmpData empdata = new EmpData();
%>
<script type="text/javascript" src="/Scripts/common/renderers.js"></script>
<script type="text/javascript">
    dojo.require("dijit.dijit");
    dojo.require("dijit.Editor");
    dojo.require("dijit.Dialog");
    dojo.require("dijit.Calendar");
    dojo.require("dijit.ColorPalette");
    dojo.require("dijit.Tooltip");
    dojo.require("dijit.layout.AccordionContainer");
    dojo.require("dijit.layout.TabContainer");
    dojo.require("dijit.layout.LinkPane");
    dojo.require("dijit.layout.BorderContainer");
    dojo.require("dijit.layout.SplitContainer");
    dojo.require("dijit.layout.ContentPane");
    dojo.require("dojox.layout.ExpandoPane");
    dojo.require("dijit.form.FilteringSelect");
    dojo.require("dijit.form.Button");
    dojo.require("dijit.form.ToggleButton");
    dojo.require("dijit.form.ComboBox");
    dojo.require("dijit.form.Select");
    dojo.require("dijit.form.DateTextBox");
    dojo.require("dijit.form.Form");
    dojo.require("dojox.data.JsonRestStore");
    dojo.require("dojox.widget.PlaceholderMenuItem");
    dojo.require("dojox.grid.enhanced.plugins.Filter");
    dojo.require("dojox.grid.enhanced.plugins.exporter.CSVWriter");
    dojo.require("dojox.grid.enhanced.plugins.Printer");
    dojo.require("dojox.grid.enhanced.plugins.Cookie");
    dojo.require("dojox.grid.enhanced.plugins.IndirectSelection");
    dojo.require("dojox.grid.enhanced.plugins.NestedSorting");
    dojo.require("dojox.grid.enhanced.plugins.Selector");
    dojo.require("dojox.grid.enhanced.plugins.Menu");
    dojo.require("dojox.grid.enhanced.plugins.DnD");
    dojo.require("dojox.grid.enhanced.plugins.Search");
    dojo.require("dojox.grid.enhanced.plugins.CellMerge");
    dojo.require("dojox.grid.enhanced.plugins.Pagination");
    dojo.require("dojox.grid.enhanced.plugins.GridSource");
    dojo.require("dojox.grid.EnhancedGrid");
    dojo.require("dojox.grid.cells.dijit");
    dojo.require("dojo.date");
    dojo.require("dojo.date.locale");
    dojo.require("dojo.data.ItemFileWriteStore");
    dojo.require("dojo.fx");

    dojo.require("dojo.parser");

    var currDate = new Date();
    var prevDate = new Date();
    prevDate.setDate(currDate.getDate() - 1);
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";
    
    var damStore, damGrid, historyStore, historyGrid, exItemGrid, damTpOptions, damCdOptions,
        damTpCombo, damCdCombo, startDtCal, startHrCombo, endDtCal, endHrCombo, dataTpCombo,
        damGridItems, pStartDtCal, pEndDtCal, sStartDtCal, sEndDtCal, startDtVal, endDtVal;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var glUpdateCellVal = "";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };
    var pWin; //Opener Window Object
    
    var windowClose = function() {
        window.close();
    }
    var applyCancelAll = function () {
        //exItemGrid.edit.cancel();
        //dijit.byId("updateAllDlg").hide();
        windowClose();
    }

    var getSelectValue = function (id) {
        var sel = document.getElementById(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    var objectFormatter = function (value, rownIndex) {
        if (typeof arguments[0] == 'object') {
            return "{ ... }";
        }
        return value;

    };

    //손으로 수정했을때 직접수정으로 변경
    var setCustomEdited = function (id, rowIndex, inAttrName, value) {
        //store 세팅
        var targetRec = exItemGrid.getItem(rowIndex);
        //exItemGrid.edit.cancel();
        exItemGrid.store.setValue(targetRec, inAttrName, value);
        if (inAttrName == "act") {
            //var item = this.getItem(inRowIndex);
            switch (value) {
                case "미적용":
                    exItemGrid.store.setValue(targetRec, "value", "");
                    break;
                case "직접수정":
                    exItemGrid.edit.setEditCell(exItemGrid.getCell(5), rowIndex);
                    exItemGrid.focus.setFocusIndex(rowIndex, 5);
                    break;
                case "전일평균":
                    calcYest(targetRec);
                    break;
                case "지정시간평균":
                    calcPrev(targetRec);
                    break;
                case "선형보간":
                    calcLinear(targetRec);
                    break;
                case "지수함수곡선":
                    calcCurve(targetRec);
                    break;
                default:
                    break;

            }
        }

    };
    var exComboOptionList = ["미적용", "직접수정", "전일평균", "지정시간평균", "선형보간", "지수함수곡선"];
    var exComboFormatter = function (val, rowIndex, cell) {
        var item = exItemGrid.getItem(rowIndex);
        var id = exItemGrid.store.getIdentity(item);
        var selId = id + "Combo";
        var opts = [];
        for (var rr = 0; rr < exComboOptionList.length; rr++) {
            if (val == exComboOptionList[rr]) {
                opts[rr] = { label: exComboOptionList[rr], value: exComboOptionList[rr], selected: true };
            } else {
                opts[rr] = { label: exComboOptionList[rr], value: exComboOptionList[rr] };
            }

        }
        return new dijit.form.Select({
            style: { width: '100px' },
            options: opts,
            onChange: function (val) {
                exItemGrid.edit.cancel();
                exItemGrid.onSelected(rowIndex);
                setCustomEdited(id, rowIndex, 'act', val);
            }
        });
    }
    //보정방법 : 일괄수정='UA',전일평균='PA',지정시간평균:'SA',선형보간:'LE'
    var exItemSt = [
        { field: 'sdate', name: "시작시간", width: '100px;', styles: 'text-align: center;', formatter: convStrToDateMin },
        { field: 'edate', name: "끝시간", width: '100px;', styles: 'text-align: center;', formatter: convStrToDateMin },
        { field: 'selectcnt', name: "갯수", width: '30px;', styles: 'text-align: center;' },
        { field: 'name', name: "항목", width: '90px;', styles: 'text-align: center;' },
        { field: 'act', name: '보정방법', width: '100px', styles: 'text-align:center;cursor:hand;', formatter: exComboFormatter },
        { field: 'value', name: "산출값", width: '70px;', styles: 'text-align: center;cursor:hand;', formatter: objectFormatter, editable: true }
    ];

    var setExItemData = function () {
        var cellItems = pWin.damGrid.layout.cells;
        var cellSelected = pWin.getSelectedItems();

        for (var i = 0; i < cellItems.length; i++) {
            var cell = cellItems[i];
            if (cellItems[i].editable == true && cellSelected[cell.field].length > 0) {
                var selectcnt = cellSelected[cell.field].length
                var editableField = cellSelected[cell.field];
                var sdate = editableField[0].OBSDT;
                var edate = editableField[0].OBSDT;

                //최소 최대 일시를 구함.
                for (var j = 0; j < editableField.length; j++) {
                    if (sdate > editableField[j].OBSDT) {
                        sdate = editableField[j].OBSDT;
                    } else if (edate < editableField[j].OBSDT) {
                        edate = editableField[j].OBSDT;
                    }
                }

                var newItem = { id: cell.field, sdate: sdate, edate: edate, selectcnt: selectcnt, name: cell.name.replace("</br>", ""), act: '미적용', value: '' }
                exItemStore.newItem(newItem);
            }
        }
        exItemGrid.render();
    };

    var exItemStore = new dojo.data.ItemFileWriteStore({ data: { identifier: 'id', label: 'id', items: []} });

    var showUpdateAllWin = function () {

        openChartPanel();
        //선택한 자료가 있는지 확인
        var selected = damGrid.plugin("selector").getSelected("cell");
        if (selected.length < 1) {
            alert("보정할 자료를 선택하여 합니다.");
            return;
        }

        //이전 수정값을 초기화.
        exItemStore.revert();

        //어는 버튼이 클릭되었는지 확인

        if (!exItemGrid) {
            exItemGrid = new dojox.grid.EnhancedGrid({
                id: "exItemGrid",
                structure: exItemSt,
                store: exItemStore,
                singleClickEdit: true,
                selectionMode: "none",
                canSort: function (colIndex) {
                    return false;
                }
            }, document.createElement("div"));
            exItemGrid.placeAt(dojo.byId("exItemGridPanel"));
            exItemGrid.startup();
        }

        setExItemData();
        dijit.byId('updateAllDlg').show();
    };



    var applyUpdateAll = function () {
        //산출값을 읽어온다. 
        //exItemStore
        //exItemGrid
        //선택된 값을 읽어온다.
        //damStore, damGrid
        //dijit.byId('updateAllDlg').hide();
        $(document.body).mask("적용중입니다....");
        exItemGrid.edit.apply();
        var cellSelected = pWin.getSelectedItems();
        var i = 0, len = exItemGrid.rowCount;

        var saveItems = [];

        for (i = 0; i < len; i++) {
            var item = exItemGrid.getItem(i);
            var itemact = exItemGrid.store.getValue(item, "act");
            var itemval = exItemGrid.store.getValue(item, "value");
            if (typeof (itemval) == 'object') {     //지수함수곡선 등일 경우는 기존방식으로는 [0]의 값밖에 가져오지 못하므로...
                itemval = exItemGrid.store.getValues(item, "value");
            }
            if (item) {
                var target = item.id;
                var cells = cellSelected[target];
                if ((itemact == "선형보간" || itemact == "지수함수곡선") && typeof (itemval) == 'object') {
                    var linearItems = itemval;
                    for (var j = 0; j < cells.length; j++) {
                        var targetRow = cells[j];
                        for (var k = 0; k < linearItems.length; k++) {
                            var linearItem = linearItems[k];
                            if (targetRow.OBSDT == linearItem.OBSDT) {
                                var it = pWin.damGrid.getItem(cells[j].row);
                                saveItems[saveItems.length] = { item: it, field: target, value: parseFloat(linearItem.BOGAN) };
                                saveItems[saveItems.length] = { item: it, field: target + "_CK", value: "1" };
                            }
                        }
                    }
                }
                else if (itemact != "미적용" && isNaN(itemval) == false && itemval.replace(/ /gi, '') != '') {
                    for (var j = 0; j < cells.length; j++) {
                        var it = pWin.damGrid.getItem(cells[j].row);
                        saveItems[saveItems.length] = { item: it, field: target, value: parseFloat(itemval) };
                        saveItems[saveItems.length] = { item: it, field: target + "_CK", value: "1" };

                    }
                }
            }
        }

        if (saveItems.length == 0) {
            $(document.body).unmask();
            return;
        }

        i = 0;
        var tmpRec;
        var ro = new RepeatingOperation(function () {
            tmpRec = saveItems[i].item;
            pWin.damGrid.store.changing(tmpRec);
            tmpRec[saveItems[i].field] = saveItems[i].value;
            if (++i < saveItems.length) { ro.step(); }
            else {
                pWin.damGrid.update();
                $(document.body).unmask();
                pWin.loadChartPanel();
                this.close();
            }
        }, 100);
        ro.step();
    };
    function setAvg(params, targetRec, act) {
        //asdfasd.asdfasd();
        //평균값 구하기
        var xhrArgs = {
            url: '/Verify/GetDamDataAvg',
            //postData: "Some random text", 
            //handleAs: "text", 
            content: params,
            load: function (data) {
                var jsonData = dojo.fromJson(data);
                if (jsonData == null || jsonData == "") {
                    exItemGrid.store.setValue(targetRec, "value", "Error");
                }
                //alert(jsonData);
                exItemGrid.store.setValue(targetRec, "act", act);
                exItemGrid.store.setValue(targetRec, "value", jsonData);
            },
            error: function (error) {
                exItemGrid.store.setValue(targetRec, "value", "Error");
            }
        };
        var deferred = dojo.xhrPost(xhrArgs);
    };
    //지정시간평균
    var calcPrev = function (targetRec) {
        var pStartDt = dijit.byId('pStartDt').get('value')
        var pStartHr = getSelectValue('pStartHr');
        var pEndDt = dijit.byId('pEndDt').get('value')
        var pEndHr = getSelectValue('pEndHr');

        var cellSelected = pWin.getSelectedItems();
        //        var targetRec = exItemGrid.selection.getSelected()[0];

        //        if (targetRec == undefined) {
        //            alert("적용할 항목을 선택하세요.");
        //            return;
        //        }

        var target = targetRec.id[0];
        var rec = cellSelected[target][0];
        if (rec == undefined) {
            alert("선택하지 않은 항목입니다.");
            return;
        }

        var damCd = rec["DAMCD"];
        //var dataTp = getSelectValue("dataTp");
        var startDt = dojo.date.locale.format(pStartDt, datePattern2) + pStartHr + '00';
        var endDt = dojo.date.locale.format(pEndDt, datePattern2) + pEndHr + '00';

        var params = {
            damcd: damCd,
            startdt: startDt,
            enddt: endDt,
            targetcolumn: target
        };
        setAvg(params, targetRec, "지정시간평균");

    };
    //전일평균
    var calcYest = function (targetRec) {
        var cellSelected = pWin.getSelectedItems();
        var target = targetRec.id[0];
        var rec = cellSelected[target][0];
        if (rec == undefined) {
            alert("선택하지 않은 항목입니다.");
            return;
        }

        var damCd = rec["DAMCD"];
        //var dataTp = getSelectValue("dataTp");
        var startDt = rec["OBSDT"];
        var endDt = startDt;

        //최소 최대 일시를 구함.
        for (var i = 0; i < cellSelected[target].length; i++) {
            rec = cellSelected[target][i];
            if (startDt > rec["OBSDT"]) {
                startDt = rec["OBSDT"];
            } else if (endDt < rec["OBSDT"]) {
                endDt = rec["OBSDT"];
            }
        }

        //전일평균은 마지막선택시간부터 24시간을 계산  
        endDt = startDt;
        //어제 날짜로 변경.
        tempStartDt = dojo.date.locale.parse(startDt, datePattern3);
        tempStartDt.setDate(tempStartDt.getDate() - 1);
        startDt = dojo.date.locale.format(tempStartDt, datePattern3);

        var params = {
            damcd: damCd,
            startdt: startDt,
            enddt: endDt,
            targetcolumn: target
        };
        setAvg(params, targetRec, "전일평균");

    };
    function setLinear(params, targetRec, act) {
        var xhrArgs = {
            url: '/Verify/GetDAMLinearInterpolationResult',
            //postData: "Some random text", 
            //handleAs: "text", 
            content: params,
            load: function (data) {
                var jsonData = dojo.fromJson(data);
                if (jsonData == null || jsonData == "") {
                    exItemGrid.store.setValue(targetRec, "value", "Error");
                }

                //alert(jsonData);
                exItemGrid.store.setValue(targetRec, "act", act);
                exItemGrid.store.setValue(targetRec, "value", jsonData);
            },
            error: function (error) {
                exItemGrid.store.setValue(targetRec, "value", "Error");
            }
        };
        var deferred = dojo.xhrPost(xhrArgs);
    }

    var calcCurve = function (targetRec) {
        calcLinear(targetRec, "지수함수곡선")
    };
    // 선형보간
    var calcLinear = function (targetRec, act) {
        if (act) {
            act = "지수함수곡선";
        } else {
            act = "선형보간";
        }

        var cellSelected = pWin.getSelectedItems();
        var target = targetRec.id[0];
        var rec = cellSelected[target][0];
        if (rec == undefined) {
            alert("선택하지 않은 항목입니다.");
            return;
        }

        //연속구간 검정.
        var cells = cellSelected[target];
        var startRowNum = cells[0].row;
        var endRowNum = cells[cells.length - 1].row;
        for (var i = 0, j = startRowNum; i < cells.length; i++) {
            if (j != cells[i].row) {
                alert('선형보간은 연속 데이터만 가능합니다.');
                return;
            }

            if (j < endRowNum) j++;
            else j--;
        }

        var damCd = rec["DAMCD"];
        var dataTp = pWin.getSelectValue("dataTp");
        var startDt = rec["OBSDT"];
        var logBase = dijit.byId('logBase').getValue().toString();
        var endDt = startDt;

        //최소 최대 일시를 구함.
        for (var i = 0; i < cellSelected[target].length; i++) {
            rec = cellSelected[target][i];
            if (startDt > rec["OBSDT"]) {
                startDt = rec["OBSDT"];
            } else if (endDt < rec["OBSDT"]) {
                endDt = rec["OBSDT"];
            }
        }

        var params = {
            damcd: damCd,
            startdt: startDt,
            enddt: endDt,
            dataTp: dataTp,
            targetcolumn: target,
            logbase: logBase,
            act: act
        };
        setLinear(params, targetRec, act);
    };

    window.onbeforeunload = function () {
        pWin.unmaskBody();
    }

    dojo.ready(function () {
        pWin = window.dialogArguments;
        var pStartDt = pWin.startDtCal.getValue();
        startDtVal = dojo.date.locale.format(pStartDt, datePattern);
        endDtVal = dojo.date.locale.format(currDate, datePattern);
        yesterDtVal = dojo.date.locale.format(prevDate, datePattern);

        setOptions("pStartHr", gl_24times);
        setOptions("pEndHr", gl_24times);
        setSelectOption("pEndHr", "24");

        pStartDtCal = new dijit.form.DateTextBox({
            id: "pStartDt",
            name: "pStartDt",
            value: startDtVal,
            style: "width:100px;"
        }, "pStartDt");

        pEndDtCal = new dijit.form.DateTextBox({
            id: "pEndDt",
            name: "pEndDt",
            value: startDtVal,
            style: "width:100px;"
        }, "pEndDt");

        vvv = new dijit.form.NumberTextBox({
            name: "NumbericBox",
            value: 20,
            style: 'width:40px;height:15px;font-size:2;editable:false;',
            readOnly: true
        }, "sliderInput");
        var slider = new dijit.form.HorizontalSlider({
            name: "logBase",
            value: 20,
            minimum: -10, maximum: 50,
            //intermediateChanges: true,
            style: "width:200px;",
            onChange: function (value) {
                //alert(dijit.byId('logBase').getValue());
                dijit.byId('sliderInput').setValue(value);
            }
        }, "logBase");

        //이전 수정값을 초기화.
        //exItemStore.revert();

        //어는 버튼이 클릭되었는지 확인

        if (!exItemGrid) {
            exItemGrid = new dojox.grid.EnhancedGrid({
                id: "exItemGrid",
                structure: exItemSt,
                store: exItemStore,
                singleClickEdit: true,
                selectionMode: "none",
                canSort: function (colIndex) {
                    return false;
                }
            }, document.createElement("div"));
            exItemGrid.placeAt(dojo.byId("exItemGridPanel"));
            exItemGrid.startup();
        }

        setExItemData();

        //보정창 지정시간 평균 셋팅 
        //var prevDay = new Date();
        //prevDay.setDate(startDtCal.getValue().getDate() - 1);
        //        alert(pWin.startDtCal.getValue());
        //        dijit.byId('pStartDt').set('value', pWin.startDtCal.getValue());
        //        setSelectOption('pStartHr', "00");
        //        dijit.byId('pEndDt').set('value', pWin.startDtCal.getValue());
        //        setSelectOption('pEndHr', "24");
        hidePreloader();
    });
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div data-dojo-type="dijit.layout.BorderContainer" data-dojo-props='style:"height:560px; width:880px"'>
        <div data-dojo-type="dijit.layout.ContentPane" data-dojo-props='title:"항목 선택", region:"left", style:"width:580px"'>
            <div id="exItemGridPanel" style="width:100%;height:540px;"></div>
        </div>
        <div data-dojo-type="dijit.layout.ContentPane" data-dojo-props='title:"항목 선택", region:"center", style:"width:300px"'>
            <b>* 보정방법 설정</b>
            <hr style="border: 1px solid #000;" />
            <table style="width:100%">
            <tr>
            <td style="font-weight:bold;height:30px;vertical-align:middle;">
            * 지정시간평균 기간설정
            </td>
            <td align="right">
            </td>
            </table>
            시작일시 : <input id="pStartDt"/><select id="pStartHr" class="wsComboBox"></select><br />
            종료일시 : <input id="pEndDt"/><select id="pEndHr" class="wsComboBox"></select>
            <hr /><p>&nbsp;</p>
            <table style="width:100%">
            <tr>
            <td style="font-weight:bold;height:30px;vertical-align:middle;">
            * 지수함수곡선보간 설정
            </td>
            <td align="right">
            </td>
            </tr>
            <tr>
            <td><div id="logBase"></div></td>
            <td><input readonly id="sliderInput" size="4"/></td>
            </tr>
            </table>
            <hr /><p>&nbsp;</p>
            <p style="width:100%;text-align:right;">
            <div style="width:240px;border:1px solid red;padding:5px 5px 5px 5px;">
            - 직접수정은 산출값에 입력하세요.<br />
            - 전일평균값은 선택한 시작시간 기준 24시간전의 평균으로 계산됩니다.<br />
            - 지정시간평균은 시작일시와 종료일시를 입력하셔야 합니다.<br />
            - 선형보간은 10분전후 데이터를 기준으로 자동 계산됩니다.
            </div> 
            </p> 
        </div>

        <div data-dojo-type="dijit.layout.ContentPane" data-dojo-props='region:"bottom", style:"width:600px;text-align:right;"'>
            <button type="button" data-dojo-type="dijit.form.Button" data-dojo-props='id:"exCloseBtn",onClick:applyCancelAll'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/pencil--minus.png'> 닫 기</button>
            <button type="button" data-dojo-type="dijit.form.Button" data-dojo-props='id:"exApplyBtn",onClick:applyUpdateAll'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/pencil--plus.png'> 적 용</button>
        </div>
    </div>
</asp:Content>
