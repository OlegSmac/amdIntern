import { useState } from 'react';
import { Alert, Box, Button, Snackbar, TextField, Typography } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from '../api/axios';
import { useAuth } from '../contexts/AuthContext';

const Login = () => {
    const { login } = useAuth();
    
    const [snackbarOpen, setSnackbarOpen] = useState(false);
    
    const navigate = useNavigate();

    const formik = useFormik({
        initialValues: {
            email: '',
            password: '',
        },

        validationSchema: Yup.object({
            email: Yup.string().email('Invalid email address').required('Required'),
            password: Yup.string()
                .min(8, 'Password must be at least 8 characters')
                .required('Required'),
        }),

        onSubmit: async (values) => {
            try {
                const response = await axios.post('/api/accounts/login', {
                    email: values.email,
                    password: values.password
                });

                login(response.data);

                console.log('Token:', response.data);
                navigate('/');

            } catch (error) {
                console.error('Error during login:', error);
                setSnackbarOpen(true);
            }
        }
    });

    return (
    <>
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                minHeight: '93vh',
            }}
        >
            <Box 
                maxWidth="400px" 
                my="auto"
                mx="auto" 
                sx={{p: 5, border: 1, borderRadius: 1, backgroundColor: '#f1f1f1'}}
            >
                <Typography variant="h5" mb={2}>Login</Typography>
                <form onSubmit={formik.handleSubmit}>
                    <TextField
                        fullWidth
                        id="email"
                        name="email"
                        label="Email"
                        margin="normal"
                        value={formik.values.email}
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                        error={formik.touched.email && Boolean(formik.errors.email)}
                        helperText={formik.touched.email && formik.errors.email}
                    />
                    <TextField
                        fullWidth
                        id="password"
                        name="password"
                        label="Password"
                        type="password"
                        margin="normal"
                        value={formik.values.password}
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                        error={formik.touched.password && Boolean(formik.errors.password)}
                        helperText={formik.touched.password && formik.errors.password}
                    />
                                    
                    <Button 
                        fullWidth 
                        variant="contained" 
                        color="primary" 
                        sx={{mt: 5, height: 40}}
                        type="submit"
                    >
                        Login
                    </Button>

                    <Button 
                        fullWidth 
                        component={Link} 
                        to="/register"
                        variant="outlined" 
                        color="primary" 
                        sx={{mt: 2, height: 40}}
                    >
                        Register
                    </Button>

                </form>
            </Box>

            <Snackbar
                open={snackbarOpen}
                autoHideDuration={6000}
                onClose={() => setSnackbarOpen(false)}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
            >
                <Alert onClose={() => setSnackbarOpen(false)} severity="error" sx={{ width: '100%', backgroundColor: '#f05a5a', color: '#000000'}}>
                    Login failed. Please check your credentials.
                </Alert>
            </Snackbar>
        </Box>
    </>
  );
};

export default Login;
