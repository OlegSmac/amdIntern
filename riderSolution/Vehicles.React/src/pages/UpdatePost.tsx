import React, { useEffect, useState } from "react";
import {
  Box,
  TextField,
  Typography,
  Button,
  Paper,
  Divider,
  MenuItem,
} from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import { useParams, useNavigate } from "react-router-dom";
import axios, { axiosPrivate } from "../api/axios";
import { Snackbar, Alert } from "@mui/material";
import { RadioGroup, FormControlLabel, Radio, FormLabel } from "@mui/material";

type UpdatePostRequest = {
    id: number;
    title: string;
    body: string;
    date: string;
    price: number | "";
    companyId: string;
    vehicleId: number,
    images: string[];
    vehicle: UpdateVehicleRequest;
    categories: string[]
};

type UpdateVehicleRequest = {
    id: number,
    brand: string;
    model: string;
    transmissionType: string;
    fuelType: string;
    color: string;
    year: number | "";
    enginePower: number | "";
    mileage: number | "";
    maxSpeed: number | "";
    engineVolume: number | "";
    fuelConsumption: number | "";
    vehicleType: string;
    carInfo?: {
        bodyType?: string;
        seats?: number;
        doors?: number;
    };
    motorcycleInfo?: {
        hasSidecar?: boolean;
    };
    truckInfo?: {
        cabinType?: string;
        loadCapacity?: number;
    };
};

