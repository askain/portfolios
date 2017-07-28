
Ext.define('Ext.ux.ColorField', {
    extend: 'Ext.form.field.Picker',
    alias: 'widget.colorfield',
    requires: ['Ext.picker.Color'],

    matchFieldWidth: false,

    lengthText: "Color hex values must be either 3 or 6 characters.",
    blankText: "Must have a hexidecimal value in the format ABCDEF.",

    regex: /^[0-9a-f]{3,6}$/i,

    editable: false,

    validateValue: function (value) {
        //alert('validateValue :' + value);
        return value;
    },

    initComponent: function () {
        var me = this,
            isString = Ext.isString;

        me.callParent();
        //alert('initComponent :' + me);
    },

    initValue: function () {
        var me = this,
            value = me.value;

        if (Ext.isString(value)) {
            me.value = me.value;
        }

        me.callParent();
        //alert('initValue :' + me);
    },

    createPicker: function () {

        var me = this;
        //alert('createPicker');
        return Ext.create('Ext.picker.Color', {
            ownerCt: me.ownerCt,
            renderTo: Ext.getBody(),
            floating: true,
            shadow: true,
            value: me.value,
            border: 0,
            style: 'background-color:green;',
            listeners: {
                scope: me,
                select: me.onSelect
            },
            keyNavConfig: {
                esc: function () {
                    me.collapse();
                }
            }
        });
    },

    onSelect: function (m, d) {
        //alert('onSelect: ' + d);
        var me = this;
        me.setValue(d);
        me.fireEvent('select', me, d);
        me.collapse();
    },

    onExpand: function () {
        var me = this,
            value = me.getValue();
        //alert('onExpand :' + value);
    },

    /**
    * @private
    * Focuses the field when collapsing the Color picker.
    */
    onCollapse: function () {
        this.focus(false, 0);
        //alert('onCollapse');
    },

    // private
    beforeBlur: function () {
        var me = this,
            v = me.validateValue(me.getRawValue()),
            focusTask = me.focusTask;
        if (focusTask) {
            focusTask.cancel();
        }

        if (v) {
            me.setValue(v);
        }
        //alert('beforeBlur :' + v);
    }
});