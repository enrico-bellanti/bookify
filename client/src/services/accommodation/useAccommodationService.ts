import { useReducer } from "react";
import { useApiClient } from "../../api/api-client";
import { AccommodationApiApiAccommodationGetRequest, AccommodationApiApiAccommodationIdPutRequest, AccommodationApiApiAccommodationPostRequest, AccommodationDto } from "../../api/src/api";
import {
    ACCOMMODATIONS_GET_SUCCESS,
    ACCOMMODATION_ADD_SUCCESS,
    ACCOMMODATION_DELETE_SUCCESS,
    ACCOMMODATION_EDIT_SUCCESS,
    ACCOMMODATION_SET_ACTIVE,
    ERROR,
    PENDING
} from "./accommodation-action-types";
import { accommodationReducer, initialState } from "./accommodation.reducer";

export type AccommodationCreate = Omit<AccommodationDto, 'id' | 'uuid' | 'ownerId' | 'bookings'>;

export function useAccommodationService() {
    const AccommodationApi = useApiClient().accommodation;
    const [state, dispatch] = useReducer(accommodationReducer, initialState);

    async function getAccommodations(requestParameters: AccommodationApiApiAccommodationGetRequest = {}) {
        dispatch({ type: PENDING, payload: true });
        try {
            const res = await AccommodationApi.apiAccommodationGet(requestParameters);
            dispatch({ type: ACCOMMODATIONS_GET_SUCCESS, payload: res.data.items || [] })
        } catch (e) {
            dispatch({ type: ERROR, payload: 'Accommodations not loaded' })
        }
    }

    async function deleteAccommodation(id: number) {
        dispatch({ type: PENDING, payload: true })
        try {
            await AccommodationApi.apiAccommodationIdDelete({
                id
            });
            dispatch({ type: ACCOMMODATION_DELETE_SUCCESS, payload: id })
        } catch (e) {
            dispatch({ type: ERROR, payload: 'Accommodations not deleted' })
        }
    }

    async function addAccommodation(accommodationCreate: AccommodationApiApiAccommodationPostRequest) {
        dispatch({ type: PENDING, payload: true })
        try {
            const res = await AccommodationApi.apiAccommodationPost(accommodationCreate);
            dispatch({ type: ACCOMMODATION_ADD_SUCCESS, payload: res.data })
        } catch (e) {
            dispatch({ type: ERROR, payload: 'Accommodations not added' })
        }
    }

    async function editAccommodation(accommodationUpdate: AccommodationApiApiAccommodationIdPutRequest) {
        dispatch({ type: PENDING, payload: true })
        try {
            const res = await AccommodationApi.apiAccommodationIdPut(accommodationUpdate);
            dispatch({ type: ACCOMMODATION_EDIT_SUCCESS, payload: res.data })
        } catch (e) {
            dispatch({ type: ERROR, payload: 'Accommodations not edited' })
        }
    }

    function setActiveItem(accommodation: AccommodationDto) {
        dispatch({ type: ACCOMMODATION_SET_ACTIVE, payload: accommodation })
    }

    function resetActiveItem() {
        dispatch({ type: ACCOMMODATION_SET_ACTIVE, payload: null })
    }

    return {
        actions: {
            getAccommodations,
            deleteAccommodation,
            addAccommodation,
            editAccommodation,
            setActiveItem,
            resetActiveItem
        },
        state
    }
}