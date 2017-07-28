/**
 * @author yeshtml5@gmail.com
 * @version 1.0
 * @since �ۼ���(2013.08.21)
 * @class core.js
 * @description
 * namespace �� Class ���̵带 ����ִ� Core Class
 */
(function(context) {"use strict";
	var FRAMEWORK_CODE = "fn";
	var core = window[FRAMEWORK_CODE] = window[FRAMEWORK_CODE] || {};
	/**
	 * @function
	 * @name namespace
	 * @param { string } name  ���ӽ����̽�
	 * @param { object } obj ������ Class
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
	 * @param { string } name  ���ӽ����̽�
	 * @param { object } opt �߰� �ɼǰ���
	 */
	core.define = function(name, opt) {
		var Class = function(obj) {
			if (this.init) {
				this.init.apply(this, arguments);		// Class �����Ǹ� init();  �⺻ȣ��
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