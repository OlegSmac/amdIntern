import React, { useEffect, useState } from 'react';
import { BarChart, Bar, CartesianGrid, XAxis, YAxis, Tooltip, ResponsiveContainer } from 'recharts';
import { axiosPrivate } from '../api/axios';

interface MonthStat {
    month: number;
    count: number;
    monthName?: string;
}

const monthNames = [
    'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
    'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec',
];

const PostsChart = () => {
    const [data, setData] = useState<MonthStat[]>([]);
    const companyId = localStorage.getItem("userId");

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axiosPrivate.get<MonthStat[]>(`/api/statistics/lastYearPostsCount/${companyId}`);
                const transformed = response.data.map(item => ({
                    ...item,
                    monthName: monthNames[item.month - 1] || 'Unknown',
                }));
                setData(transformed);
            } catch (err) {
                console.error("Failed to fetch posts count", err);
            }
        };
        fetchData();
    }, [companyId]);

    return (
        <ResponsiveContainer width="100%" height={280}>
            <BarChart data={data} margin={{ top: 20, right: 30, left: 0, bottom: 5 }}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="monthName" />
                <YAxis allowDecimals={false} />
                <Tooltip />
                <Bar dataKey="count" fill="#00bfa5" />
            </BarChart>
        </ResponsiveContainer>
    );
};

export default PostsChart;
