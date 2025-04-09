import { SearchProps } from "@/model/";
import { useState } from "react";
import { AccommodationType } from "../../../api/src/api";

// Define filter type with mapping to API field and operator
interface FilterOption {
    id: string;
    label: string;
    field: string;
    operator: string;
}

// Property type option interface
interface AccommodationTypeOption {
    value: AccommodationType;
    label: string;
    selected: boolean;
}

export function Search({ onSearch }: SearchProps) {
    const [searchTerm, setSearchTerm] = useState("");
    const [activeFilter, setActiveFilter] = useState<string>("city"); // Default filter type
    const [showTypeDropdown, setShowTypeDropdown] = useState(false);
    
    // Property type options with selection state - generated from AccommodationType enum
    const [propertyTypes, setPropertyTypes] = useState<AccommodationTypeOption[]>(
        Object.entries(AccommodationType).map(([key, value]) => ({
            value: value,
            label: key,
            selected: false
        }))
    );
    
    // Filter options with their corresponding API field and operator
    const filterOptions: FilterOption[] = [
        { id: "city", label: "City", field: "address.city", operator: "LIKE" },
        { id: "name", label: "Name", field: "name", operator: "LIKE" },
        { id: "type", label: "Type", field: "type", operator: "IN" },
        // { id: "priceMin", label: "Min Price", field: "price", operator: "GTE" },
        // { id: "priceMax", label: "Max Price", field: "price", operator: "LTE" }
    ];
    
    const handleSearch = () => {
        const filters = [];
        
        // Handle text search filters
        const currentSearchTerm = searchTerm.trim();
        if (currentSearchTerm) {
            // Find the active filter option
            const filterOption = filterOptions.find(option => option.id === activeFilter);
            
            if (filterOption) {
                // Build the filter string in the format: field:operator:value
                const filterString = `${filterOption.field}:${filterOption.operator}:${currentSearchTerm}`;
                filters.push(filterString);
            }
        }
        
        // Handle type multiselect filter
        const selectedTypes = propertyTypes.filter(type => type.selected).map(type => type.value);
        if (selectedTypes.length > 0) {
            const typeFilterOption = filterOptions.find(option => option.id === "type");
            if (typeFilterOption) {
                const typeFilterString = `${typeFilterOption.field}:${typeFilterOption.operator}:[${selectedTypes.join(',')}]`;
                filters.push(typeFilterString);
            }
        }
        
        // Call the onSearch callback with the properly formatted parameters
        onSearch({
            filters: filters.length > 0 ? filters.join(';') : undefined // Join multiple filters with semicolon
        });
    };
    
    const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === "Enter") {
            handleSearch();
        }
    };
    
    const toggleTypeSelection = (index: number) => {
        // Create a new array with the updated selection
        const updatedTypes = propertyTypes.map((type, i) => 
            i === index ? { ...type, selected: !type.selected } : type
        );
        
        // Update state
        setPropertyTypes(updatedTypes);
        
        // Prepare filters with the updated selection without waiting for state update
        const filters = [];
        
        // Add text search filter if present
        if (searchTerm.trim()) {
            const filterOption = filterOptions.find(option => option.id === activeFilter);
            if (filterOption) {
                const filterString = `${filterOption.field}:${filterOption.operator}:${searchTerm}`;
                filters.push(filterString);
            }
        }
        
        // Add type filter with the updated selection
        const selectedTypes = updatedTypes.filter(type => type.selected).map(type => type.value);
        if (selectedTypes.length > 0) {
            const typeFilterOption = filterOptions.find(option => option.id === "type");
            if (typeFilterOption) {
                const typeFilterString = `${typeFilterOption.field}:${typeFilterOption.operator}:[${selectedTypes.join(',')}]`;
                filters.push(typeFilterString);
            }
        }
        
        // Trigger search with the prepared filters
        onSearch({
            filters: filters.length > 0 ? filters.join(';') : undefined
        });
    };
    
    const toggleTypeDropdown = () => {
        setShowTypeDropdown(!showTypeDropdown);
    };
    
    const clearSearch = () => {
        setSearchTerm("");

        // handleSearch()
        
        // Use an empty search term directly in a new search instead of depending on state
        const filters = [];
        
        // Skip adding text search filter since we're clearing it
        
        // Only include type filters if any are selected
        const selectedTypes = propertyTypes.filter(type => type.selected).map(type => type.value);
        if (selectedTypes.length > 0) {
            const typeFilterOption = filterOptions.find(option => option.id === "type");
            if (typeFilterOption) {
                const typeFilterString = `${typeFilterOption.field}:${typeFilterOption.operator}:[${selectedTypes.join(',')}]`;
                filters.push(typeFilterString);
            }
        }
        
        // Call the onSearch callback with the properly formatted parameters
        onSearch({
            filters: filters.length > 0 ? filters.join(';') : undefined
        });
    };
    
    // const clearAllFilters = () => {
    //     setSearchTerm("");
    //     // Clear all selected property types
    //     setPropertyTypes(propertyTypes.map(type => ({ ...type, selected: false })));
    //     // Trigger search with no filters
    //     onSearch({});
    // };
    
    // Get the current filter option to use in placeholder
    const currentFilter = filterOptions.find(option => option.id === activeFilter);
    const placeholderText = currentFilter ? `Search by ${currentFilter.label}...` : "Search...";
    
    return (
        <div className="bg-white p-4 rounded-lg shadow-lg">
            <div className="flex flex-col md:flex-row gap-2">
                <div className="flex-grow relative">
                    <input
                        type="text"
                        className="w-full p-2 border border-gray-300 rounded"
                        placeholder={placeholderText}
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        onKeyDown={handleKeyPress}
                    />
                    {searchTerm && (
                        <button
                            className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-500 hover:text-gray-700"
                            onClick={clearSearch}
                            aria-label="Clear search"
                        >
                            ✕
                        </button>
                    )}
                </div>
                <button
                    className="bg-purple-800 text-white py-2 px-4 rounded hover:bg-purple-700"
                    onClick={handleSearch}
                >
                    Search
                </button>
                {/* {(searchTerm || propertyTypes.some(type => type.selected)) && (
                    <button
                        className="bg-gray-200 text-gray-700 py-2 px-4 rounded hover:bg-gray-300"
                        onClick={clearAllFilters}
                    >
                        Clear All
                    </button>
                )} */}
            </div>
            
            <div className="mt-2">
                <ul className="flex gap-2 flex-wrap">
                    {filterOptions.filter(option => option.id !== "type").map((option) => (
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
            
            {/* Type multiselect section always visible */}
            <div className="mt-3">
                <div className="flex items-center">
                    <h3 className="text-sm font-medium mr-2">Property Types:</h3>
                    <button 
                        onClick={toggleTypeDropdown}
                        className="text-purple-800 text-sm font-medium flex items-center"
                    >
                        {showTypeDropdown ? "Hide" : "Show"} options
                        <span className="ml-1">{showTypeDropdown ? "▲" : "▼"}</span>
                    </button>
                </div>
                
                {/* Type options dropdown */}
                {showTypeDropdown && (
                    <div className="mt-1 p-2 border border-gray-200 rounded bg-gray-50">
                        <ul className="grid grid-cols-2 gap-1 sm:grid-cols-3 md:grid-cols-5">
                            {propertyTypes.map((type, index) => (
                                <li 
                                    key={type.value}
                                    className="flex items-center"
                                >
                                    <label className="flex items-center hover:bg-gray-100 p-1 rounded w-full cursor-pointer">
                                        <input
                                            type="checkbox"
                                            checked={type.selected}
                                            onChange={() => toggleTypeSelection(index)}
                                            className="mr-2"
                                        />
                                        <span className="text-sm">{type.label}</span>
                                    </label>
                                </li>
                            ))}
                        </ul>
                    </div>
                )}
                
                {/* Display selected types as chips */}
                {/* {propertyTypes.some(type => type.selected) && (
                    <div className="mt-2 flex flex-wrap gap-1">
                        {propertyTypes.filter(type => type.selected).map((type, index) => (
                            <div 
                                key={index}
                                className="bg-purple-100 text-purple-800 px-2 py-1 rounded-full text-sm flex items-center"
                            >
                                <span>{type.label}</span>
                                <button 
                                    className="ml-1 text-purple-800 hover:text-purple-900 font-bold"
                                    onClick={() => {
                                        const typeIndex = propertyTypes.findIndex(t => t.value === type.value);
                                        if (typeIndex !== -1) {
                                            toggleTypeSelection(typeIndex);
                                        }
                                    }}
                                >
                                    ×
                                </button>
                            </div>
                        ))}
                    </div>
                )} */}
            </div>
        </div>
    );
}