import React, { useEffect, useState, type FC } from 'react';
import { Box, TextField, Button, Typography, IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { axiosPrivate } from '../api/axios';

interface Props {
    from: string;
    to: string;
    onClose: () => void;
}

const SendEmailForm: FC<Props> = ({ from, to, onClose }) => {
    const [subject, setSubject] = useState('');
    const [body, setBody] = useState('');

    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);
    const [error, setError] = useState('');

    const handleSubmit = async () => {
        if (!subject || !body) {
            setError('All fields are required.');
            return;
        }

        setLoading(true);
        setError('');
        try {
            await axiosPrivate.post('/api/emails', {
                from,
                to,
                subject,
                body,
            });
            setSuccess(true);
        } catch (err) {
            setError('Failed to send email.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        if (success) {
            const timer = setTimeout(() => {
                onClose();
            }, 2000);
            return () => clearTimeout(timer);
        }
    }, [success]);

    return (
        <Box 
            sx={{
                position: 'fixed',
                bottom: 60,
                left: '74%',
                width: 400,
                bgcolor: 'white',
                boxShadow: 5,
                p: 3,
                borderRadius: 2,
                zIndex: 1,
            }}
        >
            <Box display="flex" justifyContent="space-between" alignItems="center">
                <Typography variant="h6">Send Email</Typography>
                <IconButton onClick={onClose}><CloseIcon /></IconButton>
            </Box>

            {success ? (
                <Typography sx={{ color: 'green', mt: 2 }}>Email sent successfully!</Typography>
            ) : (
                <>
                    <TextField
                        fullWidth
                        label="Subject"
                        value={subject}
                        onChange={(e) => setSubject(e.target.value)}
                        sx={{ mt: 2 }}
                    />
                    <TextField
                        fullWidth
                        multiline
                        rows={4}
                        label="Message"
                        value={body}
                        onChange={(e) => setBody(e.target.value)}
                        sx={{ mt: 2 }}
                    />
                    
                    {error && 
                        <Typography sx={{ color: 'red', mt: 1 }}>{error}</Typography>
                    }

                    <Button
                        fullWidth
                        variant="contained"
                        sx={{ mt: 2 }}
                        onClick={handleSubmit}
                        disabled={loading}
                    >
                        {loading ? 'Sending...' : 'Send'}
                    </Button>
                </>
            )}
        </Box>
    );
};

export default SendEmailForm;