const UpdatePost = () => {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    const [brands, setBrands] = useState<string[]>([]);
    const [models, setModels] = useState<string[]>([]);
    const [years, setYears] = useState<number[]>([]);
    const [loadingModels, setLoadingModels] = useState(false);
    const [loadingYears, setLoadingYears] = useState(false);

    const [form, setForm] = useState<UpdatePostRequest | null>(null);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        axios.get<string[]>("api/models/brands").then(res => setBrands(res.data));
    }, []);

    useEffect(() => {
        if (!form?.vehicle.brand) return setModels([]);
        setLoadingModels(true);

        axios.get<string[]>(`api/models/models/${form.vehicle.brand}`)
        .then(res => setModels(res.data))
        .catch(() => setModels([]))
        .finally(() => setLoadingModels(false));
    }, [form?.vehicle.brand]);

    useEffect(() => {
        if (!form?.vehicle.brand || !form.vehicle.model) return setYears([]);
        setLoadingYears(true);

        axios.get<number[]>(`api/models/years/${form.vehicle.brand}/${form.vehicle.model}`)
        .then(res => setYears(res.data))
        .catch(() => setYears([]))
        .finally(() => setLoadingYears(false));
    }, [form?.vehicle.brand, form?.vehicle.model]);

    useEffect(() => {
        const fetchPost = async () => {
            try {
                const res = await axios.get(`/api/posts/${id}`);
                const post = res.data;
                const vehicle = post.vehicle || {};

                const extractRelativePath = (fullUrl: string) => {
                    try {
                        const url = new URL(fullUrl);
                        return url.pathname.replace(/^\/images\//, '');
                    } catch {
                        return fullUrl;
                    }
                };

                setForm({
                    id: Number(id),
                    title: post.title || "",
                    body: post.body || "",
                    date: post.date?.split("T")[0] || "",
                    price: post.price || "",
                    companyId: post.company.id || "",
                    vehicleId: vehicle.id,
                    images: (post.images || []).map((img: any) => extractRelativePath(img.url)),
                    categories: post.categories || [],
                    vehicle: {
                        id: vehicle.id,
                        brand: vehicle.brand || "",
                        model: vehicle.model || "",
                        transmissionType: vehicle.transmissionType || "",
                        fuelType: vehicle.fuelType || "",
                        color: vehicle.color || "",
                        year: vehicle.year || "",
                        enginePower: vehicle.enginePower || "",
                        mileage: vehicle.mileage || "",
                        maxSpeed: vehicle.maxSpeed || "",
                        engineVolume: vehicle.engineVolume || "",
                        fuelConsumption: vehicle.fuelConsumption || "",
                        vehicleType: vehicle.vehicleType || "",
                        carInfo: {
                            bodyType: vehicle.bodyType || "",
                            seats: vehicle.seats ?? "",
                            doors: vehicle.doors ?? ""
                        },
                        motorcycleInfo: {
                            hasSidecar: vehicle.hasSidecar || false
                        },
                        truckInfo: {
                            cabinType: vehicle.cabinType || "",
                            loadCapacity: vehicle.loadCapacity ?? ""
                        }
                    },
                });
            } catch (error) {
                console.error("Failed to fetch post:", error);
            }
        };

        fetchPost();
    }, [id]);

    const handlePostChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (!form) return;

        setForm(prev => prev && ({
            ...prev,
            [name]: name === "price" ? (value === "" ? "" : Number(value)) : value,
        }));
    };

    const handleVehicleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (!form) return;
        const isNumberField = ["year", "enginePower", "mileage", "maxSpeed", "engineVolume", "fuelConsumption"].includes(name);

        setForm(prev => prev && ({
            ...prev,
            vehicle: {
                ...prev.vehicle,
                [name]: isNumberField ? (value === "" ? "" : Number(value)) : value,
                ...(name === "brand" && { model: "", year: "" }),
                ...(name === "model" && { year: "" }),
            },
        }));
    };

    const handleCarInfoChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (!form) return;

        setForm(prev => prev && ({
            ...prev,
            vehicle: {
                ...prev.vehicle,
                carInfo: {
                    ...prev.vehicle.carInfo,
                    [name]: ["seats", "doors"].includes(name) ? (value === "" ? "" : Number(value)) : value,
                },
            },
        }));
    };

    const handleMotorcycleInfoChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { value } = e.target;
        if (!form) return;

        setForm(prev => prev && ({
            ...prev,
            vehicle: {
                ...prev.vehicle,
                motorcycleInfo: {
                    ...prev.vehicle.motorcycleInfo,
                    hasSidecar: value === "yes",
                },
            },
        }));
    };

    const handleTruckInfoChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (!form) return;

        setForm(prev => prev && ({
            ...prev,
            vehicle: {
                ...prev.vehicle,
                truckInfo: {
                    ...prev.vehicle.truckInfo,
                    [name]: name === "loadCapacity" ? (value === "" ? "" : Number(value)) : value,
                },
            },
        }));
    };

    const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
        if (!form) return;
        const files = e.target.files;
        if (!files) return;

        const formData = new FormData();
        Array.from(files).forEach(file => {
            formData.append("files", file);
        });

        try {
            const res = await axios.post("api/images/upload", formData);
            const urls: string[] = res.data;

            setForm(prev => prev && ({
                ...prev,
                images: [...prev.images, ...urls],
            }));
        } catch (err) {
            console.error("Upload failed", err);
        }
    };

    const handleRemoveImage = (idx: number) => {
        if (!form) return;
        setForm(prev => prev && ({
            ...prev,
            images: prev.images.filter((_, i) => i !== idx),
        }));
    };

    const handleSubmit = async () => {
        if (!form || !id) return;

        try {
            await axiosPrivate.put(`/api/posts`, form);

            navigate("/managePosts");
        } catch (err : any) {
            console.error("Update failed", err);
            setError(err.response?.data?.message || "Failed to update post.");
        }
    };

    if (!form) return <Typography sx={{ mt: 20, textAlign: "center" }}>Loading post...</Typography>;

    const commonFieldSx = { flex: "0 0 32.4%", maxWidth: "33%" };

    return (
        <Paper sx={{ mt: 10, p: 4, mx: 40, backgroundColor: "#f1f1f1" }}>
            <Box display="flex" justifyContent="center">
                <Typography variant="h5" gutterBottom>
                Update Post
                </Typography>
            </Box>

            <Box display="flex" flexDirection="column" gap={2} mt={2}>
                <Typography variant="h6">Post Info</Typography>

                <TextField
                label="Title"
                name="title"
                value={form.title}
                onChange={handlePostChange}
                />

                <TextField
                label="Body"
                name="body"
                multiline
                rows={4}
                value={form.body}
                onChange={handlePostChange}
                fullWidth
                />

                <Button variant="contained" component="label">
                Upload Images
                <input
                    type="file"
                    hidden
                    multiple
                    accept="image/*"
                    onChange={handleImageUpload}
                />
                </Button>

                {form.images.length > 0 && (
                <Box display="flex" flexDirection="column" gap={1}>
                    {form.images.map((img, idx) => (
                    <Box key={idx} display="flex" gap={1} alignItems="center">
                        <Typography variant="body2" sx={{ flex: 1, wordBreak: "break-all" }}>
                        {img}
                        </Typography>

                        <Button
                        variant="outlined"
                        color="error"
                        size="small"
                        onClick={() => handleRemoveImage(idx)}
                        >
                        <DeleteIcon fontSize="small" />
                        </Button>
                    </Box>
                    ))}
                </Box>
                )}
            </Box>

            <Box mt={4}>
                <Typography variant="h6" gutterBottom pb={1}>
                Vehicle Info
                </Typography>

                <Box display="flex" flexWrap="wrap" gap={2}>
                <TextField
                    select
                    label="Brand"
                    name="brand"
                    value={form.vehicle.brand}
                    onChange={handleVehicleChange}
                    sx={commonFieldSx}
                >
                    {brands.map((br) => (
                    <MenuItem key={br} value={br}>
                        {br}
                    </MenuItem>
                    ))}
                </TextField>

                <TextField
                    select
                    label="Model"
                    name="model"
                    value={form.vehicle.model}
                    onChange={handleVehicleChange}
                    disabled={!form.vehicle.brand || loadingModels}
                    sx={commonFieldSx}
                >
                    {models.map((m) => (
                    <MenuItem key={m} value={m}>
                        {m}
                    </MenuItem>
                    ))}
                </TextField>

                <TextField
                    select
                    label="Year"
                    name="year"
                    value={form.vehicle.year}
                    onChange={handleVehicleChange}
                    disabled={!form.vehicle.model || loadingYears}
                    sx={commonFieldSx}
                >
                    {years.map((y) => (
                    <MenuItem key={y} value={y}>
                        {y}
                    </MenuItem>
                    ))}
                </TextField>

                {[
                    { label: "Transmission Type", name: "transmissionType", value: form.vehicle.transmissionType },
                    { label: "Fuel Type", name: "fuelType", value: form.vehicle.fuelType },
                    { label: "Color", name: "color", value: form.vehicle.color },
                    { label: "Engine Power", name: "enginePower", value: form.vehicle.enginePower, type: "number" },
                    { label: "Mileage", name: "mileage", value: form.vehicle.mileage, type: "number" },
                    { label: "Max Speed", name: "maxSpeed", value: form.vehicle.maxSpeed, type: "number" },
                    { label: "Engine Volume", name: "engineVolume", value: form.vehicle.engineVolume, type: "number" },
                    { label: "Fuel Consumption", name: "fuelConsumption", value: form.vehicle.fuelConsumption, type: "number" },
                ].map(({ label, name, value, type }) => (
                    <TextField
                    key={name}
                    label={label}
                    name={name}
                    value={value}
                    type={type || "text"}
                    onChange={handleVehicleChange}
                    sx={commonFieldSx}
                    />
                ))}

                <Box width="100%" display="flex" justifyContent="flex-start" gap={2}>
                    <TextField
                    label="Vehicle Type"
                    name="vehicleType"
                    select
                    value={form.vehicle.vehicleType}
                    onChange={handleVehicleChange}
                    sx={commonFieldSx}
                    >
                    <MenuItem value="car">Car</MenuItem>
                    <MenuItem value="motorcycle">Motorcycle</MenuItem>
                    <MenuItem value="truck">Truck</MenuItem>
                    </TextField>
                </Box>

                {form.vehicle.vehicleType === "car" && (
                    <>
                    {[
                        { label: "Body Type", name: "bodyType", value: form.vehicle.carInfo?.bodyType || "", type: "text" },
                        { label: "Seats", name: "seats", value: form.vehicle.carInfo?.seats ?? "", type: "number" },
                        { label: "Doors", name: "doors", value: form.vehicle.carInfo?.doors ?? "", type: "number" },
                    ].map(({ label, name, value, type }) => (
                        <TextField
                        key={name}
                        label={label}
                        name={name}
                        value={value}
                        type={type}
                        onChange={handleCarInfoChange}
                        sx={commonFieldSx}
                        />
                    ))}
                    </>
                )}

                {form.vehicle.vehicleType === "motorcycle" && (
                    <TextField
                    label="Has Sidecar"
                    name="hasSidecar"
                    select
                    value={form.vehicle.motorcycleInfo?.hasSidecar ? "yes" : "no"}
                    onChange={handleMotorcycleInfoChange}
                    sx={commonFieldSx}
                    >
                    <MenuItem value="yes">Yes</MenuItem>
                    <MenuItem value="no">No</MenuItem>
                    </TextField>
                )}

                {form.vehicle.vehicleType === "truck" && (
                    <>
                    <TextField
                        label="Cabin Type"
                        name="cabinType"
                        value={form.vehicle.truckInfo?.cabinType || ""}
                        onChange={handleTruckInfoChange}
                        sx={commonFieldSx}
                    />
                    <TextField
                        label="Load Capacity"
                        name="loadCapacity"
                        type="number"
                        value={form.vehicle.truckInfo?.loadCapacity ?? ""}
                        onChange={handleTruckInfoChange}
                        sx={commonFieldSx}
                    />
                    </>
                )}
                </Box>
            </Box>

            <Box mt={4} width="100%" display="flex" justifyContent="flex-start" gap={2}>
                <TextField
                label="Price"
                name="price"
                type="number"
                value={form.price}
                onChange={handlePostChange}
                sx={commonFieldSx}
                />
            </Box>

            <FormLabel component="legend" sx={{mt: 3}}>Category</FormLabel>
            <RadioGroup
                row
                value={form.categories[0] || ""}
                onChange={(e) =>
                    setForm((prev) => ({
                        ...prev!,
                        categories: [e.target.value],
                    }))
                }
            >
                <FormControlLabel value="Cars" control={<Radio />} label="Cars" />
                <FormControlLabel value="Trucks" control={<Radio />} label="Trucks" />
                <FormControlLabel value="Motorcycles" control={<Radio />} label="Motorcycles" />
            </RadioGroup>

            <Divider sx={{ my: 3 }} />

            <Box display="flex" justifyContent="flex-end">
                <Button
                variant="contained"
                onClick={handleSubmit}
                sx={{ width: 150, height: 40 }}
                >
                Update
                </Button>
            </Box>

            <Snackbar
                open={!!error}
                autoHideDuration={4000}
                onClose={() => setError(null)}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
            >
                <Alert severity="error" onClose={() => setError(null)}>
                    {error}
                </Alert>
            </Snackbar>

        </Paper>
    );
};

export default UpdatePost;
