/*
===================================================================
Copyright DHTMLX LTD. http://www.dhtmlx.com
This code is obfuscated and not allowed for any purposes except 
using on sites which belongs to DHTMLX LTD.

Please contact sales@dhtmlx.com to obtain necessary 
license for usage of dhtmlx components.
===================================================================
*/function eXcell_grid(b){if(b){this.cell=b;this.grid=this.cell.parentNode.grid;if(!this.grid._sub_grids)return;this._sub=this.grid._sub_grids[b._cellIndex];if(!this._sub)return;this._sindex=this._sub[1];this._sub=this._sub[0]}this.getValue=function(){return this.cell.val};this.setValue=function(a){this.cell.val=a;this._sub.getRowById(a)&&(a=(a=this._sub.cells(a,this._sindex))?a.getValue():"");this.setCValue(a||"&nbsp;",a)};this.edit=function(){this.val=this.cell.val;this._sub.entBox.style.display="block";var a=this.grid.getPosition(this.cell);this._sub.entBox.style.top=a[1]+"px";this._sub.entBox.style.left=a[0]+"px";this._sub.entBox.style.position="absolute";this._sub.setSizes();var b=this.grid.editStop;this.grid.editStop=function(){};this._sub.getRowById(this.cell.val)&&this._sub.setSelectedRow(this.cell.val);this._sub.setActive(!0);this.grid.editStop=b};this.detach=function(){var a=this.cell.val;this._sub.entBox.style.display="none";if(this._sub.getSelectedId()===null)return!1;this.setValue(this._sub.getSelectedId());this.grid.setActive(!0);return this.cell.val!=a}}eXcell_grid.prototype=new eXcell;dhtmlXGridObject.prototype.setSubGrid=function(b,a,c){if(!this._sub_grids)this._sub_grids=[];this._sub_grids[a]=[b,c];b.entBox.style.display="none";var d=this;b.attachEvent("onRowSelect",function(){d.editStop();return!0});b._chRRS= !1};