import axios from 'axios'
import { fetch } from '../storeExternal'
import appConfig from '../appConfig'

import { saving, success, error } from '../store/alertSlice'
import { setIsFetching } from '../store/globalSlice'


const baseUrl = appConfig.baseUrl;
const isCORS = appConfig.isCORS;

function handleSimple() {
    return (res) => {
        return res.data.Data;
    }
}

function handle({ successMsg, errorMsg, alert }) {
    const store = fetch();

    return (res) => {
        let data = res.data;
        if(data.IsSuccessful) {
            if(alert) {
                store.dispatch(success({
                    text: successMsg
                }));
            }
        } else if(!data.IsSuccessful && data.Error) {
            if(alert) {
                store.dispatch(error({
                    text: data.Error
                }));
            }

            let statusCode = data.StatusCode;
            if(data.Error) {
                statusCode = 200;
            }

            return Promise.reject({ response: { status: statusCode, failed: true } });
        } else {
            if(alert) {
                store.dispatch(error({
                    text: errorMsg
                }));
            }
        }

        return data.Data;
    }
}

function handleError() {
    return (err) => {
        let res = err.response || {};
        let statusCode = res.status || -1;
        let failed = res.failed || true;

        if(statusCode) {
            if(statusCode == 200) {
                if(failed) {
                    throw `Error occured ${statusCode}`;
                }
                return statusCode;
            } else {
                if(appConfig.errorCodes.includes(statusCode)) {
                    window.location.replace(`/error/${statusCode}`);
                } else {
                    window.location.replace(`/error/500`);
                }
                throw `Error occured ${statusCode}`;
            }
        }

        return statusCode;
    }
}

function get({ url, params = {}, config = {}, successMsg, errorMsg, alert = false }) {
    const store = fetch();
    let fullUrl = baseUrl + url;

    if(isCORS) {
        config.headers = Object.assign(config.headers || {}, {
            'Access-Control-Allow-Origin': '*'
        });
    }

    let newConfig = Object.assign(config, { params });

    store.dispatch(setIsFetching(true));
    return axios.get(fullUrl, newConfig)
                .then(handle({ successMsg, errorMsg, alert }))
                .catch(handleError())
                .finally(() => {
                    store.dispatch(setIsFetching(false));
                });
}

function getSimple(url) {
    const store = fetch();
    let config = {};
    let fullUrl = baseUrl + url;

    if(isCORS) {
        config.headers = Object.assign(config.headers || {}, {
            'Access-Control-Allow-Origin': '*'
        });
    }

    store.dispatch(setIsFetching(true));
    return axios.get(fullUrl, config)
                .then(handleSimple())
                .catch(handleError())
                .finally(() => {
                    store.dispatch(setIsFetching(false));
                });
}

function post({ url, data = {}, config = {}, successMsg, errorMsg, loadingMsg, alert = true }) {
    const store = fetch();
    let fullUrl = baseUrl + url;

    if(isCORS) {
        config.headers = Object.assign(config.headers || {}, {
            'Access-Control-Allow-Origin': '*'
        });
    }

    store.dispatch(saving({
        text: loadingMsg || ''
    }));
    return axios.post(fullUrl, data, config)
                .then(handle({ successMsg, errorMsg, alert }))
                .catch(handleError());
}

const ajaxService = {
    handleSimple: handleSimple,
    handle: handle,
    handleError: handleError,
    get: get,
    getSimple: getSimple,
    post: post,
};


export default ajaxService