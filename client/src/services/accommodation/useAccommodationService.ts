import { useReducer } from "react";
import { createApiClient } from "../../open-api/api-client";
import { AccommodationCreate, AccommodationDto, AccommodationUpdate } from "../../open-api/src/api";
import { accommodationReducer, initialState } from "./accommodation.reducer";


export function useAccommodationService() {
    const AccommodationApi = createApiClient().accommodation;
    const [state, dispatch] = useReducer(accommodationReducer, initialState);

    async function getAccommodations() {
        dispatch({ type: 'pending', payload: true });
        try {
            const res = await AccommodationApi.apiAccommodationGet({
                includes: 'Address'
            });
            dispatch({ type: 'accommodationsGetSuccess', payload: res.data.items || [] })
        } catch (e) {
            dispatch({ type: 'error', payload: 'Accommodations not loaded' })
        }
    }

    async function deleteAccommodation(id: number) {
        dispatch({ type: 'pending', payload: true })
        try {
            await AccommodationApi.apiAccommodationIdDelete({
                id
            });
            dispatch({ type: 'accommodationDeleteSuccess', payload: id })
        } catch (e) {
            dispatch({ type: 'error', payload: 'Accommodations not deleted' })
        }
    }



    async function addAccommodation(accommodationCreate: AccommodationCreate) {
        dispatch({ type: 'pending', payload: true })
        try {
            const res = await AccommodationApi.apiAccommodationPost({
                accommodationCreate
            });
            dispatch({ type: 'accommodationAddSuccess', payload: res.data })
        } catch (e) {
            dispatch({ type: 'error', payload: 'Accommodations not added' })
        }
    }

    async function editAccommodation(id: number, accommodationUpdate: AccommodationUpdate) {
        dispatch({ type: 'pending', payload: true })
        try {
            const res = await AccommodationApi.apiAccommodationIdPut({
                id,
                accommodationUpdate
            });
            dispatch({ type: 'accommodationEditSuccess', payload: res.data })
        } catch (e) {
            dispatch({ type: 'error', payload: 'Accommodations not edited' })
        }
    }

    function setActiveItem(accommodation: AccommodationDto) {
        dispatch({ type: 'accommodationSetActive', payload: accommodation })
    }

    function resetActiveItem() {
        dispatch({ type: 'accommodationSetActive', payload: null })
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
