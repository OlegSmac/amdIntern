import React, { useEffect, useState, type SyntheticEvent } from 'react'
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { Box, Button, Typography } from '@mui/material';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import NotificationsIcon from '@mui/icons-material/Notifications';
import Badge from '@mui/material/Badge';
import { useAuth } from '../contexts/AuthContext';
import axios from '../api/axios';

const Navbar = () => {
    const { isAuthenticated, username, logout } = useAuth();
    const [role, setRole] = useState<string | null>(null);

    useEffect(() => {
        const fetchRole = async () => {
            if (isAuthenticated && localStorage.getItem('authToken')) {
                try {
                    const roleResponse = await axios.post('/api/application-users/getRoleFromToken', {
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
    
    const navigate = useNavigate();
    const location = useLocation();

    const getTabValue = () => {
        if (location.pathname === '/allPosts') return 1;
        if (location.pathname === '/managePosts' && (role === "Company" || role === "Admin")) return 2;
        if (location.pathname === '/favoritePosts' && role == "User") return 3;
        if (location.pathname === '/addModel' && role === "Company") return 4;
        if (location.pathname === '/companies' && role === "User") return 5;
        return 0;
    };

    const [tabValue, setTabValue] = useState<number | false>(getTabValue());

    useEffect(() => {
        setTabValue(getTabValue());
    }, [location.pathname]);

    const handleChangeTab = (event: SyntheticEvent, newValue: number) => {
        setTabValue(newValue);
        
        if (newValue === 0) return;
        else if (newValue === 1) navigate('/allPosts');
        else if (newValue === 2) navigate('/managePosts');
        else if (newValue === 3) navigate('/favoritePosts');
        else if (newValue === 4) navigate('/addModel');
        else if (newValue === 5) navigate('/companies');
    }

    const [unreadCount, setUnreadCount] = useState(0);

    useEffect(() => {
        if (!role) return;

        const fetchUnreadCount = async () => {
            const filters = [];
            filters.push({ path: "isRead", value: false });
            if (role === "User") filters.push({ path: "userId", value: localStorage.getItem("userId") });
            if (role === "Company") filters.push({ path: "companyId", value: localStorage.getItem("userId") });
            if (role === "Admin") filters.push({ path: "adminId", value: localStorage.getItem("userId") });

            try {
                const requestBody = {
                    pageIndex: 0,
                    pageSize: 100,
                    columnNameForSorting: "createdAt",
                    sortDirection: "desc",
                    requestFilters: {
                        logicalOperator: 0,
                        filters: filters
                    }
                };

                const response = await axios.post(`/api/notifications/getPagedNotifications/${role}`, requestBody);
                setUnreadCount(response.data.items.length);
            } catch (error) {
                console.error("Failed to fetch unread notifications count", error);
            }
        };

        fetchUnreadCount();
    }, [role, location.pathname]);

    return (
    <>
        <nav className="navbar" style={{ position: 'fixed', top: 0, width: '100%', zIndex: 1000 }}>
            <Box
                sx={{
                    backgroundColor: 'background.paper',
                    display: 'flex',
                    justifyContent: 'space-between',
                    alignItems: 'center',
                    paddingLeft: 12,
                    paddingRight: 9,
                    py: 1.5,
                    boxShadow: 3,
                    marginX: 'auto'
                }}
            >
                <Box className="left-group" sx={{ display: 'flex', alignItems: 'center' }}>
                    <Typography
                        className="logo"
                        onClick={() => navigate('/')}
                        sx={{
                            cursor: 'pointer',
                            fontWeight: 'bold',
                            fontSize: '1.5rem',
                            color: 'text.primary',
                            textDecoration: 'none',
                        }}
                    >
                        Vehicles Dealership
                    </Typography>

                    <Tabs
                        value={tabValue}
                        onChange={handleChangeTab}
                        aria-label="tabs"
                        sx={{ ml: 5 }}
                    >
                        <Tab label="Hidden Tab" value={0} sx={{ display: 'none' }} />
                        <Tab label="All Posts" value={1} disabled={tabValue === 1} />
                        {role === "Company" || role === "Admin" ? <Tab label="Manage Posts" value={2} disabled={tabValue === 2} /> : null}
                        {role === "User" ? <Tab label="Favorite Posts" value={3} disabled={tabValue === 3} /> : null}
                        {role === "Company" ? <Tab label="Add Model" value={4} disabled={tabValue === 4} /> : null}
                        {role === "User" ? <Tab label="Companies" value={5} disabled={tabValue === 5} /> : null}
                    </Tabs>
                </Box>

                <Box className="auth-buttons" sx={{ display: 'flex', alignItems: 'center' }}>
                {!isAuthenticated ? (
                    <>
                    <Button 
                        component={Link} 
                        to="/register" 
                        variant="contained" 
                        color="primary" 
                        sx={{ mr: 3, minWidth: 120 }}
                    >
                        Register
                    </Button>
                    <Button 
                        component={Link} 
                        to="/login"
                        variant="contained" 
                        color="primary" 
                        sx={{ mr: 3, minWidth: 120 }}
                    >
                        Login
                    </Button>
                    </>
                ) : (
                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 2, mr: 8 }}>
                        <Box
                            component={Link}
                            to="/notifications"
                            sx={{
                                display: 'flex',
                                alignItems: 'center',
                                p: 1,
                                borderRadius: 1,
                                textDecoration: 'none',
                                cursor: 'pointer',
                                '&:hover': {
                                bgcolor: 'primary.light',
                                }
                            }}
                        >
                            <Badge badgeContent={unreadCount} color="error">
                                <NotificationsIcon sx={{ fontSize: 28, color: 'primary.main' }} />
                            </Badge>

                        </Box>

                        <Box
                            component={Link}
                            to="/user"
                            sx={{
                                display: 'flex',
                                alignItems: 'center',
                                p: 1,
                                borderRadius: 1,
                                textDecoration: 'none',
                                cursor: 'pointer',
                                '&:hover': {
                                    bgcolor: 'primary.light',
                                }
                            }}
                        >
                            <AccountCircleIcon sx={{ fontSize: 32, color: 'primary.main', mr: 1 }} />
                            <Typography variant="body1" sx={{ color: 'black' }}> {username} </Typography>
                        </Box>
                    </Box>
                )}
                </Box>

            </Box>
        </nav>
    </>
    );
}

export default Navbar;