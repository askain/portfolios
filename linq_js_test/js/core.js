/**
 * @author yeshtml5@gmail.com
 * @version 1.0
 * @since 작성일(2013.08.21)
 * @class core.js
 * @description
 * namespace 및 Class 가이드를 잡아주는 Core Class
 */
(function(context) {"use strict";
	var FRAMEWORK_CODE = "fn";
	var core = window[FRAMEWORK_CODE] = window[FRAMEWORK_CODE] || {};
	/**
	 * @function
	 * @name namespace
	 * @param { string } name  네임스페이스
	 * @param { object } obj 생성된 Class
	 */
	core.namespace = function(name, obj) {
		obj || ( obj = {});
		if ( typeof name !== 'string') {
			obj && ( name = obj);
			return name;
		};
		var root = window, part, parts = name.split('.'), total = parts.length - 1, className = parts[total];
		for (var i = 0; i < total; i++) {
			part = parts[i];
			root = root[part] || (root[part] = {});
		};
		obj && (root[className] = obj);
		return root[className];
	};
	/**
	 * @function
	 * @name define
	 * @param { string } name  네임스페이스
	 * @param { object } opt 추가 옵션값들
	 */
	core.define = function(name, opt) {
		var Class = function(obj) {
			if (this.init) {
				this.init.apply(this, arguments);		// Class 생성되면 init();  기본호출
			}
		};
		(Class.getAttribute = function(obj) {
			obj = obj || {};
			for (var prop in obj) {
				Class.prototype[prop] = obj[prop];
			}
			return Class;
		})(opt);
	//	Class.superclass = Parent.prototype;
	//	Class.prototype = new Parent();
		Class.prototype.constructor = Class;
		return core.namespace(name, Class);
	};
})(window);