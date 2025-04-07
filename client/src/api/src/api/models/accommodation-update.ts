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


// May contain unused imports in some cases
// @ts-ignore
import type { AccommodationType } from './accommodation-type';
// May contain unused imports in some cases
// @ts-ignore
import type { AddressDto } from './address-dto';

/**
 * 
 * @export
 * @interface AccommodationUpdate
 */
export interface AccommodationUpdate {
    /**
     * 
     * @type {string}
     * @memberof AccommodationUpdate
     */
    'name'?: string | null;
    /**
     * 
     * @type {AccommodationType}
     * @memberof AccommodationUpdate
     */
    'type'?: AccommodationType;
    /**
     * 
     * @type {File}
     * @memberof AccommodationUpdate
     */
    'imgFile'?: File | null;
    /**
     * 
     * @type {AddressDto}
     * @memberof AccommodationUpdate
     */
    'address'?: AddressDto;
}



