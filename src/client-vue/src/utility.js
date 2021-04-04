import Vue from 'vue'

const utility = new Vue({
    methods: {
        toObjectMap(array, prop = 'Id', arrayMode = false, arrayProp = false) {
            let objectMap = {};

            array.forEach(function(item) {
                if(arrayMode) {
                    if(arrayProp) {
                        item[prop].forEach(function(subItem) {
                            objectMap[subItem] = (objectMap[subItem] || []).concat([item]);
                        });
                    } else {
                        objectMap[item[prop]] = (objectMap[item[prop]] || []).concat([item]);
                    }
                } else {
                    objectMap[item[prop]] = item;
                }
            });
            
            return objectMap;
        },
        isEmptyObject(obj) {
            return Object.keys(obj).length === 0 && obj.constructor === Object;
        },
    }
})

export default utility;