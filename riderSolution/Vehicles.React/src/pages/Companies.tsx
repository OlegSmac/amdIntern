import { Box, Button, Stack } from "@mui/material";
import { useEffect, useState } from "react";
import type { Company } from "./Post";
import axiosInstance from "../api/axios";
import CompaniesList from "../smallComponents/CompaniesList";

const Companies = () => {
    const [companies, setCompanies] = useState<Company[]>([]);
    const [subscribeIds, setSubscribeIds] = useState<string[]>([]);
    const [currentPage, setCurrentPage] = useState(0);
    const [totalPages, setTotalPages] = useState(1);
    const pageSize = 10;

    useEffect(() => {
        const fetchPaginatedPosts = async (pageIndex: number) => {
            try {
                const requestBody = {
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    columnNameForSorting: "",
                    sortDirection: "asc",
                    requestFilters: {
                        logicalOperator: 0,
                        filters: []
                    }
                };

                const response = await axiosInstance.post("/api/companies/getPagedCompanies", requestBody);

                setCompanies(response.data.items);
                setTotalPages(Math.ceil(response.data.total / pageSize));

            } catch (error) {
                console.error("Failed to fetch paginated posts", error);
            }
        };

        fetchPaginatedPosts(currentPage);
    }, [currentPage]);

    useEffect(() => {
        const fetchSubscribeIds = async () => {
            try {
                const userId = localStorage.getItem('userId');
                const response = await axiosInstance.get(`/api/users/getUserSubscriptions/${userId}`);
                setSubscribeIds(response.data);
            } catch (error) {
                console.log("Failed to fetch user subscriptions", error);
            }
        }

        fetchSubscribeIds();
    }, []);

    const handlePrevious = () => {
        if (currentPage > 0) setCurrentPage(prev => prev - 1);
    };

    const handleNext = () => {
        if (currentPage < totalPages - 1) setCurrentPage(prev => prev + 1);
    };

    return (
        <Box sx={{ mt: 14, minHeight: '81vh' }}>
            <CompaniesList companies={companies} subscribeIds={subscribeIds} />

            <Stack direction="row" spacing={2} justifyContent="center" sx={{ mt: 8 }}>
                <Button variant="outlined" onClick={handlePrevious} disabled={currentPage === 0} sx={{ minWidth: 130 }}>
                Previous
                </Button>
                <Button variant="outlined" onClick={handleNext} disabled={currentPage >= totalPages - 1} sx={{ minWidth: 130 }}>
                Next
                </Button>
            </Stack>
            
            <Box sx={{ textAlign: 'center', mt: 1 }}>
                Page {currentPage + 1} of {totalPages}
            </Box>
        </Box>
    );
}

export default Companies;