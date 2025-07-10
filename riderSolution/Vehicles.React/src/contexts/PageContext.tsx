import React, { useEffect, useState, type Dispatch, type ReactNode, type SetStateAction } from "react";
import { useLocation } from "react-router-dom";

interface Filters {
    brand: string;
    model: string;
    year: number | null;
    company: string;
    vehicleType: string;
    sortColumn: string;
    sortDirection: string;
}

interface ContextProps {
    currentPage: number;
    setCurrentPage: Dispatch<SetStateAction<number>>;
    filters: Filters;
    setFilters: Dispatch<SetStateAction<Filters>>;
}

export const PageContext = React.createContext<ContextProps>({
    currentPage: 0,
    setCurrentPage: () => {},
    filters: {
        brand: '',
        model: '',
        year: null,
        company: '',
        vehicleType: 'All',
        sortColumn: 'date',
        sortDirection: 'desc',
    },
    setFilters: () => {}
});

export const PageContextProvider = ({ children }: { children: ReactNode }) => {
    const [currentPage, setCurrentPage] = useState(0);
    const [filters, setFilters] = useState<Filters>({
        brand: '',
        model: '',
        year: null,
        company: '',
        vehicleType: 'All',
        sortColumn: 'date',
        sortDirection: 'desc',
    });
    const location = useLocation();

    useEffect(() => {
        if (!(location.pathname.startsWith("/allPosts") ||
              location.pathname.startsWith("/post"))) {
            setCurrentPage(0);
            setFilters({
                brand: '',
                model: '',
                year: null,
                company: '',
                vehicleType: 'All',
                sortColumn: 'date',
                sortDirection: 'desc',
            });
        }
    }, [location.pathname]);

    return (
        <PageContext.Provider value={{ currentPage, setCurrentPage, filters, setFilters }}>
            {children}
        </PageContext.Provider>
    );
};
