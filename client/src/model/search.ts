// Define the search parameters interface
export interface SearchParams {
    city?: string;
    priceMin?: string | number;
    priceMax?: string | number;
    [key: string]: string | number | undefined; // Allow for additional dynamic filter types
}

// Define the search component props
export interface SearchProps {
    /**
     * Callback function that is called when a search is performed
     * @param params The search parameters to filter accommodations
     */
    onSearch: (params: SearchParams) => void;
}