import { AccommodationDto } from "../../api/src/api";
import {
    ACCOMMODATIONS_GET_SUCCESS,
    ACCOMMODATION_ADD_SUCCESS,
    ACCOMMODATION_DELETE_SUCCESS,
    ACCOMMODATION_EDIT_SUCCESS,
    ACCOMMODATION_SET_ACTIVE,
    ERROR,
    PENDING
} from "./accommodation-action-types";
import { AccommodationActions } from "./accommodation.actions";

export interface AccommodationState {
    accommodations: AccommodationDto[];
    activeAccommodation: Partial<AccommodationDto> | null;
    loading: boolean;
    error: string | null;
}

export const initialState: AccommodationState = {
    accommodations: [],
    activeAccommodation: null,
    loading: false,
    error: null
};

export function accommodationReducer(state: AccommodationState, action: AccommodationActions): AccommodationState {
    switch (action.type) {
        case ACCOMMODATIONS_GET_SUCCESS:
            return {
                ...state,
                accommodations: action.payload,
                loading: false,
                error: null
            };

        case ACCOMMODATION_DELETE_SUCCESS:
            return {
                ...state,
                accommodations: state.accommodations.filter(accommodation => accommodation.id !== action.payload),
                loading: false,
                error: null
            };

        case ACCOMMODATION_ADD_SUCCESS:
            return {
                ...state,
                accommodations: [...state.accommodations, action.payload],
                loading: false,
                error: null
            };

        case ACCOMMODATION_EDIT_SUCCESS:
            return {
                ...state,
                accommodations: state.accommodations.map(accommodation =>
                    accommodation.id === action.payload.id ? action.payload : accommodation
                ),
                loading: false,
                error: null
            };

        case ACCOMMODATION_SET_ACTIVE:
            return {
                ...state,
                activeAccommodation: action.payload
            };

        case ERROR:
            return {
                ...state,
                loading: false,
                error: action.payload
            };

        case PENDING:
            return {
                ...state,
                loading: action.payload,
                error: null
            };

        default:
            return state;
    }
}