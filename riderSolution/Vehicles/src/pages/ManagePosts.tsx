import { useEffect, useState } from 'react';
import { Button, Box, Stack } from '@mui/material';
import { Link } from 'react-router-dom';
import type { Post } from './Post';
import PostsListManage from '../smallComponents/PostsListManage';
import { useAuth } from '../contexts/AuthContext';
import axios from '../api/axios';

const ManagePosts = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [currentPage, setCurrentPage] = useState(0);
    const [totalPages, setTotalPages] = useState(1);
    const pageSize = 8;

    const userId = localStorage.getItem('userId');

    const { isAuthenticated, logout } = useAuth();
    const [role, setRole] = useState<string | null>(null);
    
    useEffect(() => {
        const fetchRole = async () => {
            if (isAuthenticated && localStorage.getItem('authToken')) {
                try {
                    const roleResponse = await axios.post('/api/accounts/getRoleFromToken', {
                        token: localStorage.getItem('authToken'),
                    });
                    
                    if (roleResponse.data != null) setRole(roleResponse.data);
                    else logout();
                } catch (error) {
                    console.error("Error fetching role:", error);
                }
            } else {
                setRole(null);
            }
        };

        fetchRole();
    }, [isAuthenticated]);

    useEffect(() => {
        const fetchPaginatedPosts = async (pageIndex: number) => {
            if (!role) return;

            try {
                const filters = [];
                if (role === "Company") await filters.push({ path: "companyId", value: userId });

                const response = await axios.post("/api/posts/paginated-search", {
                    pageIndex,
                    pageSize,
                    columnNameForSorting: "date",
                    sortDirection: "desc",
                    requestFilters: {
                        logicalOperator: 0,
                        filters: filters,
                    },
                });

                setPosts(response.data.items);
                setTotalPages(Math.ceil(response.data.total / pageSize));

            } catch (error) {
                console.error("Failed to fetch paginated posts", error);
            }
        };

        fetchPaginatedPosts(currentPage);
    }, [role, currentPage]);

    const handlePrevious = () => {
        if (currentPage > 0) setCurrentPage(prev => prev - 1);
    };

    const handleNext = () => {
        if (currentPage < totalPages - 1) setCurrentPage(prev => prev + 1);
    };

    return (
        <Box sx={{ mt: 10, minHeight: '85vh' }}>
            {role !== "Admin" && (
                <Button 
                    component={Link} 
                    to="/createPost"
                    variant="contained"
                    sx={{ minWidth: 130, mt: 4, ml: 35 }}
                >
                    Create New Post
                </Button>
            )}

            <PostsListManage posts={posts} />

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
};

export default ManagePosts;
