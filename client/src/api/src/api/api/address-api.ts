/* tslint:disable */
/* eslint-disable */
/**
 * Bookify API
 * API del back-end di Bookify
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import type { Configuration } from '../configuration';
import type { AxiosPromise, AxiosInstance, RawAxiosRequestConfig } from 'axios';
import globalAxios from 'axios';
// Some imports not used depending on template conditions
// @ts-ignore
import { DUMMY_BASE_URL, assertParamExists, setApiKeyToObject, setBasicAuthToObject, setBearerAuthToObject, setOAuthToObject, setSearchParams, serializeDataIfNeeded, toPathString, createRequestFunction } from '../common';
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, type RequestArgs, BaseAPI, RequiredError, operationServerMap } from '../base';
// @ts-ignore
import type { AddressDto } from '../models';
/**
 * AddressApi - axios parameter creator
 * @export
 */
export const AddressApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @param {number} [page] 
         * @param {number} [size] 
         * @param {string} [sortBy] 
         * @param {boolean} [isDescending] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressGet: async (page?: number, size?: number, sortBy?: string, isDescending?: boolean, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/Address`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)

            if (page !== undefined) {
                localVarQueryParameter['page'] = page;
            }

            if (size !== undefined) {
                localVarQueryParameter['size'] = size;
            }

            if (sortBy !== undefined) {
                localVarQueryParameter['sortBy'] = sortBy;
            }

            if (isDescending !== undefined) {
                localVarQueryParameter['isDescending'] = isDescending;
            }


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {number} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressIdDelete: async (id: number, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            assertParamExists('apiAddressIdDelete', 'id', id)
            const localVarPath = `/api/Address/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'DELETE', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {number} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressIdGet: async (id: number, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            assertParamExists('apiAddressIdGet', 'id', id)
            const localVarPath = `/api/Address/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {number} id 
         * @param {AddressDto} [addressDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressIdPut: async (id: number, addressDto?: AddressDto, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            assertParamExists('apiAddressIdPut', 'id', id)
            const localVarPath = `/api/Address/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'PUT', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)


    
            localVarHeaderParameter['Content-Type'] = 'application/json';

            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            localVarRequestOptions.data = serializeDataIfNeeded(addressDto, localVarRequestOptions, configuration)

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {AddressDto} [addressDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressPost: async (addressDto?: AddressDto, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/Address`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)

            // authentication oauth2 required
            // oauth required
            await setOAuthToObject(localVarHeaderParameter, "oauth2", ["openid", "profile", "email"], configuration)


    
            localVarHeaderParameter['Content-Type'] = 'application/json';

            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            localVarRequestOptions.data = serializeDataIfNeeded(addressDto, localVarRequestOptions, configuration)

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * AddressApi - functional programming interface
 * @export
 */
export const AddressApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = AddressApiAxiosParamCreator(configuration)
    return {
        /**
         * 
         * @param {number} [page] 
         * @param {number} [size] 
         * @param {string} [sortBy] 
         * @param {boolean} [isDescending] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiAddressGet(page?: number, size?: number, sortBy?: string, isDescending?: boolean, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiAddressGet(page, size, sortBy, isDescending, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['AddressApi.apiAddressGet']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {number} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiAddressIdDelete(id: number, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiAddressIdDelete(id, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['AddressApi.apiAddressIdDelete']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {number} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiAddressIdGet(id: number, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiAddressIdGet(id, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['AddressApi.apiAddressIdGet']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {number} id 
         * @param {AddressDto} [addressDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiAddressIdPut(id: number, addressDto?: AddressDto, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiAddressIdPut(id, addressDto, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['AddressApi.apiAddressIdPut']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {AddressDto} [addressDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiAddressPost(addressDto?: AddressDto, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiAddressPost(addressDto, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['AddressApi.apiAddressPost']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
    }
};

/**
 * AddressApi - factory interface
 * @export
 */
export const AddressApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = AddressApiFp(configuration)
    return {
        /**
         * 
         * @param {AddressApiApiAddressGetRequest} requestParameters Request parameters.
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressGet(requestParameters: AddressApiApiAddressGetRequest = {}, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiAddressGet(requestParameters.page, requestParameters.size, requestParameters.sortBy, requestParameters.isDescending, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {AddressApiApiAddressIdDeleteRequest} requestParameters Request parameters.
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressIdDelete(requestParameters: AddressApiApiAddressIdDeleteRequest, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiAddressIdDelete(requestParameters.id, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {AddressApiApiAddressIdGetRequest} requestParameters Request parameters.
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressIdGet(requestParameters: AddressApiApiAddressIdGetRequest, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiAddressIdGet(requestParameters.id, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {AddressApiApiAddressIdPutRequest} requestParameters Request parameters.
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressIdPut(requestParameters: AddressApiApiAddressIdPutRequest, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiAddressIdPut(requestParameters.id, requestParameters.addressDto, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {AddressApiApiAddressPostRequest} requestParameters Request parameters.
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiAddressPost(requestParameters: AddressApiApiAddressPostRequest = {}, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiAddressPost(requestParameters.addressDto, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * AddressApi - interface
 * @export
 * @interface AddressApi
 */
export interface AddressApiInterface {
    /**
     * 
     * @param {AddressApiApiAddressGetRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApiInterface
     */
    apiAddressGet(requestParameters?: AddressApiApiAddressGetRequest, options?: RawAxiosRequestConfig): AxiosPromise<void>;

    /**
     * 
     * @param {AddressApiApiAddressIdDeleteRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApiInterface
     */
    apiAddressIdDelete(requestParameters: AddressApiApiAddressIdDeleteRequest, options?: RawAxiosRequestConfig): AxiosPromise<void>;

    /**
     * 
     * @param {AddressApiApiAddressIdGetRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApiInterface
     */
    apiAddressIdGet(requestParameters: AddressApiApiAddressIdGetRequest, options?: RawAxiosRequestConfig): AxiosPromise<void>;

    /**
     * 
     * @param {AddressApiApiAddressIdPutRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApiInterface
     */
    apiAddressIdPut(requestParameters: AddressApiApiAddressIdPutRequest, options?: RawAxiosRequestConfig): AxiosPromise<void>;

    /**
     * 
     * @param {AddressApiApiAddressPostRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApiInterface
     */
    apiAddressPost(requestParameters?: AddressApiApiAddressPostRequest, options?: RawAxiosRequestConfig): AxiosPromise<void>;

}

/**
 * Request parameters for apiAddressGet operation in AddressApi.
 * @export
 * @interface AddressApiApiAddressGetRequest
 */
export interface AddressApiApiAddressGetRequest {
    /**
     * 
     * @type {number}
     * @memberof AddressApiApiAddressGet
     */
    readonly page?: number

    /**
     * 
     * @type {number}
     * @memberof AddressApiApiAddressGet
     */
    readonly size?: number

    /**
     * 
     * @type {string}
     * @memberof AddressApiApiAddressGet
     */
    readonly sortBy?: string

    /**
     * 
     * @type {boolean}
     * @memberof AddressApiApiAddressGet
     */
    readonly isDescending?: boolean
}

/**
 * Request parameters for apiAddressIdDelete operation in AddressApi.
 * @export
 * @interface AddressApiApiAddressIdDeleteRequest
 */
export interface AddressApiApiAddressIdDeleteRequest {
    /**
     * 
     * @type {number}
     * @memberof AddressApiApiAddressIdDelete
     */
    readonly id: number
}

/**
 * Request parameters for apiAddressIdGet operation in AddressApi.
 * @export
 * @interface AddressApiApiAddressIdGetRequest
 */
export interface AddressApiApiAddressIdGetRequest {
    /**
     * 
     * @type {number}
     * @memberof AddressApiApiAddressIdGet
     */
    readonly id: number
}

/**
 * Request parameters for apiAddressIdPut operation in AddressApi.
 * @export
 * @interface AddressApiApiAddressIdPutRequest
 */
export interface AddressApiApiAddressIdPutRequest {
    /**
     * 
     * @type {number}
     * @memberof AddressApiApiAddressIdPut
     */
    readonly id: number

    /**
     * 
     * @type {AddressDto}
     * @memberof AddressApiApiAddressIdPut
     */
    readonly addressDto?: AddressDto
}

/**
 * Request parameters for apiAddressPost operation in AddressApi.
 * @export
 * @interface AddressApiApiAddressPostRequest
 */
export interface AddressApiApiAddressPostRequest {
    /**
     * 
     * @type {AddressDto}
     * @memberof AddressApiApiAddressPost
     */
    readonly addressDto?: AddressDto
}

/**
 * AddressApi - object-oriented interface
 * @export
 * @class AddressApi
 * @extends {BaseAPI}
 */
export class AddressApi extends BaseAPI implements AddressApiInterface {
    /**
     * 
     * @param {AddressApiApiAddressGetRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApi
     */
    public apiAddressGet(requestParameters: AddressApiApiAddressGetRequest = {}, options?: RawAxiosRequestConfig) {
        return AddressApiFp(this.configuration).apiAddressGet(requestParameters.page, requestParameters.size, requestParameters.sortBy, requestParameters.isDescending, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {AddressApiApiAddressIdDeleteRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApi
     */
    public apiAddressIdDelete(requestParameters: AddressApiApiAddressIdDeleteRequest, options?: RawAxiosRequestConfig) {
        return AddressApiFp(this.configuration).apiAddressIdDelete(requestParameters.id, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {AddressApiApiAddressIdGetRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApi
     */
    public apiAddressIdGet(requestParameters: AddressApiApiAddressIdGetRequest, options?: RawAxiosRequestConfig) {
        return AddressApiFp(this.configuration).apiAddressIdGet(requestParameters.id, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {AddressApiApiAddressIdPutRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApi
     */
    public apiAddressIdPut(requestParameters: AddressApiApiAddressIdPutRequest, options?: RawAxiosRequestConfig) {
        return AddressApiFp(this.configuration).apiAddressIdPut(requestParameters.id, requestParameters.addressDto, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {AddressApiApiAddressPostRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof AddressApi
     */
    public apiAddressPost(requestParameters: AddressApiApiAddressPostRequest = {}, options?: RawAxiosRequestConfig) {
        return AddressApiFp(this.configuration).apiAddressPost(requestParameters.addressDto, options).then((request) => request(this.axios, this.basePath));
    }
}

