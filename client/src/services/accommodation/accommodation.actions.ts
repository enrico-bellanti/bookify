import { AccommodationDto } from "../../open-api/src/api"

export type AccommodationsGetSuccess = { type: 'accommodationsGetSuccess', payload: AccommodationDto[] }
export type AccommodationDeleteSuccess = { type: 'accommodationDeleteSuccess', payload: number }
export type AccommodationAddSuccess = { type: 'accommodationAddSuccess', payload: AccommodationDto }
export type AccommodationEditSuccess = { type: 'accommodationEditSuccess', payload: AccommodationDto }
export type AccommodationSetActive = { type: 'accommodationSetActive', payload: Partial<AccommodationDto> | null }
export type Error = { type: 'error', payload: string }
export type Pending = { type: 'pending', payload: boolean }


export type AccommodationActions =
    AccommodationsGetSuccess |
    AccommodationDeleteSuccess |
    AccommodationAddSuccess |
    AccommodationEditSuccess |
    AccommodationSetActive |
    Error |
    Pending;