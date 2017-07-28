<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	수위자료 보정
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    html,body{
	    overflow:hidden;
	    margin:0; padding:0; 
	    width:100%; height:100%;
    }
    .dojoxGrid table { 
       margin: 0; 
    } 
    .dojoxGrid
    {
       text-align:center;
       font-size: 12px; 
    } 
    .edited 
    {
        background-color:Red;
    }
    .claro .dijitDialogUnderlay { background:transparent; } 
</style>
<script type="text/javascript">
    dojo.require("dijit.dijit"); // optimize: load dijit layer
    dojo.require("dijit.layout.BorderContainer"); // optimize: load dijit layer
    dojo.require("dijit.layout.TabContainer");
    dojo.require("dojox.layout.ExpandoPane");
    dojo.require("dijit.Dialog");

    dojo.require("dojox.grid.EnhancedGrid");
    dojo.require("dojox.data.AndOrWriteStore");
    dojo.require("dojo.data.ItemFileReadStore");
    dojo.require("dojox.data.JsonRestStore");
    dojo.require("dijit.form.TextBox");
    dojo.require("dijit.form.NumberTextBox");

    dojo.require("dojo.parser");
    dojo.require("dojox.dtl.filter.strings");
    dojo.require("dojox.grid.enhanced.plugins.Filter");
    dojo.require("dojox.grid.enhanced.plugins.exporter.CSVWriter");
    dojo.require("dojox.grid.enhanced.plugins.Printer");
    dojo.require("dojox.grid.enhanced.plugins.IndirectSelection");
    dojo.require("dojox.grid.enhanced.plugins.Selector");
    //dojo.require("dojox.grid.enhanced.plugins.Menu");
    dojo.require("dijit.Tooltip");
    dojo.require('doh.runner');
    dojo.require("dijit.form.Slider");
</script>

<%
    EmpData empdata = new EmpData();
