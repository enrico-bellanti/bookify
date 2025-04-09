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

export type AccommodationsGetSuccess = { type: typeof ACCOMMODATIONS_GET_SUCCESS, payload: AccommodationDto[] }
export type AccommodationDeleteSuccess = { type: typeof ACCOMMODATION_DELETE_SUCCESS, payload: number }
export type AccommodationAddSuccess = { type: typeof ACCOMMODATION_ADD_SUCCESS, payload: AccommodationDto }
export type AccommodationEditSuccess = { type: typeof ACCOMMODATION_EDIT_SUCCESS, payload: AccommodationDto }
export type AccommodationSetActive = { type: typeof ACCOMMODATION_SET_ACTIVE, payload: Partial<AccommodationDto> | null }
export type Error = { type: typeof ERROR, payload: string }
export type Pending = { type: typeof PENDING, payload: boolean }

export type AccommodationActions =
    AccommodationsGetSuccess |
    AccommodationDeleteSuccess |
    AccommodationAddSuccess |
    AccommodationEditSuccess |
    AccommodationSetActive |
    Error |
    Pending;