import { Box, Button, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import axios from "../api/axios";
import NotificationsList from "../smallComponents/NotificationsList";

type BaseNotification = {
    id: number;
    title: string;
    body: string;
    createdAt: Date;
    isSent: boolean;
    isRead: boolean;
};

type UserNotification = BaseNotification & {
    type: "User";
    userId: number;
};

type CompanyNotification = BaseNotification & {
    type: "Company";
    companyId: number;
};

type AdminNotification = BaseNotification & {
    type: "Admin";
    adminId: number;
    isResolved: boolean;
    brand: string;
    model: string;
    year: number;
};

export type Notification = UserNotification | CompanyNotification | AdminNotification;

const Notifications = () => {
    const [notifications, setNotifications] = useState<Notification[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const [currentPage, setCurrentPage] = useState(0);
    const [totalPages, setTotalPages] = useState(1);
    const pageSize = 10;

    const [role, setRole] = useState('');

    useEffect(() => {
        const fetchRole = async () => {
            try {
                const roleResponse = await axios.post('/api/accounts/getRoleFromToken', {
                    token: localStorage.getItem('authToken'),
                });
                setRole(roleResponse.data);
            } catch (err) {
                console.error("Failed to fetch role: ", err);
            }
        }

        fetchRole();
    }, []);
    
    useEffect(() => {
        if (!role) return;

        const fetchPagedUserNotifications = async (pageIndex: number) => {
            setLoading(true);

            const filters = [];
            if (role === "User") filters.push({ path: "userId", value: localStorage.getItem("userId") });
            if (role === "Company") filters.push({ path: "companyId", value: localStorage.getItem("userId") });
            if (role === "Admin") filters.push({ path: "adminId", value: localStorage.getItem("userId") });
            
            const requestBody = {
                pageIndex: pageIndex,
                pageSize: pageSize,
                columnNameForSorting: "createdAt",
                sortDirection: "desc",
                requestFilters: {
                    logicalOperator: 0,
                    filters: filters
                }
            };

            try {
                const response = await axios.post(`/api/notifications/getPagedNotifications/${role}`, requestBody);
                
                const typedItems = response.data.items.map((item: any) => ({
                    ...item,
                    type: role
                }));
                setNotifications(typedItems);
                setTotalPages(Math.ceil(response.data.total / pageSize));
            } catch (err) {
                console.error('Error fetching notifications:', err);
                setError('Failed to load notifications.');
            } finally {
                setLoading(false);
            }
        } 

        fetchPagedUserNotifications(currentPage);
    }, [role, currentPage]);

    useEffect(() => {
        const unreadNotificationIds = notifications.filter(n => !n.isRead).map(n => n.id);
        unreadNotificationIds.forEach(id => {
            axios.post(`/api/notifications/setRead/${id}`)
        });
        
    }, [notifications]); 

    const handlePrevious = () => {
        if (currentPage > 0) setCurrentPage(prev => prev - 1);
    };

    const handleNext = () => {
        if (currentPage < totalPages - 1) setCurrentPage(prev => prev + 1);
    };

    if (loading) {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                Loading...
            </div>
        );
    }

    if (!loading && error) {
        return (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                {error || 'Notifications not found.'}
            </div>
        );
    }

    return (
        <Box sx={{ mt: 10, minHeight: '85vh' }}>
            <Typography variant="h5" sx={{ mt: 2, ml: 41, mb: 3, color: 'text.primary', fontSize: '1.5rem'}}>
                Your Notifications
            </Typography>

            <NotificationsList notifications={notifications} />

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

export default Notifications;