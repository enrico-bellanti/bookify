import { useEffect, useState } from "react";
import { SearchParams } from "../../model/search";
import { useAccommodationService } from "../../services/accommodation";
import { HeroBanner } from "../../shared/components/core/HeroBanner";
import { Search } from "../../shared/components/core/Search";
import { AccommodationCard } from "./components/AccommodationCard";

export function HomePage() {
    const { actions, state } = useAccommodationService();
    const [searchParams, setSearchParams] = useState<SearchParams>({});

    useEffect(() => {
        // Initial load of accommodations
        actions.getAccommodations({
            includes: 'Address'
        });
    }, []);

    // Handle search with new parameters
    const handleSearch = (params: SearchParams) => {
        setSearchParams(params);
        actions.getAccommodations({
            includes: 'Address',
            filters: `address.city:LIKE:${params.city}`
        }); // Make a new request with search parameters
    };

    return (
        <>
            <div className="jumbotron relative mb-10">
                <HeroBanner />
                <div className="absolute bottom-0 left-0 translate-y-1/2 w-full md:w-[50%]">
                    <div className="search-container px-4">
                        <Search onSearch={handleSearch} />
                    </div>
                </div>
            </div>
            <div className="max-w-screen-lg mx-auto p-10">
                {state.pending ? (
                    <div className="text-center py-10">
                        <p className="text-xl">Loading accommodations...</p>
                    </div>
                ) : state.error ? (
                    <div className="text-center py-10 text-red-600">
                        <p>{state.error}</p>
                    </div>
                ) : state.accommodations.length === 0 ? (
                    <div className="text-center py-10">
                        <p className="text-xl">No accommodations found matching your search.</p>
                        {Object.keys(searchParams).length > 0 && (
                            <button
                                className="mt-4 bg-purple-800 text-white py-2 px-4 rounded hover:bg-purple-700"
                                onClick={() => {
                                    setSearchParams({});
                                    actions.getAccommodations({
                                        includes: 'Address'
                                    });
                                }}
                            >
                                Clear search
                            </button>
                        )}
                    </div>
                ) : (
                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-16">
                        {state.accommodations.map((a) => (
                            <AccommodationCard
                                key={a.id}
                                accommodation={a}
                            />
                        ))}
                    </div>
                )}
            </div>
        </>
    );
}

export default HomePage;