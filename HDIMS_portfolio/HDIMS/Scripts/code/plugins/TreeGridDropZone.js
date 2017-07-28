Ext.define('Ext.ws.TreeGridDropZone', {
    extend: 'Ext.dd.DropZone',

    indicatorHtml: '<div class="x-grid-drop-indicator-left"></div><div class="x-grid-drop-indicator-right"></div>',

    allowParentInserts: false,
    allowContainerDrops: false,

    appendOnly: false,
    expandDelay: 500,

    indicatorCls: 'x-tree-ddindicator',

    constructor: function (config) {
        var me = this;
        Ext.apply(me, config);
        if (!me.ddGroup) {
            me.ddGroup = 'view-dd-zone-' + me.view.id;
        }
        me.callParent([me.view.el]);
    },

    fireViewEvent: function () {
        var me = this,
            result;

        me.lock();
        result = me.view.fireEvent.apply(me.view, arguments);
        me.unlock();
        return result;
    },

    getTargetFromEvent: function (e) {
        var node = e.getTarget(this.view.getItemSelector()),
            mouseY, nodeList, testNode, i, len, box;

        if (!node) {
            mouseY = e.getPageY();
            for (i = 0, nodeList = this.view.getNodes(), len = nodeList.length; i < len; i++) {
                testNode = nodeList[i];
                box = Ext.fly(testNode).getBox();
                if (mouseY <= box.bottom) {
                    return testNode;
                }
            }
        }
        return node;
    },

    // private
    expandNode: function (node) {
        var view = this.view;
        if (!node.isLeaf() && !node.isExpanded()) {
            view.expand(node);
            this.expandProcId = false;
        }
    },

    // private
    queueExpand: function (node) {
        this.expandProcId = Ext.Function.defer(this.expandNode, this.expandDelay, this, [node]);
    },

    // private
    cancelExpand: function () {
        if (this.expandProcId) {
            clearTimeout(this.expandProcId);
            this.expandProcId = false;
        }
    },

    getIndicator: function () {
        var me = this;

        if (!me.indicator) {
            me.indicator = Ext.createWidget('component', {
                html: me.indicatorHtml,
                cls: me.indicatorCls,
                ownerCt: me.view,
                floating: true,
                shadow: false
            });
        }
        return me.indicator;
    },

    getPosition: function (e, node) {
        var view = this.view,
            record = view.getRecord(node),
            y = e.getPageY(),
            noAppend = record.isLeaf(),
            noBelow = false,
            region = Ext.fly(node).getRegion(),
            fragment;

        // If we are dragging on top of the root node of the tree, we always want to append.
        if (record.isRoot()) {
            return 'append';
        }

        // Return 'append' if the node we are dragging on top of is not a leaf else return false.
        if (this.appendOnly) {
            return noAppend ? false : 'append';
        }

        if (!this.allowParentInsert) {
            noBelow = record.hasChildNodes() && record.isExpanded();
        }

        fragment = (region.bottom - region.top) / (noAppend ? 2 : 3);
        if (y >= region.top && y < (region.top + fragment)) {
            return 'before';
        }
        else if (!noBelow && (noAppend || (y >= (region.bottom - fragment) && y <= region.bottom))) {
            return 'after';
        }
        else {
            return 'append';
        }
    },

    containsRecordAtOffset: function (records, record, offset) {
        if (!record) {
            return false;
        }
        var view = this.view,
            recordIndex = view.indexOf(record),
            nodeBefore = view.getNode(recordIndex + offset),
            recordBefore = nodeBefore ? view.getRecord(nodeBefore) : null;

        return recordBefore && Ext.Array.contains(records, recordBefore);
    },

    positionIndicator: function (node, data, e) {
        var me = this,
            view = me.view,
            pos = me.getPosition(e, node),
            overRecord = view.getRecord(node),
            draggingRecords = data.records,
            indicator, indicatorY;

        if (!Ext.Array.contains(draggingRecords, overRecord) && (
            pos == 'before' && !me.containsRecordAtOffset(draggingRecords, overRecord, -1) ||
            pos == 'after' && !me.containsRecordAtOffset(draggingRecords, overRecord, 1)
        )) {
            me.valid = true;

            if (me.overRecord != overRecord || me.currentPosition != pos) {

                indicatorY = Ext.fly(node).getY() - view.el.getY() - 1;
                if (pos == 'after') {
                    indicatorY += Ext.fly(node).getHeight();
                }
                me.getIndicator().setWidth(Ext.fly(view.el).getWidth()).showAt(0, indicatorY);

                // Cache the overRecord and the 'before' or 'after' indicator.
                me.overRecord = overRecord;
                me.currentPosition = pos;
            }
        } else {
            me.invalidateDrop();
        }
    },

    isValidDropPoint: function (node, position, dragZone, e, data) {
        if (!node || !data.item) {
            return false;
        }

        var view = this.view,
            targetNode = view.getRecord(node),
            draggedRecords = data.records,
            dataLength = draggedRecords.length,
            ln = draggedRecords.length,
            i, record;

        // No drop position, or dragged records: invalid drop point
        if (!(targetNode && position && dataLength)) {
            return false;
        }

        // If the targetNode is within the folder we are dragging
        for (i = 0; i < ln; i++) {
            record = draggedRecords[i];
            if (record.isNode && record.contains(targetNode)) {
                return false;
            }
        }

        // Respect the allowDrop field on Tree nodes
        if (position === 'append' && targetNode.get('allowDrop') === false) {
            return false;
        }
        else if (position != 'append' && targetNode.parentNode.get('allowDrop') === false) {
            return false;
        }

        // If the target record is in the dragged dataset, then invalid drop
        if (Ext.Array.contains(draggedRecords, targetNode)) {
            return false;
        }

        // @TODO: fire some event to notify that there is a valid drop possible for the node you're dragging
        // Yes: this.fireViewEvent(blah....) fires an event through the owning View.
        return true;
    },

    invalidateDrop: function () {
        if (this.valid) {
            this.valid = false;
            this.getIndicator().hide();
        }
    },

    onNodeOver: function (node, dragZone, e, data) {
        var position = this.getPosition(e, node),
            returnCls = this.dropNotAllowed,
            view = this.view,
            targetNode = view.getRecord(node),
            indicator = this.getIndicator(),
            indicatorX = 0,
            indicatorY = 0;
        alert("onNodeOver : " + position);
        // auto node expand check
        this.cancelExpand();
        if (position == 'append' && !this.expandProcId && !Ext.Array.contains(data.records, targetNode) && !targetNode.isLeaf() && !targetNode.isExpanded()) {
            this.queueExpand(targetNode);
        }


        if (this.isValidDropPoint(node, position, dragZone, e, data)) {
            this.valid = true;
            this.currentPosition = position;
            this.overRecord = targetNode;

            indicator.setWidth(Ext.fly(node).getWidth());
            indicatorY = Ext.fly(node).getY() - Ext.fly(view.el).getY() - 1;

            /*
            * In the code below we show the proxy again. The reason for doing this is showing the indicator will
            * call toFront, causing it to get a new z-index which can sometimes push the proxy behind it. We always 
            * want the proxy to be above, so calling show on the proxy will call toFront and bring it forward.
            */
            if (position == 'before') {
                returnCls = targetNode.isFirst() ? Ext.baseCSSPrefix + 'tree-drop-ok-above' : Ext.baseCSSPrefix + 'tree-drop-ok-between';
                indicator.showAt(0, indicatorY);
                dragZone.proxy.show();
            } else if (position == 'after') {
                returnCls = targetNode.isLast() ? Ext.baseCSSPrefix + 'tree-drop-ok-below' : Ext.baseCSSPrefix + 'tree-drop-ok-between';
                indicatorY += Ext.fly(node).getHeight();
                indicator.showAt(0, indicatorY);
                dragZone.proxy.show();
            } else {
                returnCls = Ext.baseCSSPrefix + 'tree-drop-ok-append';
                // @TODO: set a class on the parent folder node to be able to style it
                indicator.hide();
            }
        } else {
            this.valid = false;
        }

        this.currentCls = returnCls;
        return returnCls;
    },

    onContainerOver: function (dd, e, data) {
        return e.getTarget('.' + this.indicatorCls) ? this.currentCls : this.dropNotAllowed;
    },

    onContainerDrop: function (dd, e, data) {
        return this.onNodeDrop(dd, null, e, data);
    },

    notifyOut: function (node, dragZone, e, data) {
        var me = this;
        alert('test');
        me.callParent(arguments);
        delete me.overRecord;
        delete me.currentPosition;
        if (me.indicator) {
            me.indicator.hide();
        }
        this.cancelExpand();
    },

    onNodeDrop: function (node, dragZone, e, data) {
        alert("onNodeDrop");
        var me = this,
            dropped = false,

        // Create a closure to perform the operation which the event handler may use.
        // Users may now return <code>false</code> from the beforedrop handler, and perform any kind
        // of asynchronous processing such as an Ext.Msg.confirm, or an Ajax request,
        // and complete the drop gesture at some point in the future by calling this function.
            processDrop = function () {
                me.invalidateDrop();
                me.handleNodeDrop(data, me.overRecord, me.currentPosition);
                dropped = true;
                me.fireViewEvent('drop', node, data, me.overRecord, me.currentPosition);
            },
            performOperation = false;

        if (me.valid) {
            performOperation = me.fireViewEvent('beforedrop', node, data, me.overRecord, me.currentPosition, processDrop);
            if (performOperation !== false) {
                // If the processDrop function was called in the event handler, do not do it again.
                if (!dropped) {
                    processDrop();
                }
            }
        }
        return performOperation;
    },
    handleNodeDrop: function (data, targetNode, position) {
        var me = this,
            view = me.view,
            parentNode = targetNode.parentNode,
            store = view.getStore(),
            recordDomNodes = [],
            records, i, len,
            insertionMethod, argList,
            needTargetExpand,
            transferData,
            processDrop;
        alert(data.copy);
        if (data.copy) {
            records = data.records;
            data.records = [];
            for (i = 0, len = records.length; i < len; i++) {
                data.records.push(Ext.apply({}, records[i].data));
            }
        }

        // Cancel any pending expand operation
        me.cancelExpand();

        // Grab a reference to the correct node insertion method.
        // Create an arg list array intended for the apply method of the
        // chosen node insertion method.
        // Ensure the target object for the method is referenced by 'targetNode'
        if (position == 'before') {
            insertionMethod = parentNode.insertBefore;
            argList = [null, targetNode];
            targetNode = parentNode;
        }
        else if (position == 'after') {
            if (targetNode.nextSibling) {
                insertionMethod = parentNode.insertBefore;
                argList = [null, targetNode.nextSibling];
            }
            else {
                insertionMethod = parentNode.appendChild;
                argList = [null];
            }
            targetNode = parentNode;
        }
        else {
            if (!targetNode.isExpanded()) {
                needTargetExpand = true;
            }
            insertionMethod = targetNode.appendChild;
            argList = [null];
        }

        // A function to transfer the data into the destination tree
        transferData = function () {
            var node;
            for (i = 0, len = data.records.length; i < len; i++) {
                argList[0] = data.records[i];
                node = insertionMethod.apply(targetNode, argList);

                if (Ext.enableFx && me.dropHighlight) {
                    recordDomNodes.push(view.getNode(node));
                }
            }

            // Kick off highlights after everything's been inserted, so they are
            // more in sync without insertion/render overhead.
            if (Ext.enableFx && me.dropHighlight) {
                //FIXME: the check for n.firstChild is not a great solution here. Ideally the line should simply read 
                //Ext.fly(n.firstChild) but this yields errors in IE6 and 7. See ticket EXTJSIV-1705 for more details
                Ext.Array.forEach(recordDomNodes, function (n) {
                    if (n) {
                        Ext.fly(n.firstChild ? n.firstChild : n).highlight(me.dropHighlightColor);
                    }
                });
            }
        };

        // If dropping right on an unexpanded node, transfer the data after it is expanded.
        if (needTargetExpand) {
            targetNode.expand(false, transferData);
        }
        // Otherwise, call the data transfer function immediately
        else {
            transferData();
        }
    }
});
