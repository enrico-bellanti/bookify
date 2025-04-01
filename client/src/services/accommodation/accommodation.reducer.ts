import { AccomodationDto } from "../../open-api/src/api";
import { AccommodationActions } from "./accommodation.actions";

export interface AccomodationState {
    products: AccomodationDto[];
    pending: boolean;
    error: string | null
    activeItem: Partial<AccomodationDto> | null
}
export const initialState: AccomodationState = {
    products: [],
    pending: false,
    error: null,
    activeItem: null
};

export function productsReducer(state: AccomodationState, action: AccommodationActions) {
    const { type, payload } = action;

    switch (type) {
        case 'accommodationsGetSuccess':
            return { ...state, products: payload, pending: false, error: null }
        // case 'accommodationDeleteSuccess':
        //     return {
        //         ...state,
        //         products: state.products.filter(item => item.id !== payload),
        //         error: null,
        //         pending: false,
        //         activeItem: null,
        //     };
        // case 'accommodationAddSuccess':
        //     return {
        //         ...state,
        //         products: [...state.products, payload],
        //         activeItem: null,
        //         error: null,
        //         pending: false
        //     };
        // case 'accommodationEditSuccess':
        //     return {
        //         ...state,
        //         products: state.products.map(item => item.id === payload.id ? payload : item),
        //         activeItem: null,
        //         error: null,
        //         pending: false,
        //     };
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

