Ext.define('Ext.ws.TreeGridDragDrop', {
    extend: 'Ext.AbstractPlugin',
    alias: 'plugin.treegriddragdrop',

    uses: [
        'Ext.ws.TreeGridDragZone',
        'Ext.ws.TreeGridDropZone'
    ],
    dragText: '{0} selected node{1}',
    allowParentInserts: false,
    allowContainerDrops: false,
    appendOnly: false,
    ddGroup: "TreeDD",
    expandDelay: 1000,
    enableDrop: true,
    enableDrag: true,
    nodeHighlightColor: 'c3daf9',
    nodeHighlightOnDrop: Ext.enableFx,
    nodeHighlightOnRepair: Ext.enableFx,

    init: function (view) {
        view.on('render', this.onViewRender, this, { single: true });
    },
    destroy: function () {
        Ext.destroy(this.dragZone, this.dropZone);
    },

    onViewRender: function (view) {
        var me = this;
        if (me.enableDrag) {
            me.dragZone = Ext.create('Ext.ws.TreeGridDragZone', {
                view: view,
                ddGroup: me.dragGroup || me.ddGroup,
                dragText: me.dragText,
                repairHighlightColor: me.nodeHighlightColor,
                repairHighlight: me.nodeHighlightOnRepair
            });
        }

        if (me.enableDrop) {
            me.dropZone = Ext.create('Ext.ws.TreeGridDropZone', {
                view: view,
                ddGroup: me.dropGroup || me.ddGroup,
                allowContainerDrops: me.allowContainerDrops,
                appendOnly: me.appendOnly,
                allowParentInserts: me.allowParentInserts,
                expandDelay: me.expandDelay,
                dropHighlightColor: me.nodeHighlightColor,
                dropHighlight: me.nodeHighlightOnDrop
            });
        }
    }
});
