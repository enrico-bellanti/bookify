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
import type { BookingStatus } from './booking-status';

/**
 * 
 * @export
 * @interface BookingDto
 */
export interface BookingDto {
    /**
     * 
     * @type {string}
     * @memberof BookingDto
     */
    'checkInDate'?: string;
    /**
     * 
     * @type {string}
     * @memberof BookingDto
     */
    'checkOutDate'?: string;
    /**
     * 
     * @type {number}
     * @memberof BookingDto
     */
    'totalPrice'?: number;
    /**
     * 
     * @type {BookingStatus}
     * @memberof BookingDto
     */
    'status'?: BookingStatus;
    /**
     * 
     * @type {number}
     * @memberof BookingDto
     */
    'userId'?: number;
    /**
     * 
     * @type {number}
     * @memberof BookingDto
     */
    'accommodationId'?: number;
}



