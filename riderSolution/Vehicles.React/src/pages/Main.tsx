import React, { useContext, useEffect, useState } from 'react'
import { Box, Button, Typography } from '@mui/material';
import axiosInstance from '../api/axios';
import type { Post } from '../pages/Post';
import PostsList from '../smallComponents/PostsList';
import { Link } from 'react-router-dom';

const Main = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        setLoading(true);

        const fetchPaginatedPosts = async () => {
            try {
                const response = await axiosInstance.post('/api/posts/paginated-search', {
                    pageIndex: 0,
                    pageSize: 5,
                    columnNameForSorting: 'date',
                    sortDirection: 'desc',
                    requestFilters: {
                        logicalOperator: 0,
                        filters: [{ path: 'isHidden', value: false }]
                    }
                });

                setPosts(response.data.items);
            } catch (error) {
                console.error('Failed to fetch paginated posts', error);
            }  finally {
                setLoading(false);
            }
        };

        fetchPaginatedPosts();
    }, [])

    useEffect(() => {
        if (loading) return;
        
        if (posts.length === 0) setError("Failed to load latest posts.");
        else setError(null);
    }, [posts])
    
    return (
        <Box
            component="main"
            sx={{ pt: "60px" }}
        >
            <Box
                className="description"
                sx={{
                    position: 'relative',
                    width: '100%',
                    height: 360,
                    overflow: 'auto',
                }}
            >
                <Box
                    sx={{
                        position: 'absolute',
                        top: '14%',
                        left: '22%',
                        right: '20%',
                        bottom: '15%',
                        bgcolor: 'rgba(255, 255, 255, 0.85)',
                        display: 'flex',
                        justifyContent: 'center',
                        borderRadius: 2,
                        boxShadow: '0 10px 10px rgba(0, 0, 0, 0.5)',
                        p: 2.5,
                        textAlign: 'center',
                        zIndex: 2,
                    }}
                >
                    <Box
                        component="img"
                        src="images/logo_short.png"
                        alt="Logo"
                        sx={{
                            mr: 6.25,
                            width: 290,
                            height: 'auto',
                        }}
                    />

                    <Box
                        sx={{
                            mr: 2.5,
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'flex-start',
                        }}
                    >
                        <Typography
                            variant="h1"
                            sx={{
                                color: 'text.primary',
                                fontSize: '2.3rem',
                                fontWeight: 'bold',
                                mt: '14px',
                            }}
                        >
                            Welcome to the vehicles dealership!
                        </Typography>
                        
                        <Typography
                            variant="h2"
                            sx={{
                                color: 'text.secondary',
                                fontSize: '1.5rem',
                                fontStyle: 'italic',
                            }}
                        >
                            Choose your favorite vehicle
                        </Typography>

                        <Box
                            sx={{
                                mt: 8
                            }}
                        >
                            <Button 
                                component={Link} 
                                to="/allPosts"
                                variant="contained" 
                                color="primary" 
                                sx={{ minWidth: 148, height: 44 }}
                            >
                                All Posts
                            </Button>

                            {localStorage.getItem('userId') == null &&
                                <Button 
                                    component={Link} 
                                    to="/login"
                                    variant="outlined" 
                                    color="info" 
                                    sx={{ ml: 3, minWidth: 148, height: 44, background: 'white', '&:hover': {backgroundColor: '#b3bdc6'}
                                    }}
                                >
                                    Login
                                </Button>
                            }
                        </Box>
                    </Box>
                </Box>

                <Box
                    component="img"
                    id="banner"
                    src="images/banner2.jpg"
                    alt="Cars Banner"
                    sx={{
                        width: '100%',
                        height: '100%',
                        objectFit: 'cover',
                        display: 'block',
                    }}
                />

            </Box>

            <PostsList posts={posts} titleSection={'Latest Posts'} currentPage={0}/>

            {loading && (
                <Box sx={{ textAlign: 'center', py: 4, fontSize: '1.2rem' }}>Loading...</Box>
            )}

            {(!loading && error) && (
                <Box sx={{ textAlign: 'center', py: 4, color: 'red', fontSize: '1.2rem' }}>{error}</Box>
            )}
            
        </Box>
    );
}

export default Main;