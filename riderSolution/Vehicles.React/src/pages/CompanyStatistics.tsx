import { Box, Typography, Paper } from "@mui/material";
import { useEffect, useState } from "react";
import { axiosPrivate } from "../api/axios";
import FavoritesChart from "../smallComponents/FavoritesChart";
import type { User } from "./User";
import TopPostsList from "../smallComponents/TopPostsList";
import PostsChart from "../smallComponents/PostsChart";

const CompanyStatistics = () => {
    const [loading, setLoading] = useState(true);
    const [userData, setUserData] = useState<User | null>(null);
    const [totalPosts, setTotalPosts] = useState<number>(0);
    const [totalFavorites, setTotalFavorites] = useState<number>(0);

    const userId = localStorage.getItem("userId");

    useEffect(() => {
        const fetchAllData = async () => {
            if (!userId) return;

            try {
                const userRes = await axiosPrivate.get(`/api/application-users/${userId}`);
                const postsRes = await axiosPrivate.get(`/api/statistics/totalCompanyPosts/${userId}`);
                const favsRes = await axiosPrivate.get(`/api/statistics/totalCompanyFavoriteCount/${userId}`);

                setUserData(userRes.data);
                setTotalPosts(postsRes.data);
                setTotalFavorites(favsRes.data);
            } catch (error) {
                console.error("Error fetching statistics:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchAllData();
    }, [userId]);

    if (loading) {
        return (
            <Typography sx={{ mt: 20, textAlign: "center" }}>
                Loading...
            </Typography>
        );
    }

    return (
        <Box sx={{ mt: 12, mx: 25 }}>
            <Typography variant="h5" align="left" sx={{ mb: 4 }}>
                {userData ? `${userData.name} statistics` : "Company statistics"}
            </Typography>

            <Box sx={{ display: "flex", flexDirection: "row", gap: 4, flexWrap: "wrap" }}>
                <Box sx={{ flex: "1 1 300px", display: "flex", flexDirection: "column", gap: 3 }}>
                    <Paper sx={{ p: 2, textAlign: "center" }} elevation={3}>
                        <Typography variant="h6">Total Posts</Typography>
                        <Typography variant="h4" color="primary">{totalPosts}</Typography>
                    </Paper>

                    <Paper sx={{ p: 2, textAlign: "center" }} elevation={3}>
                        <Typography variant="h6">Total Favorites</Typography>
                        <Typography variant="h4" color="secondary">{totalFavorites}</Typography>
                    </Paper>

                    <Box sx={{mt: 1}}>
                        <Typography variant="h6" gutterBottom textAlign={"center"}>Top 3 Posts</Typography>
                        {userId && <TopPostsList companyId={userId} />}
                    </Box>
                </Box>

                <Box sx={{ flex: "2 1 600px", display: "flex", flexDirection: "column", gap: 3 }}>
                    <Paper sx={{ p: 3 }} elevation={3}>
                        <Typography variant="h6" gutterBottom>Posts Created per Month</Typography>
                        <PostsChart />
                    </Paper>

                    <Paper sx={{ p: 3 }} elevation={3}>
                        <Typography variant="h6" gutterBottom>Favorites Count per Month</Typography>
                        <FavoritesChart />
                    </Paper>
                </Box>
            </Box>
        </Box>
    );
};

export default CompanyStatistics;
