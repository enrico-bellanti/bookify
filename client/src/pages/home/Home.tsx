import { HeroBanner } from "../../shared/components/core/HeroBanner";
import { Search } from "../../shared/components/core/Search";

export function Home(){
    return (
        <div className="jumbotron relative">
            <HeroBanner />
            <div className="absolute bottom-0 left-0 translate-y-1/2 w-full">
                <div className="search-container px-4">
                    <Search />
                </div>
            </div>
        </div>
    )
}