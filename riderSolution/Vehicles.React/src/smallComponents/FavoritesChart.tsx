import React, { useEffect, useState } from 'react';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, ResponsiveContainer } from 'recharts';
import { axiosPrivate } from '../api/axios';

interface FavoriteStat {
    month: number;
    count: number;
    monthName?: string;
}

const monthNames = [
    'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec',
];

const FavoritesChart = () => {
    const [data, setData] = useState<FavoriteStat[]>([]);
    const companyId = localStorage.getItem('userId');

    useEffect(() => {
        if (!companyId) return;

        const fetchData = async () => {
            try {
                const response = await axiosPrivate.get<FavoriteStat[]>(`/api/statistics/lastYearFavoritesCount/${companyId}`);

                const transformed = response.data.map(item => ({
                    ...item,
                    monthName: monthNames[item.month - 1] || 'Unknown',
                }));

                setData(transformed);
            } catch (error) {
                console.error('Failed to fetch favorite stats:', error);
            }
        };

        fetchData();
    }, [companyId]);

    return (
        <ResponsiveContainer width="100%" height={280}>
            <LineChart data={data} margin={{ top: 20, right: 30, left: 0, bottom: 5 }}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="monthName" />
                <YAxis allowDecimals={false} />
                <Tooltip />
                <Line dataKey="count" stroke="#0029c2" />
            </LineChart>
        </ResponsiveContainer>
    );
};

export default FavoritesChart;
