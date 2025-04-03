import { AccommodationDto } from "../../../open-api/src/api";

interface AccommodationCardProps{
    accommodation: AccommodationDto
}

export function AccommodationCard(props: AccommodationCardProps){
    const {accommodation} = props

    return (
        <div 
            className="bg-white rounded-xl overflow-hidden text-black shadow-2xl border-2 border-purple-800"
        >
            {/* {product.img && <img src={product.img} alt={product.name} className="h-64 w-full object-cover"/>} */}
            <div className="flex justify-center items-center flex-col p-3 tect-xl font-bold">
                <div>{accommodation.name}</div>
                <div>{accommodation.type}</div>
            </div>
            <p className="p-3">{accommodation.address?.city}</p>

            <button
                // onClick={() => props.onAddToCart(product)}
                className="text-white bg-purple-500 hover:bg-slate-400 transition w-full text-center font-bold p-3"
            >Book Now!</button>
        </div>
    )
}