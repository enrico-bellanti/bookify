import { AccommodationDto } from "../../api/src/api";
import { AccommodationActions } from "./accommodation.actions";

export interface AccomodationState {
    accommodations: AccommodationDto[];
    pending: boolean;
    error: string | null
    activeItem: Partial<AccommodationDto> | null
}
export const initialState: AccomodationState = {
    accommodations: [],
    pending: false,
    error: null,
    activeItem: null
};

export function accommodationReducer(state: AccomodationState, action: AccommodationActions) {
    const { type, payload } = action;

    switch (type) {
        case 'accommodationsGetSuccess':
            return { ...state, accommodations: payload, pending: false, error: null }
        case 'accommodationDeleteSuccess':
            return {
                ...state,
                accommodations: state.accommodations.filter(item => item.id !== payload),
                error: null,
                pending: false,
                activeItem: null,
            };
        case 'accommodationAddSuccess':
            return {
                ...state,
                accommodations: [...state.accommodations, payload],
                activeItem: null,
                error: null,
                pending: false
            };
        case 'accommodationEditSuccess':
            return {
                ...state,
                accommodations: state.accommodations.map(item => item.id === payload.id ? payload : item),
                activeItem: null,
                error: null,
                pending: false,
            };
        case 'accommodationSetActive':
            return { ...state, activeItem: payload }
        case 'pending':
            return { ...state, pending: payload, error: null };
        case 'error':
            return { ...state, error: payload, pending: false }
        default:
            return state;
    }
}

