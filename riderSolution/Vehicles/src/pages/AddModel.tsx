import { Box, Button, TextField } from "@mui/material";
import { useEffect, useState, type ChangeEvent } from "react";
import axios from "../api/axios";

type Request = {
    company: string;
    brand: string;
    model: string;
    year: number | "";
}

const AddModel = () => {
    const [form, setForm] = useState<Request>({
        company: "",
        brand: "",
        model: "",
        year: ""
    });

    useEffect(() => {
        const fetchCompanyName = async () => {
            try {
                const response = await axios.get(`/api/accounts/${localStorage.getItem('userId')}`);
                form.company = response.data.name;
            } catch (err) {
                console.log(err);
            }
        }

        fetchCompanyName();
    }, [])

    const handleModelChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setForm((prev) => ({
            ...prev,
            [name]: name === "year" ? (value === "" ? "" : parseInt(value)) : value
        }))
    }

    const handleSubmit = async () => {
        console.log("Submit:", form);

        try {
            const res = axios.post("api/notifications/sendAdminNotification", form);
        } catch (err) {
            console.log(err);
        }
    }
    
    return (
        <Box sx={{ 
            mt: 12,
            flexGrow: 1,
            display: 'flex',
            justifyContent: 'center',
            padding: 4,
            gap: 2 }}>

            <TextField
                label="Brand"
                name="brand"
                value={form.brand}
                onChange={handleModelChange}
            />

            <TextField
                label="Model"
                name="model"
                value={form.model}
                onChange={handleModelChange}
            />

            <TextField
                label="Year"
                name="year"
                type="number"
                value={form.year}
                onChange={handleModelChange}
            />

            <Button 
                variant="contained" 
                onClick={handleSubmit}
                sx={{ width: 150, height: 54, ml: 2 }}
            >
                Send Request
            </Button>
            
        </Box>
    );
}

export default AddModel;