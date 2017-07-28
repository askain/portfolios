<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Sub3.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HDIMS.Utils.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	우량자료 보정
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
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

    dojo.require('doh.runner');
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
        printer: true,
        //"cookie": {},
        selector: { row: true, col: false, cell: false }
    };
    var currDate = new Date();
    var prevDate = new Date();
    prevDate.setDate(currDate.getDate() - 2);
    var loginEmpNo = "<%=empdata.GetEmpData(0) %>";
    var loginEmpNm = "<%=empdata.GetEmpData(1) %>";
    //전역 변수
    var dhxLayout, dhxGrid, dhxForm, dhxFormData;
    var Date, grid, mainStore, rdsGrid, rdsStore, historyGrid, historyStore;
    var originStoreItems = [], preFrom, preTo, searchFlag = 0;
    var legendNum = 3;
    var rfParams = "";
    var datePattern = { datePattern: "yyyy-MM-dd", selector: "date" };
    var datePattern2 = { datePattern: "yyyyMMdd", selector: "date" };
    var datePattern3 = { datePattern: "yyyyMMddHHmm", selector: "date" };
    var pWin;

    //전역함수

    var windowClose = function () {
        window.close();
    }

    
    var rdsTargetOptions = [
        { text: '직접수정', value: 'VALUE', selected: true },
        { text: '평균적용', value: 'DXVL' },
        { text: 'RDS적용', value: 'EXVL' },
        { text: '선형보간', value: 'LINE' }
    ];

    var getSelectValue = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].value;
    };

    var getSelectText = function (id) {
        var sel = dojo.byId(id);
        return sel.options[sel.options.selectedIndex].text;
    };

    //↓↓↓↓↓↓↓↓↓ 버튼클릭 명령들 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

    var createGrid = function (header, store) {
        if (rdsGrid) rdsGrid.destroyRecursive();

        rdsGrid = new dojox.grid.EnhancedGrid({
            id: "rdsGrid",
            structure: header,
            noDataMessage: "인근관측소가 없거나 우량데이터가 존재하지 않습니다.",
            errorMessage: "인근관측소가 없거나 우량데이터가 존재하지 않습니다.",
            store: store,
            canSort: function (colIndex) {
                return false;
            }
        });
        rdsGrid.placeAt(dojo.byId("RdsGridPanel"));
        rdsGrid.startup();
    };
    var jsonData;

    var updateEdit = function () {

        var recs = pWin.grid.selection.getSelected();
        if (recs.length < 1) {
            malert("먼저 적용할 자료를 선택하셔야 합니다.");
            return false;
        }

        var rec = recs[0];
        var damCd = rec.DAMCD;

        var obsCd = rec.OBSCD;
        var dataTpObj = pWin.document.getElementById("dataTp");
        var dataTp = dataTpObj.options[dataTpObj.options.selectedIndex].value;
        var datelist = recs[0].OBSDT.toString();
        for (i = 1; i < recs.length; i++) {
            datelist += ":" + recs[i].OBSDT;
        }

        var xhrArgs = {
            url: "/Verify/GetRfRds",
            content: {
                damcd: damCd,
                obscd: obsCd,
                datelist: datelist,
                datatp: dataTp
            },
            load: function (data) {
                jsonData = dojo.fromJson(data);
                var tempRdsTargetOption = [];
                if (jsonData.Data == null || jsonData.Data.length == 0) {
                    alert("데이터 참조중 에러가 발생하였습니다.");
                    $(document.body).unmask();
                    return;
                }

                if (jsonData.Data[0].EXVL == undefined) {
                    //
                    tempRdsTargetOption.push(rdsTargetOptions[0]);
                    tempRdsTargetOption.push(rdsTargetOptions[3]);
                    setOptions("rdsTarget", tempRdsTargetOption);

                } else {
                    tempRdsTargetOption.push(rdsTargetOptions[0]);
                    tempRdsTargetOption.push(rdsTargetOptions[1]);
                    tempRdsTargetOption.push(rdsTargetOptions[2]);
                    tempRdsTargetOption.push(rdsTargetOptions[3]);
                    setOptions("rdsTarget", tempRdsTargetOption);
                }

                setSelectOption("rdsTarget", "VALUE");
                dijit.byId('NumbericBox').setAttribute("disabled", false);
                dijit.byId('NumbericBox').setValue(0);


                if (rdsStore) {
                    delete rdsStore;
                }

                rdsStore = new dojo.data.ItemFileReadStore({
                    data: {
                        identifier: "OBSDT",
                        items: jsonData.Data
                    }
                });

                createGrid(jsonData.Head, rdsStore);

                hidePreloader();

            },
            error: function (error) {
                alert("에러가 발생하였습니다. : " + error);
            }
        };
        var deferred = dojo.xhrPost(xhrArgs);
    };


    var applyValue = function () {

        var targetRecs = pWin.grid.selection.getSelected();
        var rdsRecs = rdsGrid.store._arrayOfAllItems;
        var targetColumn = getSelectValue("rdsTarget");

        if (targetColumn != "VALUE" && rdsRecs == null) {
            alert("RDS데이터가 존재하지 않습니다.")
            return undefined;
        }

        var saveItems = [];

        $(document.body).mask("데이터를 적용중입니다.");
        var thevalue = dijit.byId('NumbericBox').value
        if (targetColumn == "VALUE" && !isNaN(thevalue)) {
            for (var i = 0; i < targetRecs.length; i++) {
                saveItems[saveItems.length] = { item: targetRecs[i], field: 'ACURF', value: parseFloat(thevalue) };
            }
        } else if (targetColumn == "LINE") {
            for (var i = 0; i < targetRecs.length; i++) {
                targetRec = targetRecs[i];
                for (var j = 0; j < rdsRecs.length; j++) {
                    rdsRec = rdsRecs[j];
                    if (targetRec.OBSDT == rdsRec.OBSDT) {
                        saveItems[saveItems.length] = { item: targetRec, field: 'ACURF', value: parseFloat(rdsRec[targetColumn]) };
                    }
                }
            }
        } else if (targetColumn == "DXVL" || targetColumn == "EXVL") {
            //시작 계측우량을 계산
            var initAcurf = parseFloat(targetRecs[targetRecs.length - 1]["ACURF"]) - parseFloat(targetRecs[targetRecs.length - 1]["RF"]);
            var currentAcurf = initAcurf;
            for (var j = rdsRecs.length - 1; j >= 0; j--) {
                var targetRec = targetRecs[j];
                var rdsRec = rdsRecs[j];
                var currentRf = parseFloat(rdsRec[targetColumn]);
                currentAcurf = currentAcurf + currentRf;

                saveItems[saveItems.length] = { item: targetRec, field: 'ACURF', value: currentAcurf };
                saveItems[saveItems.length] = { item: targetRec, field: 'RF', value: currentRf };
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
            else { pWin.grid.update(); $(document.body).unmask(); pWin.loadChartPanel(); }
        }, 100);
        ro.step();

        ///loadChartPanel();
    };
    //↑↑↑↑↑↑↑↑↑ 버튼클릭 명령들 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    //↓↓↓↓↓↓↓↓↓ 콤보박스 변경 이벤트 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
   
    var changeRdsTarget = function () {
        var val = getSelectValue("rdsTarget");
        if (val == "VALUE") {
            dijit.byId('NumbericBox').set('disabled', false);
            dijit.byId('NumbericBox').focus();
        } else {
            dijit.byId('NumbericBox').setAttribute("disabled", true);
        }

        var structureLayout = jsonData.Head;
        if (structureLayout == undefined) return undefined;

        var cnt = structureLayout.length;
        if (val == "DXVL") {
            //평균
            structureLayout[cnt - 3].styles = 'text-align:center;border-width: 1px;background-color:#FFCCCC;';
            structureLayout[cnt - 2].styles = 'text-align:center;';
            structureLayout[cnt - 1].styles = 'text-align:center;';

        } else if (val == "EXVL") {
            //RDS
            structureLayout[cnt - 3].styles = 'text-align:center;';
            structureLayout[cnt - 2].styles = 'text-align:center;border-width: 1px;background-color:#FFCCCC;';
            structureLayout[cnt - 1].styles = 'text-align:center;';

        } else if (val == "LINE") {
            structureLayout[cnt - 3].styles = 'text-align:center;';
            structureLayout[cnt - 2].styles = 'text-align:center;';
            structureLayout[cnt - 1].styles = 'text-align:center;border-width: 1px;background-color:#FFCCCC;';
        } else {
            structureLayout[cnt - 3].styles = 'text-align:center;';
            structureLayout[cnt - 2].styles = 'text-align:center;';
            structureLayout[cnt - 1].styles = 'text-align:center;';
        }
        rdsGrid.setStructure(structureLayout);
    };
    //↑↑↑↑↑↑↑↑↑ 콤보박스 변경 이벤트 끝   ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    //↓↓↓↓↓↓↓↓↓ 그리드 이벤트 시작 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

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
        dojo.style(vv.domNode, "width", "5em");


        updateEdit();

        
    });

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div id="Div1" data-dojo-type="dijit.layout.BorderContainer" data-dojo-props='style:"height:450px;width:600px;"'>
            <div id="dialogTopPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"dialogTopPanel", region:"top", style:"margin:0 0 0 0;padding:0 0 0 0;"'>
                적용대상 : <select id="rdsTarget" class="wsComboBox" ></select>
            <input id="NumbericBox" />
            <button data-dojo-type="dijit.form.Button" data-dojo-props='id:"exCloseBtn",onClick:windowClose'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/cross-circle.png'> 닫 기</button>
            <button type="button" data-dojo-type="dijit.form.Button" data-dojo-props='id:"exApplyBtn",onClick:applyValue'><img src='<%=Page.ResolveUrl("~/Images") %>/icons/pencil--plus.png'> 적 용</button>
            </div>
            <div id="RdsGridPanelWrapper" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"RdsGridPanelWrapper", region:"center", style:"margin:0 0 0 0;padding:0 0 0 0;", style:"width:600px;height:450px;"'>
            <div id="RdsGridPanel" data-dojo-type="dijit.layout.ContentPane" data-dojo-props='id:"RdsGridPanel", region:"center", style:"width:100%;height:100%"' >
            </div>
            </div>
        </div>
    
</asp:Content>


