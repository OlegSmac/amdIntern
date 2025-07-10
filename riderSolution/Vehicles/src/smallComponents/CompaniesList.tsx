import { Box, Button, Divider, Paper, Typography } from "@mui/material";
import type { Company } from "../pages/Post";
import { useEffect, useState, type FC } from "react";
import axiosInstance from "../api/axios";
import axios from "../api/axios";
import SendEmailForm from "./SendEmailForm";

interface CompaniesListProps {
    companies: Company[];
    subscribeIds: string[];
}

const CompaniesList: FC<CompaniesListProps> = ({ companies, subscribeIds }) => {
    const [companiesList, setCompaniesList] = useState<Company[]>(companies);
    const [subscribedIds, setSubscribedIds] = useState<string[]>(subscribeIds);

    const [showEmailForm, setShowEmailForm] = useState<string | null>(null);
    const [userEmail, setUserEmail] = useState<string>("");
    
    const userId = localStorage.getItem('userId');

    useEffect(() => {
        setCompaniesList(companies);
        setSubscribedIds(subscribeIds);
    }, [companies, subscribeIds]);

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
    
    const handleSubscribe = async (companyId: string) => {
        const payload = { userId, companyId };
        console.log("Sending subscribe payload:", payload);
        try {
            await axios.post("/api/subscriptions", payload);
            setSubscribedIds(prev => [...prev, companyId]);
        } catch (error) {
            console.error("Failed to subscribe:", error);
        }
    };

    const handleUnsubscribe = async (companyId: string) => {
        const payload = { userId, companyId };
        console.log("Sending subscribe payload:", payload);
        try {
            await axios.delete("/api/subscriptions", {
                data: payload
            });
            setSubscribedIds(prev => prev.filter(id => id !== companyId));
        } catch (error) {
            console.error("Failed to unsubscribe:", error);
        }
    };

    const isSubscribed = (companyId: string) => subscribedIds.includes(companyId);

    return (
        <Box mt={4} mx={55}>
            <Paper sx={{ p: 2, mb: 2, backgroundColor: '#f1f1f1' }}>
                <Box display="grid" gridTemplateColumns="1.3fr 1.3fr 1fr 2fr" alignItems="center" gap={2}>
                <Typography variant="subtitle2">Name</Typography>
                <Typography variant="subtitle2">Email</Typography>
                <Typography variant="subtitle2">Phone</Typography>
                </Box>
            </Paper>

            {companiesList.map((company) => (
                <Paper key={company.id} sx={{ px: 2, py: 1, mb: 2 }}>
                    <Box display="grid" gridTemplateColumns="1.3fr 1.3fr 1fr 2fr" alignItems="center" gap={2}>
                        <Typography>{company.name}</Typography>
                        <Typography>{company.email}</Typography>
                        <Typography>{company.phone}</Typography>

                        <Box display="flex" gap={4}>
                            <Button
                                variant="contained"
                                onClick={() => setShowEmailForm(company.id)}
                                sx={{ width: 130 }}
                            >
                                Send Email
                            </Button>

                            {isSubscribed(company.id) ? (
                                <Button
                                    variant="outlined"
                                    color="error"
                                    onClick={() => handleUnsubscribe(company.id)}
                                    sx={{ width: 130 }}
                                >
                                    Unsubscribe
                                </Button>
                            ) : (
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={() => handleSubscribe(company.id)}
                                    sx={{ width: 130 }}
                                >
                                    Subscribe
                                </Button>
                            )}
                        </Box>

                        {showEmailForm === company.id && (
                            <SendEmailForm
                                from={userEmail}
                                to={company.email}
                                onClose={() => setShowEmailForm(null)}
                            />
                        )}

                    </Box>
                </Paper>
            ))}

            {companiesList.length === 0 && (
                <>
                    <Divider sx={{ my: 2 }} />
                    <Typography variant="body2" color="text.secondary">
                        No posts found.
                    </Typography>
                </>
            )}

        </Box>
    );
}

export default CompaniesList;