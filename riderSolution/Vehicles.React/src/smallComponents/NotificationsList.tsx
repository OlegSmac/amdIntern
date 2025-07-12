import { Box, Button, Link, Paper, Typography } from "@mui/material";
import { useState, type FC } from "react";
import type { Notification } from "../pages/Notifications";
import axios, { axiosPrivate } from "../api/axios";

interface PropsNotificationsList {
    notifications: Notification[];
}

const extractTextAndLink = (html: string): { message: string; linkText: string; href: string } => {
    const div = document.createElement("div");
    div.innerHTML = html;
    const paragraphs = div.querySelectorAll("p");

    const message = paragraphs[0]?.textContent ?? "";
    const link = paragraphs[1]?.querySelector("a");
    const linkText = link?.textContent ?? "";
    const href = link?.getAttribute("href") ?? "#";

    return { message, linkText, href };
};

const NotificationsList: FC<PropsNotificationsList> = ({ notifications }) => {
    const [localNotifications, setLocalNotifications] = useState(notifications);

    const markResolvedLocally = (id: number) => {
        setLocalNotifications(prev =>
            prev.map(n => (n.id === id ? { ...n, isResolved: true } : n))
        );
    };

    const handleAcceptModel = async (notification: Notification) => {
        try {
            if (notification.type === "Admin") {
                const modelData = {
                    brand: notification.brand,
                    model: notification.model,
                    year: notification.year,
                };
                await axiosPrivate.post("/api/models", modelData);
            }
            await axiosPrivate.post(`/api/notifications/setResolved/${notification.id}`);
            markResolvedLocally(notification.id);
        } catch (err) {
            console.log(err);
        }
    };

    const handleDeclineModel = async (notification: Notification) => {
        try {
            await axiosPrivate.post(`/api/notifications/setResolved/${notification.id}`);
            markResolvedLocally(notification.id);
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <Box maxWidth="1700px" mx={40} px={1}>
            {localNotifications.map((notification) => {
                const { message, linkText, href } = extractTextAndLink(notification.body);
                const isAdmin = notification.type === "Admin";
                const showButtons = isAdmin && !notification.isResolved;

                return (
                    <Paper
                        key={notification.id}
                        elevation={1}
                        sx={{
                            px: 2,
                            py: 1,
                            mb: 1,
                            border: '1px solid',
                            borderColor: 'divider',
                            borderRadius: 1,
                        }}
                    >
                        <Box
                            display="grid"
                            gridTemplateColumns={
                                isAdmin
                                    ? "1.1fr 1.8fr 0.9fr 1.4fr"
                                    : "1.1fr 1.8fr 0.7fr 0.3fr"
                            }
                            alignItems="center"
                            gap={1}
                        >
                            <Typography variant="subtitle1" fontWeight="bold">
                                {notification.title}
                            </Typography>

                            <Box>
                                <Typography>
                                    {isAdmin ? notification.body : message}
                                </Typography>
                                {(!isAdmin || linkText) && (
                                    <Link href={href} underline="hover" target="_blank" rel="noopener">
                                        {linkText}
                                    </Link>
                                )}
                            </Box>

                            <Typography fontSize={14}>
                                {new Date(notification.createdAt).toLocaleString()}
                            </Typography>

                            <Box display="flex" justifyContent="flex-end" gap={3} alignItems="center">
                                {showButtons && (
                                    <>
                                        <Button
                                            variant="contained"
                                            size="small"
                                            color="success"
                                            onClick={() => handleAcceptModel(notification)}
                                            sx={{width: 120, height: 35}}
                                        >
                                            Accept
                                        </Button>
                                        <Button
                                            variant="contained"
                                            size="small"
                                            color="error"
                                            onClick={() => handleDeclineModel(notification)}
                                            sx={{width: 120, height: 35}}
                                        >
                                            Decline
                                        </Button>
                                    </>
                                )}
                                <Box
                                    sx={{
                                        width: 10,
                                        height: 10,
                                        borderRadius: '50%',
                                        backgroundColor: notification.isRead ? 'white' : 'primary.main',
                                    }}
                                />
                                
                            </Box>
                        </Box>
                    </Paper>
                );
            })}
        </Box>
    );
};

export default NotificationsList;
