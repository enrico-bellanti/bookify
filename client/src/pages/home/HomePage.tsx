import { useEffect } from "react";
import { useAccommodationService } from "../../services/accommodation";
import { HeroBanner } from "../../shared/components/core/HeroBanner";
import { Search } from "../../shared/components/core/Search";
import { AccommodationCard } from "./components/AccommodationCard";

export function HomePage(){
    const {actions, state} = useAccommodationService()

    useEffect(() => {
        actions.getAccommodations()
    }, [])

    return (
        <>        
            <div className="jumbotron relative mb-10">
                <HeroBanner />
                <div className="absolute bottom-0 left-0 translate-y-1/2 w-full">
                    <div className="search-container px-4">
                        <Search />
                    </div>
                </div>
            </div>
            {/* <TestPage></TestPage> */}
            <div className="p-4">
                <div className="grid grid-cols-1 sm:grid-cols-2 gap-16">
                    {
                        state.accommodations.map(a => (
                            <AccommodationCard
                                key={a.id}
                                accommodation={a}
                            ></AccommodationCard>
                        ))
                    }
                </div>
            </div>
        </>
    )
}

export default HomePage;