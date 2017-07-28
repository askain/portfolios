/*
===================================================================
Copyright DHTMLX LTD. http://www.dhtmlx.com
This code is obfuscated and not allowed for any purposes except 
using on sites which belongs to DHTMLX LTD.

Please contact sales@dhtmlx.com to obtain necessary 
license for usage of dhtmlx components.
===================================================================
*/dhtmlXTreeObject.prototype.parserExtension={_parseExtension:function(a,b){this._idpull[b.id]._attrs=b}};dhtmlXTreeObject.prototype.getAttribute=function(a,b){this._globalIdStorageFind(a);var c=this._idpull[a]._attrs;return c?c[b]:window.undefined};dhtmlXTreeObject.prototype.setAttribute=function(a,b,c){this._globalIdStorageFind(a);var d=this._idpull[a]._attrs||{};d[b]=c;this._idpull[a]._attrs=d};