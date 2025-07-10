import React, { useContext, useEffect, useState } from 'react';
import { axiosPrivate } from '../api/axios';
import type { Post } from './Post';
import PostsList from '../smallComponents/PostsList';
import { Box, Button, Stack, TextField, MenuItem, FormControl, InputLabel, Select } from '@mui/material';
import { PageContext } from '../contexts/PageContext';

const AllPosts = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const [totalPages, setTotalPages] = useState(1);
    const pageSize = 10;
    const { currentPage, setCurrentPage, filters, setFilters } = useContext(PageContext);

    const [vehicleTypes, setVehicleTypes] = useState<string[]>([]);

    const fetchPosts = async (pageIndex: number) => {
        setLoading(true);
        setError(null);

        try {
            const filtersArray = [];
            if (filters.brand) filtersArray.push({ path: 'vehicle.brand', value: filters.brand });
            if (filters.model) filtersArray.push({ path: 'vehicle.model', value: filters.model });
            if (filters.year) filtersArray.push({ path: 'vehicle.year', value: filters.year.toString()});
            if (filters.company) filtersArray.push({ path: 'company.name', value: filters.company });
            if (filters.vehicleType && filters.vehicleType !== 'All') {
                filtersArray.push({ path: 'Categories', value: filters.vehicleType });
            }
            filtersArray.push({ path: 'isHidden', value: false });

            const requestBody = {
                pageIndex: pageIndex,
                pageSize: pageSize,
                columnNameForSorting: filters.sortColumn,
                sortDirection: filters.sortDirection,
                requestFilters: {
                    logicalOperator: 0,
                    filters: filtersArray
                }
            };
            
            const response = await axiosPrivate.post('/api/posts/paginated-search', requestBody);
            
            setPosts(response.data.items);
            setTotalPages(Math.ceil(response.data.total / pageSize));
        } catch (err) {
            console.error('Error fetching posts:', err);
            setError('Failed to load posts.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts(currentPage);
    }, [currentPage]);

    useEffect(() => {
        if (loading) return;

        if (totalPages === 0) setError("There are not such posts.");
    }, [posts])

    useEffect(() => {
        const fetchVehicleTypes = async () => {
            try {
                const response = await axiosPrivate.get('/api/categories');
                setVehicleTypes(response.data);
            } catch (error) {
                console.error('Failed to fetch vehicle types:', error);
            }
        };

        fetchVehicleTypes();
    }, []);

    const handlePrevious = () => {
        if (currentPage > 0) setCurrentPage(prev => prev - 1);
    }

    const handleNext = () => {
        if (currentPage < totalPages) setCurrentPage(prev => prev + 1);
    }

    const handleApplyFilters = () => {
        setCurrentPage(0);
        fetchPosts(0);
    };

    return (
        <>
            <Box sx={{ mt: 10, py: 2, minHeight: '81vh' }}>
                <Stack direction="row" spacing={2} sx={{ mb: 3 }} justifyContent="center">
                    <TextField
                        label="Brand"
                        variant="outlined"
                        value={filters.brand}
                        onChange={e => setFilters(prev => ({...prev, brand: e.target.value}))}
                        sx={{ maxWidth: 180 }}
                    />
                    <TextField
                        label="Model"
                        variant="outlined"
                        value={filters.model}
                        onChange={e => setFilters(prev => ({...prev, model: e.target.value}))}
                        sx={{ maxWidth: 180 }}
                    />
                    <TextField
                        label="Year"
                        variant="outlined"
                        type="number"
                        value={filters.year}
                        onChange={e => {
                            const val = e.target.value;
                            const parsed = parseInt(val);
                            setFilters(prev => ({...prev, year: isNaN(parsed) ? null : parsed}));
                        }}
                        sx={{ maxWidth: 180 }}
                    />
                    <TextField
                        label="Company"
                        variant="outlined"
                        value={filters.company}
                        onChange={e => setFilters(prev => ({...prev, company: e.target.value}))}
                        sx={{ maxWidth: 190 }}
                    />
                    <FormControl>
                        <InputLabel>Vehicle Type</InputLabel>
                        <Select
                            value={filters.vehicleType}
                            label="Vehicle Type"
                            onChange={e => setFilters(prev => ({...prev, vehicleType: e.target.value}))}
                            sx={{ minWidth: 150 }}
                        >
                            <MenuItem value="All">All</MenuItem>
                            {vehicleTypes.map((type) => (
                                <MenuItem key={type} value={type}> {type} </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControl>
                        <InputLabel>Sort by</InputLabel>
                        <Select
                            value={filters.sortColumn}
                            label="Sort by"
                            onChange={e => setFilters(prev => ({...prev, sortColumn: e.target.value}))}
                            sx={{ minWidth: 150 }}
                        >
                            <MenuItem value="date">Date</MenuItem>
                            <MenuItem value="title">Title</MenuItem>
                            <MenuItem value="vehicle.brand">Brand</MenuItem>
                        </Select>
                    </FormControl>
                    <FormControl>
                        <InputLabel>Direction</InputLabel>
                        <Select
                            value={filters.sortDirection}
                            label="Direction"
                            onChange={e => setFilters(prev => ({...prev, sortDirection: e.target.value}))}
                            sx={{ minWidth: 150 }}
                        >
                            <MenuItem value="asc">Ascending</MenuItem>
                            <MenuItem value="desc">Descending</MenuItem>
                        </Select>
                    </FormControl>
                    <Button variant="contained" onClick={handleApplyFilters} sx={{ minWidth: 120, fontSize: '0.97rem' }}>
                        Apply
                    </Button>
                </Stack>

                {loading && (
                    <Box sx={{ textAlign: 'center', py: 4, fontSize: '1.2rem' }}>Loading...</Box>
                )}

                {!loading && error && (
                    <Box sx={{ textAlign: 'center', py: 4, color: 'red', fontSize: '1.2rem' }}>{error}</Box>
                )}

                <PostsList posts={posts} titleSection={'All Posts'} currentPage={currentPage} />

                {!loading && !error && (
                <>
                    <Stack direction="row" spacing={2} justifyContent="center" sx={{ mt: 7 }}>
                        <Button variant="outlined" onClick={handlePrevious} disabled={currentPage === 0} sx={{minWidth: 130}}>
                            Previous
                        </Button>
                        <Button variant="outlined" onClick={handleNext} disabled={currentPage >= totalPages - 1} sx={{minWidth: 130}}>
                            Next
                        </Button>
                    </Stack>
                    <Box sx={{ textAlign: 'center', mt: 1 }}>
                        Page {currentPage + 1} of {totalPages}
                    </Box>
                </>
                )}
            </Box>
        </>
    );
};

export default AllPosts;
