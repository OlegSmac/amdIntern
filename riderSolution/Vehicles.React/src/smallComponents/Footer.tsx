import React from 'react';
import { Box, Typography } from '@mui/material';

const Footer = () => {
    return (
        <Box
            component="footer"
            sx={{
                width: '100%',
                height: 28,
                py: 1,
                backgroundColor: 'background.paper',
                borderTop: '1px solid #ddd',
                textAlign: 'right'
            }}
        >
            <Typography variant="body2" color="text.secondary" sx={{ mr: 3 }}>
                2025 Amdaris Intern-project Vehicles Dealership. Author: Oleg Smacinih
            </Typography>
        </Box>
    );
};


export default Footer;
