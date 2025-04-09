// Define the search parameters interface
export interface SearchParams {
    filters?: string;
}

// Define the search component props
export interface SearchProps {
    /**
     * Callback function that is called when a search is performed
     * @param params The search parameters to filter accommodations
     */
    onSearch: (params: SearchParams) => void;
}