%>
<script type="text/javascript" src="/Scripts/common/renderers.js"></script>
<script type="text/javascript">
    var closeEvent = function () {
        try {
            if (window.dialogArguments) {
                $(window.dialogArguments.document.body).unmask();
            }
        } catch (err) {
            // do nothing
        }
    };

    if (window.attachEvent) {
        window.attachEvent("onunload", closeEvent);
    } else {
        if (window.addEventListener) {
            window.addEventListener("unload", closeEvent, false);
        } else {
            window.onunload = testingPopup;
        }
    }

    var plugins = {
        indirectSelection: {
            headerSelector: true,
            name: "Selection",
            width: "20px",
            styles: "text-align: center;"
        },
        //"exporter": true,
        //"cookie": {},
        printer: true,
        "selector": { row: true, col: false, cell: false }
    };
    var currDate = new Date();
    var prevDate = new Date();
    prevDate.setDate(currDate.getDate() - 2);
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";


    //전역 변수
    var dhxLayout, dhxGrid, dhxForm, dhxFormData;
    var Date, grid, mainStore, linearGrid, linearStore, historyGrid, historyStore;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 4;
    var linearUrl = '/Verify/GetWLLinearInterpolationResult/';
    var wlParams = "";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };
    //전역함수

    var windowClose = function () {
        window.close();
    }

    var linearLayout = [{
        cells: [
            { field: "OBSDT", name: "측정일시", width: "100px", styles: 'text-align:center;vertical-align:middle;', formatter: formatDate },
            { field: "WL", name: "변경전<BR>수위", width: "100px", styles: 'text-align:center;' },
            { field: "EXVL", name: "변경후<BR>선형", width: "100px", styles: 'text-align:center;' },
            { field: "EXVL2", name: "변경후<BR>지수함수곡선", width: "100px", styles: 'text-align:center;' }
        ],
        noscroll: false,
        width: 'auto'
    }];


 
    
    var getSelectValue = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    var getSelectText = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].text;
    };

    
    var createGrid = function () {
        if (linearGrid) linearGrid.destroyRecursive();

        linearGrid = new dojox.grid.EnhancedGrid({
            id: "linearGrid",
            structure: linearLayout,
            //store: store,
            canSort: function (colIndex) {
                return false;
            }
        }, document.createElement("div"));
        linearGrid.startup();
        dojo.byId("linearGridPanel").appendChild(linearGrid.domNode);
    };



    var applyValues = function () {
        $(document.body).mask("적용중입니다...");

        var targetRecs = pWin.grid.selection.getSelected();
        var linearRecs = [];
        // lnearGrid.items 가 어느날부턴가 먹지 않더라. 그래서 내가 만듬.
        dojo.forEach(linearGrid._by_idx, function (item) {
            linearRecs.length = linearRecs.push(item);
        });


        var saveItems = [];

        var targetColumn = getSelectValue("rdsTarget");
        var thevalue = dijit.byId('NumbericBox').value;
        var mesg = "선형보간";
        if (targetColumn == "EXVL2") mesg = "지수함수곡선";

        if (targetColumn == "VALUE" && !isNaN(thevalue)) {
            for (var i = 0; i < targetRecs.length; i++) {
                targetRec = targetRecs[i];
                saveItems[saveItems.length] = { item: targetRec, field: "WL", value: parseFloat(thevalue) };
            }
        } else if (targetColumn == "EXVL" || targetColumn == "EXVL2") {

            // 연속데이터인가?
            var sRow = pWin.grid.getItemIndex(targetRecs[0]);
            var eRow = pWin.grid.getItemIndex(targetRecs[targetRecs.length - 1]);
            if (Math.abs(sRow - eRow) != targetRecs.length - 1) {
                alert(mesg+"은 연속 데이터만 가능합니다.");
                return;
            }

            for (var i = 0; i < targetRecs.length; i++) {
                targetRec = targetRecs[i];
                for (var j = 0; j < linearRecs.length; j++) {
                    var linearRec = linearRecs[j].item;
                    if (targetRec.OBSDT == linearRec.OBSDT) {
                        saveItems[saveItems.length] = { item: targetRec, field: "WL", value: linearRec[targetColumn] };
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
            pWin.grid.store.changing(tmpRec);
            tmpRec[saveItems[i].field] = saveItems[i].value;
            if (++i < saveItems.length) { ro.step(); }
            else {
                pWin.grid.update();
                $(document.body).unmask();
                pWin.loadChartPanel();
            }
        }, 100);
        ro.step();

    };
    //↑↑↑↑↑↑↑↑↑ 버튼클릭 명령들 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    //↓↓↓↓↓↓↓↓↓ 콤보박스 변경 이벤트 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    var changeRdsTarget = function () {
        var val = getSelectValue("rdsTarget");
        if (val == "VALUE") {
            dijit.byId('NumbericBox').set('disabled', false);
            dijit.byId('NumbericBox').focus();
            dijit.byId("logBase").domNode.style.display = 'none';
            dijit.byId("sliderInput").domNode.style.display = 'none';
        } else if (val == "EXVL") {
            dojo.byId('NumbericBox').setAttribute("disabled", true);
            dijit.byId("logBase").domNode.style.display = 'none';
            dijit.byId("sliderInput").domNode.style.display = 'none';
        } else { //EXVL2
            dojo.byId('NumbericBox').setAttribute("disabled", true);
            dijit.byId("logBase").domNode.style.display = 'block';
            dijit.byId("sliderInput").domNode.style.display = 'block';
        }

        if (val == "EXVL") {
            //변경후 선형
            linearLayout[0].cells[2].styles = 'text-align:center;border-width: 1px;background-color:#FFCCCC;';
            linearLayout[0].cells[3].styles = 'text-align:center;';

        } else if (val == "EXVL2") {
            //변경후 지수함수 곡선
            linearLayout[0].cells[2].styles = 'text-align:center;';
            linearLayout[0].cells[3].styles = 'text-align:center;border-width: 1px;background-color:#FFCCCC;';

        } else {
            linearLayout[0].cells[2].styles = 'text-align:center;';
            linearLayout[0].cells[3].styles = 'text-align:center;';
        }
        linearGrid.setStructure(linearLayout);

    };
    //↑↑↑↑↑↑↑↑↑ 콤보박스 변경 이벤트 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    var updateEdit = function () {
        dijit.byId('NumbericBox').set('value', 0);

        var recs = pWin.grid.selection.getSelected();
        if (recs.length < 1) {
            malert("먼저 적용할 자료를 선택하셔야 합니다.");
            return false;
        }
        if (!linearStore) {
            delete linearStore;
        }

        linearStore = new dojox.data.JsonRestStore({
            target: linearUrl,
            store: linearStore
        });
        createGrid();
        // div에서 기존 노드를 제거하면 안의 내용이 사라짐.
        // update 하면 그리드가 사라짐
        var dataTpObj = pWin.document.getElementById("dataTp");
        var rec = recs[0];
        var damCd = rec.DAMCD;
        var obsCd = rec.OBSCD;
        var dataTp = dataTpObj.options[dataTpObj.options.selectedIndex].value;
        var startDt = rec.OBSDT;
        var endDt = startDt;
        var logBase = dijit.byId('logBase').getValue().toString();

        //최소 최대 일시를 구함.
        for (var i = 0; i < recs.length; i++) {
            if (startDt > recs[i].OBSDT) {
                startDt = recs[i].OBSDT;
            } else if (endDt < recs[i].OBSDT) {
                endDt = recs[i].OBSDT;
            }
        }

        var params = {
            damcd: damCd,
            obscd: obsCd,
            startdt: startDt,
            enddt: endDt,
            dataTp: dataTp,
            logbase: logBase
        };
        //alert(damCd + "," + obsCd + "," + startDt + "," + endDt + "," + dataTp + "," + logBase);
        linearGrid.setStore(linearStore, params);
    }

    window.onbeforeunload = function () {
        pWin.unmaskBody();
    }
    //↓↓↓↓↓↓↓↓↓ 초기화 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    dojo.ready(function () {
        pWin = window.dialogArguments;

        dojo.connect(dojo.byId("rdsTarget"), "onchange", changeRdsTarget);

        vv = new dijit.form.NumberTextBox({
            name: "NumbericBox",
            value: ""
        }, "NumbericBox");
        vvv = new dijit.form.NumberTextBox({
            name: "NumbericBox",
            value: 20,
            style: 'width:50px;editable:false;',
            readOnly: true
        }, "sliderInput");
        dojo.style(vv.domNode, "width", "5em");


        var slider = new dijit.form.HorizontalSlider({
            name: "logBase",
            value: 20,
            minimum: -10, maximum: 50,
            style: "width:300px;",
            onChange: function (value) {
                updateEdit();
                dijit.byId('sliderInput').setValue(value);
            }
        }, "logBase");

        dijit.byId("logBase").domNode.style.display = 'none';
        dijit.byId("sliderInput").domNode.style.display = 'none';

        updateEdit();

        hidePreloader();
    });

