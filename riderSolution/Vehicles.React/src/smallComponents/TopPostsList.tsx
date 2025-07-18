import React, { useEffect, useState } from 'react';
import { Box, Card, CardContent, CardMedia, Grid, Typography } from '@mui/material';
import type { Post } from '../pages/Post';
import { axiosPrivate } from '../api/axios';
import { Link } from 'react-router-dom';

interface TopPostItem {
    post: Post;
    count: number;
}

interface TopPostsProps {
    companyId: string;
}

const TopPosts = ({ companyId } : TopPostsProps) => {
    const [loading, setLoading] = useState(true);
    const [posts, setPosts] = useState<TopPostItem[]>([]);

    useEffect(() => {
        const fetchTopPosts = async () => {
            try {
                const response = await axiosPrivate.get(`/api/statistics/top3Posts/${companyId}`);
                const data = response.data;
                console.log(data);
                setPosts(data);
            } catch (error) {
                console.error('Failed to fetch top posts:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchTopPosts();
    }, [companyId]);

    if (loading || posts.length === 0) {
        return (
            <Typography sx={{ mt: 20, textAlign: "center" }}>
                No favorite posts
            </Typography>
        );
    }

    return (
        <Box display="flex" flexDirection="column" gap={2}>
            {posts.map((item) => (
                <Grid key={item.post.id}>
                    <Card
                        component={Link}
                        to={`/post/${item.post.id}`}
                        state={{ fromPage: 0 }}
                        sx={{
                            position: 'relative',
                            py: 1.5,
                            px: 1.5,
                            borderRadius: 2,
                            boxShadow: 3,
                            display: 'flex',
                            flexDirection: 'row',
                            justifyContent: 'space-between',
                            transition: 'background-color 0.3s ease',
                            '&:hover': {
                                backgroundColor: '#d1d1d1'
                            }
                        }}
                    >
                        <CardMedia
                            component="img"
                            image={item.post.images[0]?.url}
                            alt={item.post.vehicle.brand}
                            sx={{
                                width: 130,
                                height: 130,
                                objectFit: 'cover',
                                display: 'block',
                                alignItems: 'center',
                                borderRadius: 2,
                                mx: 'auto'
                            }}
                        />

                        <CardContent 
                            sx={{ 
                                display: 'flex', 
                                flexDirection: 'column', 
                                flexGrow: 1, 
                                py: 0,
                                '&:last-child': { pb: 0 },
                            }}
                        >
                            <Typography variant="h6" sx={{ color: 'text.primary' }}>
                                {item.post.vehicle.brand}
                            </Typography>

                            <Typography
                                variant="body2"
                                sx={{
                                    color: 'text.secondary',
                                    fontSize: '0.9rem',
                                    overflow: 'hidden',
                                    textOverflow: 'ellipsis',
                                    display: '-webkit-box',
                                    WebkitLineClamp: 2,
                                    WebkitBoxOrient: 'vertical',
                                }}
                            >
                                {item.post.body}
                            </Typography>

                            <Box 
                                sx={{
                                    mt: 'auto',
                                    pt: 2,
                                    display: 'flex', 
                                    flexDirection: 'row',
                                    justifyContent: 'space-between'
                                }}
                            >
                                <Typography 
                                    sx={{
                                        fontWeight: 'bold',
                                        fontSize: '20px',
                                        color: '#000000'
                                    }}
                                >
                                    {item.post.price} €
                                </Typography>

                                <Typography 
                                    sx={{
                                        color: 'red',
                                        fontSize: '16px'
                                    }}
                                >
                                    Favorites: {item.count}
                                </Typography>
                            </Box>
                        </CardContent>
                    </Card>
                </Grid>
            ))}
        </Box>
    );
};

export default TopPosts;
