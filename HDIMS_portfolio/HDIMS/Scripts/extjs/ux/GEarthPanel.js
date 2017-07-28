Ext.define('Ext.ux.GEarthPanel', {
    extend: 'Ext.Panel',
    alias: 'widget.gearthpanel',
    requires: ['Ext.window.MessageBox'],

    initComponent: function () {
        var defConfig = {
            border: true,
            earthLayers: {
                LAYER_BORDERS: true,
                LAYER_ROADS: true,
                LAYER_BUILDINGS: true,
                LAYER_TERRAIN: true
            },

            earthOptions: {
                setStatusBarVisibility: false,
                setGridVisibility: false,
                setOverviewMapVisibility: false,
                setScaleLegendVisibility: false,
                setAtmosphereVisibility: true,
                setMouseNavigationEnabled: true
            }

            //,kmlTreePanel: null -->후에 KML패널을 이용해서 처리할 수 있도록 남겨놓음.
        };
        Ext.applyIf(this, defConfig);
        Ext.ux.GEarthPanel.superclass.initComponent.call(this);
        this.addEvents('earthLoaded');
    },

    //패널이 렌더링된 후에 Google Earth instance를 생성한다.
    afterRender: function () {
        Ext.ux.GEarthPanel.superclass.afterRender.call(this);
        google.earth.createInstance(this.body.dom, this.onEarthReady, {});
    },

    //Callback Function
    onEarthReady: function (object) {
        this.earth = object;
        this.earth.getWindow().setVisibility(true);
        this.earth.getNavigationControl().setVisibility(this.earth.VISIBILITY_SHOW);
        for (layer in this.earthLayers) {
            this.earth.getLayerRoot().enableLayerById(this.earth[layer], layers[layer]);
        }
        var la = this.earth.createLookAt('');
        la.set(37.52, 127.09,
            0, //altitude
            this.earth.ALTITUDE_RELATIVE_TO_GROUND,
            0, //heading
            0, //straight-down tilt
            1233930 //range (inverse of zoom)
        );
        this.earth.getView().setAbstractView(la);

        // Create TreePanel to show KML documents
        /*
        this.kmlTreePanel = new Ext.tree.TreePanel({
        xtype: 'treepanel',
        border: false,
        bodyStyle: 'padding-bottom: 15px',
        root: new Ext.tree.TreeNode({
        text: 'KML Documents',
        iconCls: 'folder',
        expanded: true
        }),
        rootVisible: false,
        listeners: { checkchange: { fn: function (node, checked) {
        node.attributes.kml.setVisibility(checked);
        node.bubble(function (n) {  
        if (node.getUI().isChecked()) {
        if (!node.parentNode.getUI().isChecked()) {
        node.parentNode.getUI().toggleCheck();
        }
        }
        })
        }
        }
        }
        });
        */


        this.parent.fireEvent('earthLoaded', this);
    },
    // Return Google Earth instance
    getEarth: function () {
        return this.earth;
    },

    // Set Google Earth layers
    setLayers: function (layers) {
        for (layer in layers) {
            this.earth.getLayerRoot().enableLayerById(this.earth[layer], layers[layer]);
        }
    },

    // Returns FormPanel containing Google Earth layers
    getLayersPanel: function () {

        // Define layer labels
        var layerNames = {
            LAYER_BORDERS: 'Borders and names',
            LAYER_ROADS: 'Roads',
            LAYER_BUILDINGS: 'Buildings',
            LAYER_TERRAIN: 'Terrain'
        }

        // Create checkbox for each layer
        var items = [];
        for (layer in this.earthLayers) {
            items.push({
                boxLabel: layerNames[layer],
                checked: this.earthLayers[layer],
                name: layer,
                earth: this.earth,
                handler: function (layer, visibility) {
                    this.earth.getLayerRoot().enableLayerById(this.earth[layer.name], visibility);
                } .createDelegate(this)
            });
        }

        // Create FormPanel with all layers
        var layersPanel = new Ext.FormPanel({
            title: 'Google Earth Layers',
            defaultType: 'checkbox',
            defaults: {
                hideLabel: true
            },
            items: items
        });

        return layersPanel;
    },

    // Set Google Earth options
    setOptions: function (options) {
        for (option in options) {
            this.earth.getOptions()[option](options[option]);
        }
    },

    // Returns FormPanel containing Google Earth options
    getOptionsPanel: function () {

        // Define option labels
        var optionLabels = {
            setStatusBarVisibility: 'Show status bar',
            setGridVisibility: 'Show grid',
            setOverviewMapVisibility: 'Show overview map',
            setScaleLegendVisibility: 'Show scale legend',
            setAtmosphereVisibility: 'Show atmosphere',
            setMouseNavigationEnabled: 'Enable mouse navigation'
        }

        // Create checkbox for each option
        var items = [];
        for (option in this.earthOptions) {
            items.push({
                boxLabel: optionLabels[option],
                checked: this.earthOptions[option],
                name: option,
                handler: function (option, visibility) {
                    this.earth.getOptions()[option.name](visibility);
                } .createDelegate(this)
            });
        }

        // Create FormPanel with all options
        var optionsPanel = new Ext.FormPanel({
            title: 'Options',
            defaultType: 'checkbox',
            defaults: {
                hideLabel: true
            },
            items: items
        });

        return optionsPanel;
    },

    // Returns FormPanel for finding locations
    getLocationPanel: function () {
        var locationPanel = new Ext.FormPanel({
            title: 'Find Location',
            labelAlign: 'top',
            items: new Ext.form.TriggerField({
                fieldLabel: 'Location',
                triggerClass: 'x-form-search-trigger',
                anchor: '100%',
                name: 'location',
                scope: this,
                onTriggerClick: function () {
                    this.scope.findLocation(this.getValue());
                },
                listeners: { specialkey: { fn: function (f, e) {
                    if (e.getKey() == e.ENTER) {
                        this.onTriggerClick();
                    }
                }
                }
                }
            })
        });
        return locationPanel;
    },

    // Fly to location (geocoding) - used by above function
    // Based on http://earth-api-samples.googlecode.com/svn/trunk/examples/geocoder.html
    findLocation: function (geocodeLocation) {
        var geocoder = new google.maps.ClientGeocoder();
        geocoder.getLatLng(geocodeLocation, function (point) {
            if (point) {
                var lookAt = this.earth.createLookAt('');
                lookAt.set(point.y, point.x, 10, this.earth.ALTITUDE_RELATIVE_TO_GROUND, 0, 60, 20000);
                this.earth.getView().setAbstractView(lookAt);
            }
        } .createDelegate(this));
    },

    // Returns FormPanel for KML documents
    getKmlPanel: function () {

        var kmlUrlField = new Ext.form.TriggerField({
            fieldLabel: 'Add KML/KMZ',
            triggerClass: 'x-form-search-trigger',
            anchor: '100%',
            name: 'url',
            value: 'http://',
            selectOnFocus: true,
            scope: this,
            onTriggerClick: function () {
                google.earth.fetchKml(this.scope.earth, this.getValue(), this.scope.addKml.createDelegate(this.scope));
                this.reset();
            }, 
            listeners: { specialkey: { fn: function (f, e) {
                if (e.getKey() == e.ENTER) {
                    this.onTriggerClick();
                }
            }
            }
            }
        });

        var kmlPanel = new Ext.FormPanel({
            title: 'KML Documents',
            labelAlign: 'top',
            items: [this.kmlTreePanel, kmlUrlField]
        });

        return kmlPanel;
    },

    // Load and display KML file
    fetchKml: function (kmlUrl) {
        google.earth.fetchKml(this.earth, kmlUrl, this.addKml.createDelegate(this));
    },

    // Add KML object (called by above function)
    addKml: function (kmlObject) {
        if (kmlObject) {
            this.earth.getFeatures().appendChild(kmlObject);
            //this.kmlTreePanel.getRootNode().appendChild(this.treeNodeFromKml(kmlObject));
        } else {
            alert('Bad KML');
        }
    },

    // Create KML tree (called by above function)
    treeNodeFromKml: function (kmlObject) {
        var result = this.createKmlTreeNode(kmlObject);

        if (kmlObject.getFeatures().hasChildNodes()) {
            var subNodes = kmlObject.getFeatures().getChildNodes();
            for (var i = 0; i < subNodes.getLength(); i++) {
                var subNode = subNodes.item(i);
                switch (subNode.getType()) {
                    case 'KmlFolder':
                        var node = this.treeNodeFromKml(subNode); // Recursion
                        break;
                    default:
                        var node = this.createKmlTreeNode(subNode);
                        break;
                }
                result.appendChild(node);
            }
        }
        return result;
    },

    // Create KML tree node (called by above function)
    createKmlTreeNode: function (kmlEl) {
        var node = new Ext.tree.TreeNode({
            text: kmlEl.getName(),
            checked: (kmlEl.getType() != 'KmlPlacemark' ? (kmlEl.getVisibility() ? true : false) : null),
            expanded: (kmlEl.getOpen() ? true : false),
            iconCls: kmlEl.getType(),
            kml: kmlEl
        });
        return node;
    }

});

//Ext.reg('gearthpanel', Ext.ux.GEarthPanel);
