import { useEffect, useState } from "react";
import type { Post } from "./Post";
import axiosInstance from "../api/axios";
import { Box, Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Divider, Paper, Stack, Typography } from "@mui/material";
import VisibilityIcon from '@mui/icons-material/Visibility';
import DeleteIcon from '@mui/icons-material/Delete';
import { Link } from "react-router-dom";


const FavoritePosts = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [openConfirm, setOpenConfirm] = useState(false);
    const [postToDelete, setPostToDelete] = useState<number | null>(null);

    const [currentPage, setCurrentPage] = useState(0);
    const [totalPages, setTotalPages] = useState(1);
    const pageSize = 8;

    const userId = localStorage.getItem('userId');

    useEffect(() => {
        const fetchPaginatedPosts = async (pageIndex: number) => {
            try {
            const response = await axiosInstance.post("/api/posts/paginated-search", {
                pageIndex,
                pageSize,
                columnNameForSorting: "date",
                sortDirection: "desc",
                requestFilters: {
                    logicalOperator: 0,
                    filters: [{ path: "FavoritePosts.UserId", value: userId }],
                },
            });

            setPosts(response.data.items);
            setTotalPages(Math.ceil(response.data.total / pageSize));

            } catch (error) {
            console.error("Failed to fetch paginated posts", error);
            }
        };

        fetchPaginatedPosts(currentPage);
    }, [currentPage]);

    const handlePrevious = () => {
        if (currentPage > 0) setCurrentPage(prev => prev - 1);
    };

    const handleNext = () => {
        if (currentPage < totalPages - 1) setCurrentPage(prev => prev + 1);
    };

    const handleDeleteClick = (postId: number) => {
        setPostToDelete(postId);
        setOpenConfirm(true);
    };

    const handleConfirmDelete = async () => {
        if (!postToDelete) return;

        try {
            await axiosInstance.delete(`/api/accounts/removePostFromFavorite/${userId}/${postToDelete}`);
            
            setPosts(prevList => prevList.filter(post => post.id !== postToDelete));
        } catch (error) {
            console.error('Delete failed:', error);
        } finally {
            setOpenConfirm(false);
            setPostToDelete(null);
        }
    };

    return (
        <Box sx={{ mt: 14, minHeight: '81vh' }}>
            
            <Box mt={4} mx={40}>
                <Paper sx={{ p: 2, mb: 2, backgroundColor: '#f1f1f1' }}>
                    <Box display="grid" gridTemplateColumns="1fr 2fr 1.5fr 1fr 1fr 1fr 1.2fr" alignItems="center" gap={2}>
                    <Typography variant="subtitle2">Image</Typography>
                    <Typography variant="subtitle2">Title</Typography>
                    <Typography variant="subtitle2">Brand</Typography>
                    <Typography variant="subtitle2">Model</Typography>
                    <Typography variant="subtitle2">Year</Typography>
                    <Typography variant="subtitle2">Price</Typography>
                    </Box>
                </Paper>

                {posts.map((post) => (
                    <Paper key={post.id} sx={{ px: 2, py: 1, mb: 1 }}>
                        <Box display="grid" gridTemplateColumns="1fr 2fr 1.5fr 1fr 1fr 1fr 1.2fr" alignItems="center" gap={2}>
                            <Box>
                                <img
                                    src={post.images?.[0]?.url || "/placeholder.jpg"}
                                    alt={post.title}
                                    style={{ width: '100%', maxWidth: 100, height: 70, objectFit: 'cover', borderRadius: 4 }}
                                />
                            </Box>

                            <Typography noWrap>{post.title}</Typography>
                            <Typography>{post.vehicle?.brand || '-'}</Typography>
                            <Typography>{post.vehicle?.model || '-'}</Typography>
                            <Typography>{post.vehicle?.year || '-'}</Typography>
                            <Typography>{(post.price + ' €') || '-'}</Typography>

                            <Box display="flex" gap={1}>
                                <Button 
                                    component={Link} 
                                    to={`/post/${post.id}`} 
                                    variant="outlined" 
                                    color="info"
                                >
                                    <VisibilityIcon />
                                </Button>
                                <Button 
                                    variant="outlined" 
                                    color="error" 
                                    onClick={() => handleDeleteClick(post.id)}
                                >
                                    <DeleteIcon />
                                </Button>
                            </Box>
                        </Box>
                    </Paper>
                ))}

                {posts.length === 0 && (
                    <>
                    <Divider sx={{ my: 2 }} />
                    <Typography variant="body2" color="text.secondary">
                        No posts found.
                    </Typography>
                    </>
                )}

                <Dialog open={openConfirm} onClose={() => setOpenConfirm(false)}>
                    <DialogTitle>Confirm Delete</DialogTitle>
                    <DialogContent>
                        <DialogContentText>
                            Are you sure you want to delete this post from favorite list? This action cannot be undone.
                        </DialogContentText>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={() => setOpenConfirm(false)}>Cancel</Button>
                        <Button onClick={handleConfirmDelete} variant="contained" color="error">
                            Delete
                        </Button>
                    </DialogActions>
                </Dialog>
            </Box>

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

export default FavoritePosts;