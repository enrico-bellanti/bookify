import { useState } from "react";
import { SearchParams, SearchProps } from "../../../model/search";

export function Search({ onSearch }: SearchProps) {
    const [searchTerm, setSearchTerm] = useState("");
    const [activeFilter, setActiveFilter] = useState("city"); // Default filter type
    
    const handleSearch = () => {
        if (searchTerm.trim()) {
            // Call the onSearch callback with the search parameters
            const params: SearchParams = {
                [activeFilter]: searchTerm
            };
            onSearch(params);
        }
    };
    
    const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === "Enter") {
            handleSearch();
        }
    };
    
    const filterOptions = [
        { id: "city", label: "City" },
        { id: "priceMin", label: "Min Price" },
        { id: "priceMax", label: "Max Price" }
    ];
    
    return (
        <div className="bg-white p-4 rounded-lg shadow-lg">
            <div className="flex flex-col md:flex-row gap-2">
                <div className="flex-grow">
                    <input
                        type="text"
                        className="w-full p-2 border border-gray-300 rounded"
                        placeholder={`Search by ${activeFilter}...`}
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        onKeyDown={handleKeyPress}
                    />
                </div>
                <button
                    className="bg-purple-800 text-white py-2 px-4 rounded hover:bg-purple-700"
                    onClick={handleSearch}
                >
                    Search
                </button>
            </div>
            
            <div className="mt-2">
                <ul className="flex gap-2 flex-wrap">
                    {filterOptions.map((option) => (
                        <li
                            key={option.id}
                            className={`cursor-pointer px-3 py-1 rounded text-sm ${
                                activeFilter === option.id
                                    ? "bg-purple-800 text-white"
                                    : "bg-gray-200 text-gray-700 hover:bg-gray-300"
                            }`}
                            onClick={() => setActiveFilter(option.id)}
                        >
                            {option.label}
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    );
}