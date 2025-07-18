import './App.css';
import './pages/Post.css'
import Navbar from "./smallComponents/Navbar";
import Main from "./pages/Main";
import Register from './pages/Register';
import Login from './pages/Login';
import User from './pages/User';
import Post from "./pages/Post";
import AllPosts from './pages/AllPosts';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import theme from './theme'
import { ThemeProvider } from '@mui/material/styles';
import "react-image-gallery/styles/css/image-gallery.css";
import ManagePosts from './pages/ManagePosts';
import CreatePost from './pages/CreatePost';
import UpdatePost from './pages/UpdatePost';
import FavoritePosts from './pages/FavoritePosts';
import AddModel from './pages/AddModel';
import Companies from './pages/Companies';
import Notifications from './pages/Notifications';
import Footer from './smallComponents/Footer';
import { Box } from '@mui/material';
import { PageContextProvider } from './contexts/PageContext';
import GoogleCallback from './pages/GoogleCallback';
import CompanyStatistics from './pages/CompanyStatistics';

function App() {
  return (
    <>
      <ThemeProvider theme={theme}>
        <BrowserRouter>
          <PageContextProvider>
            <Box
              sx={{
                minHeight: '100vh',
                display: 'flex',
                flexDirection: 'column',
              }}
            >
              <Box 
                component="main"
                sx={{flex: 1}}
              >
                <Navbar />

                <Routes>
                  <Route path="/" element={<Main />} />
                  <Route path="/register" element={<Register />} />
                  <Route path="/login" element={<Login />} />
                  <Route path="/user" element={<User />} />

                  <Route path="/notifications" element={<Notifications />} />

                  <Route path="/post/:id" element={<Post />} />
                  <Route path="/allPosts" element={<AllPosts />} />
                  <Route path="/favoritePosts" element={<FavoritePosts />} />
                  <Route path='/managePosts' element={<ManagePosts />} />
                  <Route path='/createPost' element={<CreatePost />} />
                  <Route path='/updatePost/:id' element={<UpdatePost />} />
                  <Route path='/addModel' element={<AddModel />} />

                  <Route path='/statistics' element={<CompanyStatistics />} />

                  <Route path='/companies' element={<Companies />} />

                  <Route path="/google-callback" element={<GoogleCallback />} />
                </Routes>
              </Box>

              <Footer /> 
            </Box>
          </PageContextProvider>
        </BrowserRouter>
      </ThemeProvider>
    </>
  );
}

export default App;
