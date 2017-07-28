Ext.define('Edit.Grid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.editGrid',

//    requires: [
//        'Ext.grid.plugin.CellEditing',
//        'Ext.form.field.Text',
//        'Ext.toolbar.TextItem'
//    ],

    initComponent: function () {

        this.editing = Ext.create('Ext.grid.plugin.CellEditing');

        Ext.apply(this, {
            plugins: [this.editing]
        });
        this.callParent();
    }
});