</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="Div1" data-dojo-type="dijit.layout.BorderContainer" data-dojo-props='style:"width: 100%; height: 98%;"'>
    <div id="dialogTopPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"dialogTopPanel", region:"top", style:"margin:0 0 0 0;padding:0 0 0 0;"'>
        적용대상 : <select id="rdsTarget" class="wsComboBox" >
                    <option value="VALUE" selected >직접수정</option>
                    <option value="EXVL">선형보간적용</option>
                    <option value="EXVL2">지수함수곡선적용</option>
                </select>
    <input id="NumbericBox" />
    <button type="button" data-dojo-type="dijit.form.Button" data-dojo-props='id:"exCloseBtn",onClick:windowClose'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/cross-circle.png'> 취 소</button>
    <button type="button" data-dojo-type="dijit.form.Button" data-dojo-props='id:"exApplyBtn",onClick:applyValues'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/pencil--plus.png'> 적 용</button>
    <table>
    <tr>
    <td><div id="logBase"></div></td>
    <td><input readonly id="sliderInput" size="4"/></td>
    </tr>
    </table>
    </div>
    <div id="RdsGridPanelWrapper" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"RdsGridPanelWrapper", region:"center", style:"margin:0 0 0 0;padding:0 0 0 0;", style:"width:600px;height:450px;"'>
    <div id="linearGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"linearGridPanel", region:"center", style:"width:100%;height:100%"'>
    </div>
    </div>
</div>
</asp:Content>