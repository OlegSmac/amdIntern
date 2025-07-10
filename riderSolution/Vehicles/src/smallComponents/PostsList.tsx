import React, { type FC } from 'react';
import { Box, Typography, Card, CardMedia, CardContent, Button } from '@mui/material';
import Grid from '@mui/material/Grid';
import { Link } from 'react-router-dom';
import type { Post } from '../pages/Post';

interface PostsListProps {
    posts: Post[];
    titleSection?: string;
    currentPage: number;  
}

const PostsList: FC<PostsListProps> = ({ posts, titleSection = "Vehicles", currentPage }) => {
    return (
        <Box maxWidth="1700px" mx="auto" px={1}>
            <Typography variant="h5" sx={{ mt: 2, ml: 16, mb: 1, color: 'text.primary', fontSize: '1.5rem'}}>
                {titleSection}
            </Typography>

            <Grid container spacing={3} justifyContent="center" wrap="wrap">
                {posts.map((post) => (
                    <Grid key={post.id} sx={{width: 270}}>
                        <Card
                            component={Link}
                            to={`/post/${post.id}`}
                            state={{ fromPage: currentPage }}
                            sx={{
                                position: 'relative',
                                pt: 2.4,
                                px: 2,
                                pb: 0,
                                borderRadius: 2,
                                height: 390,
                                boxShadow: 3,
                                display: 'flex',
                                flexDirection: 'column',
                                justifyContent: 'space-between',
                                transition: 'background-color 0.3s ease',
                                '&:hover': {
                                    backgroundColor: '#d1d1d1'
                                }
                            }}
                        >
                            <Typography
                                sx={{
                                    position: 'absolute',
                                    top: 19,
                                    right: 20,
                                    fontSize: '0.85rem',
                                    color: '#888',
                                    backgroundColor: 'rgba(255,255,255,0.9)',
                                    px: '8px',
                                    borderRadius: 1,
                                    zIndex: 1,
                                }}
                            >
                                {new Date(post.date).toLocaleDateString()}
                            </Typography>

                            <CardMedia
                                component="img"
                                image={post.images[0]?.url}
                                alt={post.vehicle.brand}
                                sx={{
                                    width: 230,
                                    height: 230,
                                    objectFit: 'cover',
                                    display: 'block',
                                    alignItems: 'center',
                                    borderRadius: 2,
                                    mx: 'auto'
                                }}
                            />

                            <CardContent sx={{ flexGrow: 1, textAlign: 'center', display: 'flex', flexDirection: 'column', pb: 0 }}>
                                <Typography variant="h6" sx={{ color: 'text.primary' }}>
                                    {post.vehicle.brand}
                                </Typography>

                                <Typography
                                    variant="body2"
                                    sx={{
                                        textAlign: 'left',
                                        color: 'text.secondary',
                                        fontSize: '0.9rem',
                                        mb: 1,
                                        overflow: 'hidden',
                                        textOverflow: 'ellipsis',
                                        display: '-webkit-box',
                                        WebkitLineClamp: 2,
                                        WebkitBoxOrient: 'vertical',
                                    }}
                                >
                                    {post.body}
                                </Typography>

                                <Typography 
                                    sx={{ 
                                        mt: 'auto', 
                                        alignSelf: 'flex-start',
                                        fontWeight: 'bold',
                                        fontSize: '20px',
                                        color: '#000000'
                                    }}
                                >
                                    {post.price} €
                                </Typography>
                            </CardContent>
                        </Card>
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
};

export default PostsList;
