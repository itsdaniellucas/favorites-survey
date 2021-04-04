

const utility = {
    toObjectMap: (array, prop = 'Id', arrayMode = false, arrayProp = false) => {
        let objectMap = {};

        if(array && array.length > 0) {
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
        }
        
        return objectMap;
    },
}

export default utility