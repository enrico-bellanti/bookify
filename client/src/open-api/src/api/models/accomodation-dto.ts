/* tslint:disable */
/* eslint-disable */
/**
 * Token Validation API
 * API per testare la validazione dei token JWT da Keycloak
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
 * @interface AccomodationDto
 */
export interface AccomodationDto {
    /**
     * 
     * @type {string}
     * @memberof AccomodationDto
     */
    'name': string | null;
    /**
     * 
     * @type {AccommodationType}
     * @memberof AccomodationDto
     */
    'type': AccommodationType;
    /**
     * 
     * @type {number}
     * @memberof AccomodationDto
     */
    'ownerId': number;
    /**
     * 
     * @type {AddressDto}
     * @memberof AccomodationDto
     */
    'address': AddressDto;
}



