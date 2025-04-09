
// accommodation-action-types.ts
export const ACCOMMODATIONS_GET_SUCCESS = 'accommodationsGetSuccess';
export const ACCOMMODATION_DELETE_SUCCESS = 'accommodationDeleteSuccess';
export const ACCOMMODATION_ADD_SUCCESS = 'accommodationAddSuccess';
export const ACCOMMODATION_EDIT_SUCCESS = 'accommodationEditSuccess';
export const ACCOMMODATION_SET_ACTIVE = 'accommodationSetActive';
export const ERROR = 'error';
export const PENDING = 'pending';

// Export all action types as an object for easier imports
export const ActionTypes = {
    ACCOMMODATIONS_GET_SUCCESS,
    ACCOMMODATION_DELETE_SUCCESS,
    ACCOMMODATION_ADD_SUCCESS,
    ACCOMMODATION_EDIT_SUCCESS,
    ACCOMMODATION_SET_ACTIVE,
    ERROR,
    PENDING
} as const;