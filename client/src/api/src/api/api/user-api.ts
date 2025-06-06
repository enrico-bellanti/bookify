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
import type { AddUserRequestDto } from '../models';
// @ts-ignore
import type { UpdateUserDto } from '../models';
/**
 * UserApi - axios parameter creator
 * @export
 */
export const UserApiAxiosParamCreator = function (configuration?: Configuration) {
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
        apiUserGet: async (page?: number, size?: number, sortBy?: string, isDescending?: boolean, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/User`;
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
        apiUserIdGet: async (id: number, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            assertParamExists('apiUserIdGet', 'id', id)
            const localVarPath = `/api/User/{id}`
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
         * @param {UpdateUserDto} [updateUserDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiUserIdPatch: async (id: number, updateUserDto?: UpdateUserDto, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            assertParamExists('apiUserIdPatch', 'id', id)
            const localVarPath = `/api/User/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'PATCH', ...baseOptions, ...options};
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
            localVarRequestOptions.data = serializeDataIfNeeded(updateUserDto, localVarRequestOptions, configuration)

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {AddUserRequestDto} [addUserRequestDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiUserPost: async (addUserRequestDto?: AddUserRequestDto, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/User`;
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
            localVarRequestOptions.data = serializeDataIfNeeded(addUserRequestDto, localVarRequestOptions, configuration)

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * UserApi - functional programming interface
 * @export
 */
export const UserApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = UserApiAxiosParamCreator(configuration)
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
        async apiUserGet(page?: number, size?: number, sortBy?: string, isDescending?: boolean, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiUserGet(page, size, sortBy, isDescending, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['UserApi.apiUserGet']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {number} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiUserIdGet(id: number, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiUserIdGet(id, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['UserApi.apiUserIdGet']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {number} id 
         * @param {UpdateUserDto} [updateUserDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiUserIdPatch(id: number, updateUserDto?: UpdateUserDto, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiUserIdPatch(id, updateUserDto, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['UserApi.apiUserIdPatch']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
        /**
         * 
         * @param {AddUserRequestDto} [addUserRequestDto] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiUserPost(addUserRequestDto?: AddUserRequestDto, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiUserPost(addUserRequestDto, options);
            const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
            const localVarOperationServerBasePath = operationServerMap['UserApi.apiUserPost']?.[localVarOperationServerIndex]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, localVarOperationServerBasePath || basePath);
        },
    }
};

/**
 * UserApi - factory interface
 * @export
 */
export const UserApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = UserApiFp(configuration)
    return {
        /**
         * 
         * @param {UserApiApiUserGetRequest} requestParameters Request parameters.
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiUserGet(requestParameters: UserApiApiUserGetRequest = {}, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiUserGet(requestParameters.page, requestParameters.size, requestParameters.sortBy, requestParameters.isDescending, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {UserApiApiUserIdGetRequest} requestParameters Request parameters.
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiUserIdGet(requestParameters: UserApiApiUserIdGetRequest, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiUserIdGet(requestParameters.id, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {UserApiApiUserIdPatchRequest} requestParameters Request parameters.
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiUserIdPatch(requestParameters: UserApiApiUserIdPatchRequest, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiUserIdPatch(requestParameters.id, requestParameters.updateUserDto, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {UserApiApiUserPostRequest} requestParameters Request parameters.
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiUserPost(requestParameters: UserApiApiUserPostRequest = {}, options?: RawAxiosRequestConfig): AxiosPromise<void> {
            return localVarFp.apiUserPost(requestParameters.addUserRequestDto, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * UserApi - interface
 * @export
 * @interface UserApi
 */
export interface UserApiInterface {
    /**
     * 
     * @param {UserApiApiUserGetRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApiInterface
     */
    apiUserGet(requestParameters?: UserApiApiUserGetRequest, options?: RawAxiosRequestConfig): AxiosPromise<void>;

    /**
     * 
     * @param {UserApiApiUserIdGetRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApiInterface
     */
    apiUserIdGet(requestParameters: UserApiApiUserIdGetRequest, options?: RawAxiosRequestConfig): AxiosPromise<void>;

    /**
     * 
     * @param {UserApiApiUserIdPatchRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApiInterface
     */
    apiUserIdPatch(requestParameters: UserApiApiUserIdPatchRequest, options?: RawAxiosRequestConfig): AxiosPromise<void>;

    /**
     * 
     * @param {UserApiApiUserPostRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApiInterface
     */
    apiUserPost(requestParameters?: UserApiApiUserPostRequest, options?: RawAxiosRequestConfig): AxiosPromise<void>;

}

/**
 * Request parameters for apiUserGet operation in UserApi.
 * @export
 * @interface UserApiApiUserGetRequest
 */
export interface UserApiApiUserGetRequest {
    /**
     * 
     * @type {number}
     * @memberof UserApiApiUserGet
     */
    readonly page?: number

    /**
     * 
     * @type {number}
     * @memberof UserApiApiUserGet
     */
    readonly size?: number

    /**
     * 
     * @type {string}
     * @memberof UserApiApiUserGet
     */
    readonly sortBy?: string

    /**
     * 
     * @type {boolean}
     * @memberof UserApiApiUserGet
     */
    readonly isDescending?: boolean
}

/**
 * Request parameters for apiUserIdGet operation in UserApi.
 * @export
 * @interface UserApiApiUserIdGetRequest
 */
export interface UserApiApiUserIdGetRequest {
    /**
     * 
     * @type {number}
     * @memberof UserApiApiUserIdGet
     */
    readonly id: number
}

/**
 * Request parameters for apiUserIdPatch operation in UserApi.
 * @export
 * @interface UserApiApiUserIdPatchRequest
 */
export interface UserApiApiUserIdPatchRequest {
    /**
     * 
     * @type {number}
     * @memberof UserApiApiUserIdPatch
     */
    readonly id: number

    /**
     * 
     * @type {UpdateUserDto}
     * @memberof UserApiApiUserIdPatch
     */
    readonly updateUserDto?: UpdateUserDto
}

/**
 * Request parameters for apiUserPost operation in UserApi.
 * @export
 * @interface UserApiApiUserPostRequest
 */
export interface UserApiApiUserPostRequest {
    /**
     * 
     * @type {AddUserRequestDto}
     * @memberof UserApiApiUserPost
     */
    readonly addUserRequestDto?: AddUserRequestDto
}

/**
 * UserApi - object-oriented interface
 * @export
 * @class UserApi
 * @extends {BaseAPI}
 */
export class UserApi extends BaseAPI implements UserApiInterface {
    /**
     * 
     * @param {UserApiApiUserGetRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    public apiUserGet(requestParameters: UserApiApiUserGetRequest = {}, options?: RawAxiosRequestConfig) {
        return UserApiFp(this.configuration).apiUserGet(requestParameters.page, requestParameters.size, requestParameters.sortBy, requestParameters.isDescending, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {UserApiApiUserIdGetRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    public apiUserIdGet(requestParameters: UserApiApiUserIdGetRequest, options?: RawAxiosRequestConfig) {
        return UserApiFp(this.configuration).apiUserIdGet(requestParameters.id, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {UserApiApiUserIdPatchRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    public apiUserIdPatch(requestParameters: UserApiApiUserIdPatchRequest, options?: RawAxiosRequestConfig) {
        return UserApiFp(this.configuration).apiUserIdPatch(requestParameters.id, requestParameters.updateUserDto, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {UserApiApiUserPostRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    public apiUserPost(requestParameters: UserApiApiUserPostRequest = {}, options?: RawAxiosRequestConfig) {
        return UserApiFp(this.configuration).apiUserPost(requestParameters.addUserRequestDto, options).then((request) => request(this.axios, this.basePath));
    }
}

