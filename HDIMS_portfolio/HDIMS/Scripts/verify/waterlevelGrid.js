Ext.define('Verify.WaterLevelGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.waterlevelgrid',

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
//  ----------------------------- 모델 정의는 script/common/waterlevelModule 로 이동했습니다.  ------------------------------
