import { useEffect, useState } from 'react'
import FullVehicleData, { type Vehicle } from '../smallComponents/FullVehicleData';
import axios, { axiosPrivate } from '../api/axios';
import ImageGallery from '../smallComponents/ImageGallery';
import type { Image } from '../smallComponents/ImageGallery';
import { useParams } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import SendEmailForm from '../smallComponents/SendEmailForm';
import { Button } from '@mui/material';

export type Company = {
    id: string;
    name: string;
    description: string;
    email: string;
    phone: string;
}

export type Post = {
    id: number;
    title: string;
    body: string;
    date: string;
    price: number;
    views: number;
    isHidden: boolean;
    images: Image[];
    company: Company;
    vehicle: Vehicle;
}

const Post = () => {
    const [favoriteCounter, setFavoriteCounter] = useState(0);
    const { id } = useParams<{ id: string }>();
    const [post, setPost] = useState<Post | null>(null);

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const { isAuthenticated } = useAuth();
    const userId = localStorage.getItem('userId');
    const [userEmail, setUserEmail] = useState<string>("");
    const [role, setRole] = useState<string | null>(null);
    const [isFavorite, setIsFavorite] = useState<boolean>(false);

    const [showEmailForm, setShowEmailForm] = useState(false);

    useEffect(() => {
        const fetchRole = async () => {
            if (isAuthenticated) {
                try {
                    const roleResponse = await axios.post('/api/accounts/getRoleFromToken', {
                        token: localStorage.getItem('authToken'),
                    });
                    setRole(roleResponse.data);
                    
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
        const fetchPost = async () => {
            try {
                const response = await axios.get(`/api/posts/${id}`);
                setPost(response.data);
            } catch (err) {
                console.error('Error fetching vehicle:', err);
                setError('Failed to load vehicle.');
            } finally {
                setLoading(false);
            }
        };

        fetchPost();
    }, []);

    useEffect(() => {
        const checkFavoriteStatus = async () => {
        if (isAuthenticated && userId && id) {
            try {
                const response = await axios.get(`/api/accounts/isPostFavorite/${userId}/${id}`);
                setIsFavorite(response.data);
            } catch (error) {
                console.error('Failed to check favorite status:', error);
            setIsFavorite(false);
            }
        }
        };
        checkFavoriteStatus();
    }, [isAuthenticated, userId, id]);

    useEffect(() => {
        const fetchFavoriteCount = async () => {
        if (id) {
            try {
                const response = await axios.get(`/api/posts/favoriteCount/${id}`);
                setFavoriteCounter(response.data);
            } catch (error) {
                console.error('Failed to fetch favorite count:', error);
            }
        }
        };
        fetchFavoriteCount();
    }, [role, id]);

    const handleAddFavorite = async () => {
        if (!userId || !id) return;
        try {
            await axios.post(`/api/accounts/addPostToFavorite/${userId}/${id}`);
            setIsFavorite(true);
        } catch (error) {
            console.error('Failed to add to favorite:', error);
        }
    };

    const handleRemoveFavorite = async () => {
        if (!userId || !id) return;
        try {
            await axios.delete(`/api/accounts/removePostFromFavorite/${userId}/${id}`);
            setIsFavorite(false);
        } catch (error) {
            console.error('Failed to remove from favorite:', error);
        }
    };

    useEffect(() => {
        const fetchUserEmail = async () => {
            if (userId) {
                try {
                    const response = await axios.get(`/api/accounts/${userId}`);
                    setUserEmail(response.data.email);
                } catch (error) {
                    console.error('Failed to load user email:', error);
                }
            }
        }

        fetchUserEmail();
    }, [userId])

    if (loading) {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                Loading...
            </div>
        );
    }

    if (error || !post || !post.vehicle) {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                {error || 'Vehicle not found.'}
            </div>
        );
    }

    return (
    <>
        <section className="post-page">
            <div className="publish-price-line">
                <p className="published-date">Published: {new Date(post.date).toLocaleDateString()}</p>
                <div className="price-box">{post.price} €</div>
            </div>

            <h1 className="vehicle-name">{post.title}</h1>
            <hr />

            <div className="post-content">
                <div style={{ minWidth: '1200px' }} >
                    <h2>Description</h2>
                    <p className="description"> 
                        {post.body}
                    </p>
                </div>

                <div className="left-part">
                    <FullVehicleData vehicle={post.vehicle} />

                    <div className="company-info">
                        <h2>Company Information</h2>
                        <p><strong>Name:</strong> {post.company.name}</p>
                        <p><strong>Phone:</strong> {post.company.phone}</p>
                        <p><strong>Email:</strong> {post.company.email}</p>
                    </div>

                    {role === "User" && (
                        <>
                            <Button 
                                onClick={() => setShowEmailForm(true)}
                                variant="contained" 
                                color="primary" 
                                sx={{ mt: 1, minWidth: 160 }}
                            >
                                Send Email
                            </Button>

                            {showEmailForm && (
                                <SendEmailForm
                                    from={userEmail}
                                    to={post.company.email}
                                    onClose={() => setShowEmailForm(false)}
                                />
                            )}
                        </>
                    )}

                </div>

                <div className="right-part">
                    <ImageGallery images={post.images} />

                    {role === "User" ? (
                        <div className="button-group" style={{ marginTop: '40px' }}>
                            {!isFavorite ? (
                                <button id="addToFavorite" onClick={handleAddFavorite}>Add to favorite</button>
                            ) : (
                                <button id="removeFromFavorite" onClick={handleRemoveFavorite}>Remove from favorite</button>
                            )}
                        </div>
                    ) : (
                        <p style={{ marginTop: '70px' }}>This vehicle was added to the favorite list by {favoriteCounter} users.</p>
                    )}
                </div>

            </div>
        </section>
    </>
    );
}

export default Post;