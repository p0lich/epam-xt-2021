class Service {
    constructor() {
        this.data = new Map();
        this.lastId = 1;
    }

    add(obj) {
        // key is ID
        this.data.set(this.lastId.toString(), obj);
        this.lastId++;
    }

    getById(id) {
        if(!this.data.has(id)) {
            return null;
        }

        return this.data.get(id);
    }

    getAll() {
        return this.data;
    }

    deleteById(id) {
        let removedObj = this.data.get(id);
        if(this.data.delete(id)) {
            return removedObj;
        }

        return null;
    }

    updateById(id, obj) {
        if(!this.data.has(id)) {
            return null;
        }

        let tempValue = Object.assign(this.data.get(id), obj);
        this.data.set(id, tempValue);

    }

    replaceById(id, obj) {
        if(!this.data.has(id)) {
            return null;
        }

        this.data.set(id, obj);
    }
}