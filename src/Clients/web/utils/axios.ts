import Axios, { AxiosRequestConfig } from "axios";
import { ApiUrl } from "configs";
import authTokenStorage from "./authTokenStorage";

const authenticationInterceptor = (config: AxiosRequestConfig) => {
    const authToken = authTokenStorage.get();

    if (authToken !== null && config.headers !== undefined) {
        config.headers.authorization = authToken;
    }

    return config;
};

export const axios = Axios.create({
    baseURL: ApiUrl
});

axios.interceptors.request.use(authenticationInterceptor);
