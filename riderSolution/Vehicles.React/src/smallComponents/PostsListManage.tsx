import {
  Box,
  Button,
  Divider,
  Paper,
  Typography,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions
} from '@mui/material';
import { useEffect, useState, type FC } from 'react';
import type { Post } from '../pages/Post';
import VisibilityIcon from '@mui/icons-material/Visibility';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { Link } from 'react-router-dom';
import axiosInstance, { axiosPrivate } from '../api/axios';

interface PostsListProps {
    posts: Post[];
}

const PostsListManage: FC<PostsListProps> = ({ posts }) => {
    const [postList, setPostList] = useState<Post[]>(posts);
    const [openConfirm, setOpenConfirm] = useState(false);
    const [postToDelete, setPostToDelete] = useState<number | null>(null);

    useEffect(() => {
        setPostList(posts);
    }, [posts]);

    const handleDeleteClick = (postId: number) => {
        setPostToDelete(postId);
        setOpenConfirm(true);
    };

    const handleConfirmDelete = async () => {
        if (!postToDelete) return;

        try {
            await axiosPrivate.delete(`/api/posts/${postToDelete}`);
            
            setPostList(prevList => prevList.filter(post => post.id !== postToDelete));
        } catch (error) {
            console.error('Delete failed:', error);
        } finally {
            setOpenConfirm(false);
            setPostToDelete(null);
        }
    };

    const handleToggleHidden = async (postId: number, currentHidden: boolean) => {
        try {
            await axiosPrivate.post(`/api/posts/${postId}`, JSON.stringify(!currentHidden), {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            setPostList((prevList) =>
                prevList.map((post) => 
                    post.id === postId ? {...post, isHidden: !currentHidden } : post
                )
            );
        } catch (error) {
            console.error('Failed to toggle hidden state:', error);
        }
    };

    return (
        <Box mt={4} mx={35}>
            <Paper sx={{ p: 2, mb: 2, backgroundColor: '#f1f1f1' }}>
                <Box display="grid" gridTemplateColumns="1fr 2fr 1.5fr 1fr 1fr 1fr 2.5fr" alignItems="center" gap={2}>
                <Typography variant="subtitle2">Image</Typography>
                <Typography variant="subtitle2">Title</Typography>
                <Typography variant="subtitle2">Brand</Typography>
                <Typography variant="subtitle2">Model</Typography>
                <Typography variant="subtitle2">Year</Typography>
                <Typography variant="subtitle2">Price</Typography>
                </Box>
            </Paper>

            {postList.map((post) => (
                <Paper key={post.id} sx={{ px: 2, py: 1, mb: 1 }}>
                    <Box display="grid" gridTemplateColumns="1fr 2fr 1.5fr 1fr 1fr 1fr 2.5fr" alignItems="center" gap={2}>
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
                                variant="outlined"
                                color={post.isHidden ? "success" : "warning"}
                                onClick={() => handleToggleHidden(post.id, post.isHidden)}
                                sx={{width: 70}}
                            >
                                {post.isHidden ? "Show" : "Hide"}
                            </Button>

                            <Button 
                                component={Link} 
                                to={`/post/${post.id}`} 
                                variant="outlined" 
                                color="info"
                            >
                                <VisibilityIcon />
                            </Button>
                            <Button 
                                component={Link} 
                                to={`/updatePost/${post.id}`} 
                                variant="outlined" 
                                color="primary"
                            >
                                <EditIcon />
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

            {postList.length === 0 && (
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
                        Are you sure you want to delete this post? This action cannot be undone.
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
    );
};

export default PostsListManage;
