import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Box, Button, Typography, Paper, Divider, Avatar, TextField } from '@mui/material';
import { useAuth } from '../contexts/AuthContext';
import axios from '../api/axios';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

const User = () => {
    const { logout } = useAuth();
    const [userData, setUserData] = useState<any>(null);
    const [loading, setLoading] = useState(true);
    const [editMode, setEditMode] = useState(false);
    const [role, setRole] = useState('');

    const [form, setForm] = useState({
        firstName: '',
        lastName: '',
        phone: '',
        companyName: '',
        description: '',
        currentPassword: '',
        newPassword: '',
    });

    const userId = localStorage.getItem('userId');
    const username = localStorage.getItem('userName');

    useEffect(() => {
        const fetchUserData = async () => {
            if (!userId) return;

            try {
                const userResponse = await axios.get(`/api/application-users/${userId}`);
                setUserData(userResponse.data);

                setForm({
                    firstName: userResponse.data.firstName || '',
                    lastName: userResponse.data.lastName || '',
                    phone: userResponse.data.phone || '',
                    companyName: userResponse.data.name || '',
                    description: userResponse.data.description || '',
                    currentPassword: '',
                    newPassword: '',
                });

                const roleResponse = await axios.post('/api/application-users/getRoleFromToken', {
                    token: localStorage.getItem('authToken'),
                });
                setRole(roleResponse.data);

            } catch (error) {
                console.error('Failed to fetch user data:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchUserData();
    }, [userId]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const handleSave = async () => {
        try {
            const response = await axios.put('/api/application-users/update', {
                id: userId,
                firstName: form.firstName,
                lastName: form.lastName,
                phone: form.phone,
                companyName: form.companyName,
                description: form.description,
                currentPassword: form.currentPassword,
                newPassword: form.newPassword
            });

            console.log(response);

            setEditMode(false);
            window.location.reload();
            
        } catch (error) {
            console.error("Update failed:", error);
            alert("Update failed. Make sure current password is correct.");
        }
    };

    if (loading) {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                Loading...
            </div>
        );
    }

    return (
        <Box sx={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
        <Box sx={{ p: 5, mt: 20, width: 600, mx: 'auto', flexGrow: 1 }}>
            <Paper elevation={5} sx={{ p: 4, borderRadius: 3 }}>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 3 }}>
                    <Avatar sx={{ bgcolor: 'primary.main', width: 56, height: 56, mr: 2 }}>
                        <AccountCircleIcon sx={{ fontSize: 32 }} />
                    </Avatar>
                    <Box>
                        <Typography variant="h5" fontWeight="bold">
                            {username}
                        </Typography>
                        <Typography variant="subtitle2" color="text.secondary">
                            {role}
                        </Typography>
                    </Box>
                </Box>

                <Divider sx={{ mb: 3 }} />

                {!editMode ? (
                    <Box sx={{ lineHeight: 2.5 }}>
                        {role === 'Company' ? (
                            <>
                                <Typography mb={2}><strong>Company Name:</strong> {userData.name}</Typography>
                                <Typography mb={2}><strong>Email:</strong> {userData.email}</Typography>
                                <Typography mb={2}><strong>Phone:</strong> {userData.phone}</Typography>
                                <Typography mb={2}><strong>Description:</strong> {userData.description}</Typography>
                            </>
                        ) : (
                            <>
                                <Typography mb={2}><strong>First Name:</strong> {userData.firstName}</Typography>
                                <Typography mb={2}><strong>Last Name:</strong> {userData.lastName}</Typography>
                                <Typography mb={2}><strong>Email:</strong> {userData.email}</Typography>
                                <Typography mb={2}><strong>Phone:</strong> {userData.phone}</Typography>
                            </>
                        )}
                        <Button sx={{ mt: 3 }} fullWidth variant="outlined" onClick={() => setEditMode(true)}>
                            Change Profile
                        </Button>
                    </Box>
                ) : (
                    <Box component="form" sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                        {role === 'Company' ? (
                            <>
                                <TextField label="Company Name" name="companyName" value={form.companyName} onChange={handleChange} />
                                <TextField label="Description" name="description" value={form.description} onChange={handleChange} multiline minRows={3} />
                            </>
                        ) : (
                            <>
                                <TextField label="First Name" name="firstName" value={form.firstName} onChange={handleChange} />
                                <TextField label="Last Name" name="lastName" value={form.lastName} onChange={handleChange} />
                            </>
                        )}
                        <TextField label="Phone" name="phone" value={form.phone} onChange={handleChange} />
                        <TextField label="Current Password" name="currentPassword" type="password" value={form.currentPassword} onChange={handleChange} />
                        <TextField label="New Password" name="newPassword" type="password" value={form.newPassword} onChange={handleChange} />

                        <Box sx={{ display: 'flex', gap: 2, mt: 2 }}>
                            <Button fullWidth variant="outlined" onClick={() => setEditMode(false)}>
                                Cancel
                            </Button>
                            <Button fullWidth variant="contained" color="primary" onClick={handleSave}>
                                Save Changes
                            </Button>
                        </Box>
                    </Box>
                )}

                <Divider sx={{ my: 3 }} />

                <Button
                    component={Link} 
                    to="/"
                    variant="contained"
                    color="error"
                    fullWidth
                    onClick={logout}
                >
                    Logout
                </Button>
            </Paper>
        </Box>
        </Box>
    );
};

export default User;
