import { SearchParams } from "@/model/";
import { useAccommodationService } from "@/services/";
import { HeroBanner, Search } from "@/shared/";
import { useEffect, useRef, useState } from "react";
import { AccommodationCard } from "./components/AccommodationCard";

export function HomePage() {
    const { actions, state } = useAccommodationService();
    const [searchParams, setSearchParams] = useState<SearchParams>({});
    const [isSticky, setIsSticky] = useState(false);
    const bannerRef = useRef<HTMLDivElement>(null);
    const searchWrapperRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        // Initial load of accommodations
        actions.getAccommodations({
            includes: 'Address'
        });

        // Add scroll event listener to handle sticky behavior
        const handleScroll = () => {
            if (bannerRef.current && searchWrapperRef.current) {
                const bannerBottom = bannerRef.current.getBoundingClientRect().bottom;
                const navbarHeight = 104; // Height of navbar (h-26 = 104px)
                
                // Set sticky when banner bottom reaches the navbar bottom
                if (bannerBottom <= navbarHeight) {
                    setIsSticky(true);
                } else {
                    setIsSticky(false);
                }
            }
        };

        window.addEventListener('scroll', handleScroll);
        
        // Initial check
        handleScroll();
        
        return () => {
            window.removeEventListener('scroll', handleScroll);
        };
    }, []);

    // Handle search with new parameters
    const handleSearch = (params: SearchParams) => {
        setSearchParams(params);
        actions.getAccommodations({
            includes: 'Address',
            filters: params.filters || ''
        });
    };

    return (
        <>
            {/* Navbar height compensation */}
            <div className="home-container">
                {/* Hero Banner Section */}
                <div ref={bannerRef} className="jumbotron relative">
                    <HeroBanner />
                </div>
                
                {/* Search Wrapper - conditionally sticky */}
                <div 
                    ref={searchWrapperRef} 
                    className={`search-wrapper ${isSticky ? 'fixed-search' : ''}`}
                    style={{
                        position: isSticky ? 'fixed' : 'relative',
                        top: isSticky ? '104px' : 'auto',
                        left: 0,
                        right: 0,
                        zIndex: 10,
                        background: 'white'
                    }}
                >
                    <div className="flex flex-col overflow-hidden">
                        {/* Top half - purple background */}
                        <div className="bg-purple-500 h-14"></div>
                        
                        {/* Bottom half - white background */}
                        <div className="bg-white h-14"></div>
                    </div>
                    
                    {/* Search Container */}
                    <div className="absolute top-0 left-0 w-full">
                        <div className="search-container px-4 w-[70%]">
                            <Search onSearch={handleSearch} />
                        </div>
                    </div>
                </div>
                
                {/* Add spacer when search is sticky to prevent content jump */}
                {isSticky && <div style={{ height: '96px' }}></div>}
                
                {/* Content Container */}
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
            </div>
        </>
    );
}

export default HomePage;