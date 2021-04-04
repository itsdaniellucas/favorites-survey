
let internalStore = {};

function register(store) {
    internalStore = store;
}

function fetch() {
    return internalStore;
}

export { register, fetch }