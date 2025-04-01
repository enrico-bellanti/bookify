import { AccomodationDto } from "../../open-api/src/api"

export type AccommodationsGetSuccess = { type: 'accommodationsGetSuccess', payload: AccomodationDto[] }
export type AccommodationDeleteSuccess = { type: 'accommodationDeleteSuccess', payload: AccomodationDto }
export type AccommodationAddSuccess = { type: 'accommodationAddSuccess', payload: AccomodationDto }
export type AccommodationEditSuccess = { type: 'accommodationEditSuccess', payload: AccomodationDto }
export type AccommodationSetActive = { type: 'accommodationSetActive', payload: Partial<AccomodationDto> | null }
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