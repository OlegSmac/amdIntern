import { useState } from 'react';
import { Alert, Box, Button, MenuItem, Select, Snackbar, TextField, Typography } from '@mui/material';
import GoogleIcon from '@mui/icons-material/Google';
import { Link, useNavigate } from 'react-router-dom';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios, { BASE_URL } from '../api/axios';
import { useAuth } from '../contexts/AuthContext';

const Register = () => {
    const { login } = useAuth();

    const [snackbarOpen, setSnackbarOpen] = useState(false);
    
    const navigate = useNavigate();

    const formik = useFormik({
        initialValues: {
            type: 'user',
            email: '',
            password: '',
            username: '',
            phone: '',
            firstName: '',
            lastName: '',
            name: '',
            description: '',
        },

        validationSchema: Yup.object({
            type: Yup.string().required('Required'),
            email: Yup.string().email('Invalid email address').required('Required'),
            password: Yup.string().min(8, 'Password must be at least 8 characters').required('Required'),
            username: Yup.string().required('Required'),
            phone: Yup.string().required('Required'),
            firstName: Yup.string().when('type', ([type], schema) =>
                type === 'user' ? schema.required('Required') : schema
            ),
            lastName: Yup.string().when('type', ([type], schema) =>
                type === 'user' ? schema.required('Required') : schema
            ),
            name: Yup.string().when('type', ([type], schema) =>
                type === 'company' ? schema.required('Required') : schema
            ),
            description: Yup.string().when('type', ([type], schema) =>
                type === 'company' ? schema.required('Required') : schema
            )
        }),

        onSubmit: async (values) => {
            try {
                const response = await axios.post('/api/accounts/register', values);

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
            maxWidth="400px" 
            mx="auto" 
            mt={11} 
            sx={{p: 4, border: 1, borderRadius: 1, backgroundColor: '#f1f1f1'}}
        >
            <Typography variant="h5" mb={2}>Register</Typography>
            <form onSubmit={formik.handleSubmit}>
                
                <Select
                    fullWidth
                    id="type"
                    name="type"
                    value={formik.values.type}
                    onChange={formik.handleChange}
                    sx={{ mb: 2 }}
                >
                    <MenuItem value="user">User</MenuItem>
                    <MenuItem value="company">Company</MenuItem>
                </Select>

                {formik.values.type === 'user' && (
                    <>
                        <TextField
                            fullWidth
                            id="firstName"
                            name="firstName"
                            label="First Name"
                            margin="normal"
                            value={formik.values.firstName}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            error={formik.touched.firstName && Boolean(formik.errors.firstName)}
                            helperText={formik.touched.firstName && formik.errors.firstName}
                        />
                        <TextField
                            fullWidth
                            id="lastName"
                            name="lastName"
                            label="Last Name"
                            margin="normal"
                            value={formik.values.lastName}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            error={formik.touched.lastName && Boolean(formik.errors.lastName)}
                            helperText={formik.touched.lastName && formik.errors.lastName}
                        />
                    </>
                )}

                {formik.values.type === 'company' && (
                    <>
                        <TextField
                            fullWidth
                            id="name"
                            name="name"
                            label="Company Name"
                            margin="normal"
                            value={formik.values.name}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            error={formik.touched.name && Boolean(formik.errors.name)}
                            helperText={formik.touched.name && formik.errors.name}
                        />
                        <TextField
                            fullWidth
                            id="description"
                            name="description"
                            label="Company Description"
                            margin="normal"
                            multiline
                            minRows={3}
                            value={formik.values.description}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            error={formik.touched.description && Boolean(formik.errors.description)}
                            helperText={formik.touched.description && formik.errors.description}
                        />
                    </>
                )}

                <TextField
                    fullWidth
                    id="username"
                    name="username"
                    label="Username"
                    margin="normal"
                    value={formik.values.username}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    error={formik.touched.username && Boolean(formik.errors.username)}
                    helperText={formik.touched.username && formik.errors.username}
                />

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
                    id="phone"
                    name="phone"
                    label="Phone"
                    margin="normal"
                    value={formik.values.phone}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    error={formik.touched.phone && Boolean(formik.errors.phone)}
                    helperText={formik.touched.phone && formik.errors.phone}
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
                    Register
                </Button>

                <Box sx={{ display: 'flex', gap: 2, mt: 1 }}>
                    <Button 
                        component={Link} 
                        to="/login" 
                        variant="outlined" 
                        color="primary" 
                        sx={{mt: 2, height: 40, width: 250 }}
                    >
                        Login
                    </Button>

                    <Button
                        variant="outlined"
                        color="secondary"
                        sx={{ mt: 2, height: 40, width: 250, background: 'white', color: 'black' }}
                        onClick={() => {
                            window.location.href = `${BASE_URL}/api/google/google-login?prompt=select_account`;
                        }}
                        endIcon={<GoogleIcon />}
                    >
                        Google
                    </Button>
                </Box>

            </form>
        </Box>

        <Snackbar
            open={snackbarOpen}
            autoHideDuration={6000}
            onClose={() => setSnackbarOpen(false)}
            anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
        >
            <Alert onClose={() => setSnackbarOpen(false)} severity="error" sx={{ width: '100%', backgroundColor: '#f05a5a', color: '#000000'}}>
                Register failed. Please check your credentials.
            </Alert>
        </Snackbar>
    </>
  );
};

export default Register;
