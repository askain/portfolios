Ext.define('Verify.RainFallGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.rainfallgrid',

    requires: [
        'Ext.grid.plugin.CellEditing',
        'Ext.form.field.Text',
        'Ext.toolbar.TextItem'
    ],

    initComponent: function () {

        this.editing = Ext.create('Ext.grid.plugin.CellEditing');

        Ext.apply(this, {
            iconCls: 'icon-grid',
            frame: true,
            plugins: [this.editing]
        });
        this.callParent();
    },
    onSync: function () {
        this.store.sync();
    }
});


/* 모델 정의 */
//  모델은 common/rainfallModule으로 이동했습니다.